using System;
using System.Collections.Generic;
using System.Linq;
using Appside.Scripts.ScriptableObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Appside.Scripts.SceneLoading
{
    public class RandomLevelLoader : MonoBehaviour, ILevelsLoader
    {
        [SerializeField] private AllLevels allLevels;
        [SerializeField] private RandomLevelsProgressStorage progressStorage;

        private IRandomLevelsProgress _progress;

        private void Awake()
        {
            _progress = progressStorage;
        }

        public Level[] LoadLevels(int count)
        {
            Level[] res = new Level[count];
            Level completeLevel = CrossSceneStorage.GetCompleteLevel();
            if (completeLevel != null)
            {
                _progress.SaveCompleteLevel(completeLevel);
            }
        
            Dictionary<Level, Level> generatedToActual = _progress.GetSavedLevelMap();
            List<string> usedNames = _progress.GetUsedLevelsNames();

            for (int i = 0; i < count; i++)
            {
                Level l = GetAt(i, generatedToActual);
                if (l == null)
                {
                    (Level, Level) generatedLevel = GenerateNewLevel(generatedToActual, usedNames);
                    generatedToActual = _progress.SaveGeneratedLevel(generatedLevel.Item1, generatedLevel.Item2);
                    usedNames = _progress.SaveUsedLevelName(generatedLevel.Item1.label);
                    
                    l = generatedLevel.Item1;
                }
                res[i] = l;
            }
        
            return res;
        }

        //(generated, actual)
        private (Level, Level) GenerateNewLevel(
            Dictionary<Level, Level> generatedToActual, 
            List<string> usedNames)
        {
            Level lastGenerated = GetAt(-1, generatedToActual, last: true);
            Level lastActual;
            if (lastGenerated == null)
            {
                lastActual = null;
            }
            else
            {
                lastActual = generatedToActual[lastGenerated];
            }

            Level nextActual = GetRandomLevelExcept(
                lastActual, 
                allLevels.GetAllLevels());
            string nextName = GetNextName(usedNames);
            Level nextGenerated = Instantiate(nextActual);
            nextGenerated.label = nextName;
            SceneLoader.Get().SaveObject(nextGenerated);
            return (nextGenerated, nextActual);
        }


        private Level GetAt(
            int position,
            Dictionary<Level, Level> generatedToActual,
            bool first = false,
            bool last = false)
        {
            if (generatedToActual == null) return null;
            Level[] keys = generatedToActual.Keys.ToArray();
            if (first) position = 0;
            if (last) position = keys.Length - 1;
            if (position < 0 ||  position>=keys.Length) return null;
            Array.Sort(keys, (l1, l2) =>
            {
                int i1 = int.Parse(l1.label);
                int i2 = int.Parse(l2.label);
                return (i1 > i2) ? 1 : -1;
            });
            return keys[position];
        }
    
        private string GetNextName(List<string> names)
        {
            int last = 0;
            foreach (string n in names)
            {
                int current = int.Parse(n);
                if (current > last) last = current;
            }
            return "" + (last+1);
        }
        private Level GetRandomLevelExcept(Level level, List<Level> from)
        {
            Level res = from[Random.Range(0, from.Count)];
            if (res == level) return GetRandomLevelExcept(level, from);
            return res;
        }
    }

    public interface IRandomLevelsProgress{
        Dictionary<Level, Level> GetSavedLevelMap();
        List<string> GetUsedLevelsNames();
        void SaveCompleteLevel(Level level);
        Dictionary<Level, Level> SaveGeneratedLevel(Level generated, Level actual);
        List<string> SaveUsedLevelName(string name);
    }
}
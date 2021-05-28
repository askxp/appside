using System;
using System.Collections.Generic;
using Appside.Scripts.SceneLoading;
using UnityEngine;

namespace Appside.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "RandomLevelsProgressStorage", menuName = "Data/RandomLevelsProgress", order = 1)]
    public class RandomLevelsProgressStorage : ScriptableObject,IRandomLevelsProgress
    {
        private Dictionary<Level, Level> _generatedToActual;
        //how could i forget that Dictionaries are not serializable 
        //it will be deserialized from two next lists 
        [SerializeField] private List<Level> actual;
        [SerializeField] private List<Level> generated;
    
        private List<String> _usedLevelLabels;

        public Dictionary<Level, Level> GetSavedLevelMap()
        {
            if (_generatedToActual == null)
            {
                InitDictionary();
            }

            return _generatedToActual;
        }

        private void InitDictionary()
        {
            _generatedToActual = new Dictionary<Level, Level>();
            if(generated==null) generated = new List<Level>();
            if(actual==null) actual = new List<Level>();
            for (int i = 0; i < generated.Count; i++)
            {
                //HOTFIX: remove it after scriptable object will implemented
                if(generated[i]==null || actual[i] == null) continue;
                _generatedToActual.Add(generated[i],actual[i]);
            }
        }

        public List<string> GetUsedLevelsNames()
        {
            if (_usedLevelLabels == null)
            {
                _usedLevelLabels = new List<string>();
            }

            return _usedLevelLabels;
        }

        public void SaveCompleteLevel(Level level)
        {
            //HOTFIX: REMOVE
            var d = GetSavedLevelMap();
            int i = generated.IndexOf(level);
            try
            {
                generated.RemoveAt(i);
                actual.RemoveAt(i);
                d.Remove(level);
                _generatedToActual = d;
            }
            catch (Exception)
            {
                Debug.LogWarning("Scriptable object wiped out");
            }
        }

        public Dictionary<Level, Level> SaveGeneratedLevel(Level g, Level a)
        {
            GetSavedLevelMap().Add(g,a);
            generated.Add(g);
            actual.Add(a);
            //TODO: save scriptable object to data folder 
            return _generatedToActual;
        }

        public List<string> SaveUsedLevelName(string label)
        {
            GetUsedLevelsNames().Add(label);
            return _usedLevelLabels;
        }
    }
}
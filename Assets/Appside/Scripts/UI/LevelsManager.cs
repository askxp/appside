using System;
using System.Collections;
using System.Collections.Generic;
using Appside.Scripts.SceneLoading;
using Appside.Scripts.ScriptableObjects;
using UnityEngine;

public class LevelsManager : MonoBehaviour
{
    [SerializeField] private LevelListItem[] items;
    [SerializeField] private RandomLevelLoader randomLoader;
    
    private ILevelsLoader _loader;

    private void Awake()
    {
        _loader = randomLoader;
    }

    private void Start()
    {
        Level[] levels = _loader.LoadLevels(items.Length);
        for (int i = 0; i < items.Length; i++)
        {
            items[i].SetLevel(levels[i]);
            items[i].OnLevelChoosen = OnLevelChosen;
            items[i].OnSelected = OnListItemSelected;
        }
    }

    private void OnLevelChosen(Level level)
    {
        Debug.Log("Chosen" + level.label);
        CrossSceneStorage.SetChosenLevel(level);
        SceneLoader.LoadScene(SceneLoader.GAME_SCENE);
    }

    private void OnListItemSelected(LevelListItem item)
    {
        foreach (LevelListItem i in items)
        {
            i.Unselect();
        }

        item.Select();
    }
}



public interface ILevelsLoader
{
    Level[] LoadLevels(int count);
}

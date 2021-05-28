using System;
using System.Collections;
using System.Collections.Generic;
using Appside.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Button))]
public class LevelListItem : MonoBehaviour
{
    [SerializeField] private Color[] colors;
    [SerializeField] private Text label;
    [SerializeField] private Button tapToPlay;
    [SerializeField] private Image image;
    [SerializeField] private Image bg;

    private Button _button;

    private Level _level;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        bg.color = GetRandomColor();
        _button.onClick.AddListener(() =>
        {
            OnSelected(this);
        });
        Unselect();
    }

    public Action<LevelListItem> OnSelected = delegate {  };
    public Action<Level> OnLevelChoosen = delegate(Level level) {  };
    
    public void SetLevel(Level level)
    {
        _level = level;
        label.text = level.label;
        image.sprite = level.preview;
        tapToPlay.onClick.RemoveAllListeners();
        tapToPlay.onClick.AddListener(() =>
        {
            OnLevelChoosen(_level);
        });
    }

    public void Unselect()
    {
        tapToPlay.gameObject.SetActive(false);
        Color t = bg.color;
        bg.color = new Color(t.r,t.g,t.b,0.5f);
    }

    public void Select()
    {
        tapToPlay.gameObject.SetActive(true);
        Color t = bg.color;
        bg.color = new Color(t.r,t.g,t.b,0.7f);
    }

    private Color GetRandomColor()
    {
        return colors[Random.Range(0, colors.Length)];
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using Appside.Scripts.SceneLoading;
using UnityEngine;

namespace Appside.Scripts
{
    public class LevelManager : MonoBehaviour
    {
        private static LevelManager _instance;
    
        public Action onComplete = delegate {  };
        public Action<int, int> onProgress = delegate {  };
    
        private Dictionary<IDestructive, bool> _objects;
        private int _objectsCount;
        private int _objectsDestroyed;

        public static LevelManager Get()
        {
            if (_instance == null)
            {
                Debug.LogError("Level Manager not exists");
            }
            return _instance;
        }
    
        private void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("LevelManager already exists!");
            }
            _instance = this;
            _objects = new Dictionary<IDestructive, bool>();
        }

        public void RegisterObject(Destructive o)
        {
            _objects.Add(o, false);
            o.onDestroyed += OnObjectDestroyed;
            CalculateObjects();
        }

        private void OnObjectDestroyed(IDestructive o)
        {
            _objects[o] = true;
            CalculateObjects();
        }

        private void CalculateObjects()
        {
            _objectsCount = _objects.Count;
            int d = 0;
            foreach (var kv in _objects)
            {
                if (kv.Value) d++;
            }
            _objectsDestroyed = d;
            onProgress(_objectsDestroyed, _objectsCount);
            if(d==_objectsCount) StartCoroutine(nameof(CompleteLevel));
        }

        private IEnumerator CompleteLevel()
        {
            onComplete.Invoke(); 
            yield return new WaitForSeconds(3f);
            SceneLoader.LoadScene(SceneLoader.MENU_SCENE);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                StartCoroutine(nameof(CompleteLevel));
            }
        }
    }
}


using Appside.Scripts.ScriptableObjects;
using UnityEngine;

namespace Appside.Scripts.SceneLoading
{
    public class LevelLoader : MonoBehaviour
    {
        [SerializeField] 
        private Level testLevel;

        private Level _currentLevel;
        private void Awake()
        {
            Level level = CrossSceneStorage.GetChosenLevel();
            if (level == null)
            {
                level = testLevel;
            }
            _currentLevel = level;
            Instantiate(level.levelPrefab);
        }

        private void Start()
        {
            LevelManager.Get().onComplete += () =>
            {
                CrossSceneStorage.SetCompleteLevel(_currentLevel);
            };
        }
    }
}

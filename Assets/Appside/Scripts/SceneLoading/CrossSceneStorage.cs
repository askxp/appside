
using Appside.Scripts.ScriptableObjects;

namespace Appside.Scripts.SceneLoading
{
    public static class CrossSceneStorage
    {
        private static Level _chosenLevel;
        private static Level _completeLevel;
    
        public static Level GetChosenLevel()
        {
            return _chosenLevel;
        }

        public static void SetChosenLevel(Level level)
        {
            _chosenLevel = level;
            SceneLoader.Get().SaveObject(_chosenLevel);
        }

        public static Level GetCompleteLevel()
        {
            return _completeLevel;
        }

        public static void SetCompleteLevel(Level level)
        {
            _completeLevel = level;
            SceneLoader.Get().SaveObject(_completeLevel);
        }

    
    }
}
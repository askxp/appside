using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Appside.Scripts.SceneLoading
{
    public class SceneLoader : MonoBehaviour
    {
        public static readonly int MENU_SCENE = 0;
        public static readonly int GAME_SCENE = 1;
        
        private static SceneLoader _instance;
        
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public static SceneLoader Get()
        {
            return _instance;
        }

        //HOTFIX:
        public void SaveObject(Object o)
        {
            DontDestroyOnLoad(o);
        }

        public static void LoadScene(int sceneIndex)
        {
            _instance.gameObject.SetActive(true);
            _instance.StartCoroutine(LoadSceneCor(sceneIndex));
        }

        public static void LoadScene(string sceneName)
        {
            _instance.gameObject.SetActive(true);
            _instance.StartCoroutine(LoadSceneCor(sceneName));
        }

        private static IEnumerator LoadSceneCor(int sceneIndex)
        {
            var loading = SceneManager.LoadSceneAsync(sceneIndex);
            while (!loading.isDone)
            {
                yield return null;
            }
            yield return new WaitForSecondsRealtime(1);
            _instance.gameObject.SetActive(false);
        } 

        private static IEnumerator LoadSceneCor(string sceneName)
        {
            var loading = SceneManager.LoadSceneAsync(sceneName);
            while (!loading.isDone)
            {
                yield return null;
            }
            yield return new WaitForSecondsRealtime(1);
            _instance.gameObject.SetActive(false);
        }
    }
}


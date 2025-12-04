using UnityEngine;
using UnityEngine.SceneManagement;

public class DATA_MasterSceneManager : MonoBehaviour
{
    public static void Load(string sceneName, bool additive = false)
    {
        LoadSceneMode mode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        SceneManager.LoadScene(sceneName, mode);
    }

    public static AsyncOperation LoadAsync(string sceneName, bool additive = false)
    {
        LoadSceneMode mode = additive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        return SceneManager.LoadSceneAsync(sceneName, mode);
    }

    public static AsyncOperation Unload(string sceneName)
    {
        return SceneManager.UnloadSceneAsync(sceneName);
    }

    public static void SetActive(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        if (scene.IsValid())
        {
            SceneManager.SetActiveScene(scene);
        }
        else
        {
            Debug.LogWarning($"Scene {sceneName} is not loaded.");
        }
    }

    /// <summary>
    /// Load a new scene additively, unload the previous, and set the new one active.
    /// </summary>
    public static void ChangeToScene(string previousScene, string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        asyncLoad.completed += (op) =>
        {
            // Set the newly loaded scene active
            Scene newScene = SceneManager.GetSceneByName(sceneName);
            if (newScene.IsValid())
            {
                SceneManager.SetActiveScene(newScene);
            }

            // Unload the previous scene
            if (!string.IsNullOrEmpty(previousScene))
            {
                SceneManager.UnloadSceneAsync(previousScene);
            }
        };
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoader : MonoBehaviour
{
    public string sceneName;

    public void LoadSceneAdditively(string targetSceneName = null)
    {
        if (targetSceneName == null){
            targetSceneName = sceneName;
        }

        SceneManager.LoadScene(targetSceneName, LoadSceneMode.Additive);
    }

    public void Quit(){
        Application.Quit();
    }
}
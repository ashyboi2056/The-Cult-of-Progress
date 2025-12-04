//ASH+_GENERIC_SCRIPTS_#2

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public string sceneName;

    public void LoadScene(string targetSceneName = null)
    {
        if (targetSceneName == null){
            targetSceneName = sceneName;
        }

        SceneManager.LoadScene(targetSceneName);
    }

    public void Quit(){
        Application.Quit();
    }
}
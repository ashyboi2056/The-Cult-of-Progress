using UnityEngine;
using UnityEngine.SceneManagement;

public class SCENE_CallSceneManager : MonoBehaviour
{
    public void ChangeToScene(string newScene)
    {
        string sourceScene = SceneManager.GetActiveScene().name;
        DATA_MasterSceneManager.ChangeToScene(sourceScene,newScene);
    }

    public void SaveAndQuit()
    {
        //Save
        if (FindFirstObjectByType<PlayerPersistantDataContainer>() != null){ SaveManager.SaveData(FindFirstObjectByType<PlayerPersistantDataContainer>().data); }
        
        //And Quit
        Application.Quit();
    }
}
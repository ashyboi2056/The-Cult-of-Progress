using UnityEngine;
using UnityEngine.SceneManagement;

public class SCENE_CallSceneManager : MonoBehaviour
{
    public string openSceneAdditivelyOnAwake;
    public string removeSceneOnAwake;

    void Awake()
    {
        if (openSceneAdditivelyOnAwake != ""){ DATA_MasterSceneManager.Load(openSceneAdditivelyOnAwake,true); }
        if (removeSceneOnAwake != ""){ DATA_MasterSceneManager.Unload(removeSceneOnAwake); }
    }

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
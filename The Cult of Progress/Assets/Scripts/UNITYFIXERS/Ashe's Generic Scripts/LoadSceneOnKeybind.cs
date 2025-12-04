//ASH+_GENERIC_SCRIPTS_#1

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnKeybind : MonoBehaviour
{
    //Keybinds to trigger
    public List<KeyCode> keybinds;

    //Scene to load
    public string sceneName;

    void Update()
    {
        foreach (KeyCode keybind in keybinds) //Check each specified keybind
        { 
            if (Input.GetKeyDown(keybind)) //If pressed
            {
                SceneManager.LoadScene(sceneName); //Load scene
            }
        }
    }
}
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DEBUGMonoBehaviour : MonoBehaviour
{
    //Debug Debugging!
    [OnValueChanged("DEBUGResetDebugOptions")]
    public bool ShowDebugOptions;

    [ShowIf("ShowDebugOptions")]
    [Button("Disable All Debugging!")]
    private void DEBUGDisableAll()
    {
        foreach (DEBUGMonoBehaviour debugScript in FindObjectsByType<DEBUGMonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            debugScript.DEBUGDisableThis(); // Example: disable debug on all

            debugScript.ShowDebugOptions = false;
        }
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (!scene.isLoaded) continue;

            foreach (GameObject root in scene.GetRootGameObjects())
            {
                foreach (DEBUGMonoBehaviour debugScript in root.GetComponentsInChildren<DEBUGMonoBehaviour>(true))
                {
                    debugScript.DEBUGDisableThis();

                    debugScript.ShowDebugOptions = false;
                }
            }
        }
    }

    [ShowIf("ShowDebugOptions")]
    [Button("Enable All Debugging!")]
    private void DEBUGEnableAll()
    {
        foreach (DEBUGMonoBehaviour debugScript in FindObjectsByType<DEBUGMonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None))
        {
            debugScript.DEBUGEnableThis(); // Example: disable debug on all
        }
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene scene = SceneManager.GetSceneAt(i);
            if (!scene.isLoaded) continue;

            foreach (GameObject root in scene.GetRootGameObjects())
            {
                foreach (DEBUGMonoBehaviour debugScript in root.GetComponentsInChildren<DEBUGMonoBehaviour>(true))
                {
                    debugScript.DEBUGEnableThis();
                }
            }
        }
    }

    [ShowIf("ShowDebugOptions")]
    [SerializeField]
    public bool debug;

    private float value;

    protected virtual void DEBUGResetDebugOptions()
    {
        debug = false;
    }

    private void DEBUGDisableThis()
    {
        debug = false;
        ShowDebugOptions = false;
    }

    private void DEBUGEnableThis()
    {
        debug = true;
        ShowDebugOptions = true;
    }
}
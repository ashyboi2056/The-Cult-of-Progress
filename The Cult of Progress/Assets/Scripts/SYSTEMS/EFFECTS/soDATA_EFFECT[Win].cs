using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Win Effect", menuName = "EFFECT/++Win")]
public class WinEffect : soDATA_EFFECT
{
    public override void Apply(GameObject target = null)
    {
        // Win
        string sourceScene = SceneManager.GetActiveScene().name;
        DATA_MasterSceneManager.ChangeToScene(sourceScene,"win");
    }
}
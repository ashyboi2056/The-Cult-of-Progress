using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New Win Effect", menuName = "EFFECT/++Win")]
public class WinEffect : soDATA_EFFECT
{
    public override void Apply(GameObject target = null)
    {
        // Win
        SceneManager.LoadScene("win");
    }
}
using UnityEngine;

[HelpURL("https://docs.unity.com/ScriptReference/Rigidbody.html")]
[RequireComponent(typeof(Rigidbody))] //Adds Component Automatically!!!
public class ExampleScript : MonoBehaviour
{
    [Header("Character Stats")]
    [SerializeField] 
    [Tooltip("The damage dealt by the character")] //Hover to Show
    [Range(0f, 5f)]
    private float damageDealt = 5;

    [Space(20)]

    [SerializeField] 
    [ContextMenuItem("Reset current health", "ResetCurrentHealth")] //Adds Menu to right click of below field with button to call The void with the second string name!
    private int currentHealth = 50;

    [HideInInspector] public int maxHealth = 100;

    [Header("Character Description")]
    [SerializeField] private string characterName = "Unity";

    [SerializeField] 
    [TextArea]
    private string characterDescription= "Unity Attributes";

    private void ResetCurrentHealth()
    {
        currentHealth = 100;
        Debug.Log("BYE!!! :£:3:3");
    }

    [ContextMenu("Goodbye")]
    private void Goodbye()
    {
        Debug.Log("Goodbye now! Hope it helped.");
    }
}
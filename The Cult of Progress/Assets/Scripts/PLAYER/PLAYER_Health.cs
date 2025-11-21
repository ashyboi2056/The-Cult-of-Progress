using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;

public class PLAYER_Health : DEBUGMonoBehaviour
{
    [SerializeField]
    private Dictionary<BodyPart, int> bodyPartHP = new Dictionary<BodyPart, int>()
    {
        { BodyPart.Head, RULES.RULE_HP_headMaxHP },
        { BodyPart.Body, RULES.RULE_HP_bodyMaxHP },
        { BodyPart.Arms, RULES.RULE_HP_armsMaxHP },
        { BodyPart.Legs, RULES.RULE_HP_legsMaxHP },
        { BodyPart.Soul, RULES.RULE_HP_soulMaxHP }
    };

    // Max HP reference for clamping
    private readonly Dictionary<BodyPart, int> maxHP = new Dictionary<BodyPart, int>()
    {
        { BodyPart.Head, RULES.RULE_HP_headMaxHP },
        { BodyPart.Body, RULES.RULE_HP_bodyMaxHP },
        { BodyPart.Arms, RULES.RULE_HP_armsMaxHP },
        { BodyPart.Legs, RULES.RULE_HP_legsMaxHP },
        { BodyPart.Soul, RULES.RULE_HP_soulMaxHP }
    };

    [SerializeField] private int Head;
    [SerializeField] private int Body;
    [SerializeField] private int Arms;
    [SerializeField] private int Legs;
    [SerializeField] private int Soul;

    [SerializeField] private TextMeshProUGUI headHPCounter;
    [SerializeField] private TextMeshProUGUI bodyHPCounter;
    [SerializeField] private TextMeshProUGUI armsHPCounter;
    [SerializeField] private TextMeshProUGUI legsHPCounter;
    [SerializeField] private TextMeshProUGUI soulHPCounter;

    // UI Update Event (can be hooked to UI system)
    public System.Action<BodyPart, int> OnHPChanged;

    // Heal All Button
    [Button("Heal All Body Parts!")]
    public void HealAll()
    {
        Heal(1000, BodyPart.All);
    }

    public void Heal(int amount, BodyPart target)
    {
        if (target == BodyPart.None)
        {
            if (debug) Debug.Log("No body part selected for Heal.");
            return;
        }
        if (target == BodyPart.All)
        {
            var parts = new List<BodyPart>(bodyPartHP.Keys);
            foreach (var part in parts)
            {
                ApplyHeal(part, amount);
            }
        }
        else
        {
            ApplyHeal(target, amount);
        }
    }

    private void ApplyHeal(BodyPart part, int amount)
    {
        bodyPartHP[part] = Mathf.Min(bodyPartHP[part] + amount, maxHP[part]);
        TriggerUIUpdate(part);
    }

    // Damage Method
    public void Damage(int amount, BodyPart target)
    {
        if (amount <= 0)
        {
            Debug.LogWarning("Damage amount must be positive.");
            return;
        }
        if (target == BodyPart.All)
        {
            var parts = new List<BodyPart>(bodyPartHP.Keys); // Copy keys
            foreach (var part in parts)
            {
                ApplyDamage(part, amount);
            }
        }
        else
        {
            ApplyDamage(target, amount);
        }
    }

    private void ApplyDamage(BodyPart part, int amount)
    {
        bodyPartHP[part] = Mathf.Max(bodyPartHP[part] - amount, 0);
        TriggerUIUpdate(part);
    }

    // UI Update Trigger
    private void TriggerUIUpdate(BodyPart part)
    {
        OnHPChanged?.Invoke(part, bodyPartHP[part]);
        UpdateUI();
        if (debug) Debug.Log($"Updated {part} HP: {bodyPartHP[part]}");
    }

    [Button]
    private void UpdateUI()
    {
        Head = bodyPartHP[BodyPart.Head];
        Body = bodyPartHP[BodyPart.Body];
        Arms = bodyPartHP[BodyPart.Arms];
        Legs = bodyPartHP[BodyPart.Legs];
        Soul = bodyPartHP[BodyPart.Soul];

        headHPCounter.text = "Head: " + bodyPartHP[BodyPart.Head].ToString() + "/" + maxHP[BodyPart.Head].ToString();
        bodyHPCounter.text = "Body: " + bodyPartHP[BodyPart.Body].ToString() + "/" + maxHP[BodyPart.Body].ToString();
        armsHPCounter.text = "Arms: " + bodyPartHP[BodyPart.Arms].ToString() + "/" + maxHP[BodyPart.Arms].ToString();
        legsHPCounter.text = "Legs: " + bodyPartHP[BodyPart.Legs].ToString() + "/" + maxHP[BodyPart.Legs].ToString();
        soulHPCounter.text = "Soul: " + bodyPartHP[BodyPart.Soul].ToString() + "/" + maxHP[BodyPart.Soul].ToString();
    }

    // Debug Tools
    [Space(20)]
    [SerializeField, ShowIf("debug")]
    private bool DEBUGShowDamageTests;

    [SerializeField, ShowIf(EConditionOperator.And, "debug", "DEBUGShowDamageTests")]
    private BodyPart DEBUGQueryTargetDamageTestPart = BodyPart.None;

    [SerializeField, ShowIf(EConditionOperator.And, "debug", "DEBUGShowDamageTests")]
    private int DEBUGQueryDamageAmount = 0;

    [ShowIf(EConditionOperator.And, "debug", "DEBUGShowDamageTests")]
    [Button("DEBUG_Damage!")]
    private void DEBUGDamage()
    {
        Damage(DEBUGQueryDamageAmount, DEBUGQueryTargetDamageTestPart);
    }
}

public enum BodyPart
{
    None,
    Head,
    Body,
    Arms,
    Legs,
    Soul,
    All
}

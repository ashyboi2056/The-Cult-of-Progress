using System.Collections.Generic;
using NaughtyAttributes;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;

public class UI_PLAYER_HealthDisplay : DEBUGMonoBehaviour
{
    private PLAYER_Health targetHealth;

    [SerializeField] Image head;
    [SerializeField] Image body;
    [SerializeField] Image armL;
    [SerializeField] Image armR;
    [SerializeField] Image legL;
    [SerializeField] Image legR;

    [Space(10)]

    [SerializeField] Color fullHealth;
    [SerializeField] Color damagedHealth;
    [SerializeField] Color oneHealth;
    [SerializeField] Color noHealth;

    void Awake()
    {
        targetHealth = FindFirstObjectByType<LOCAL_PLAYER_FLAG>().GetComponent<PLAYER_Health>();

        targetHealth.UpdateUI();
    }

    public void UpdateUI(Dictionary<BodyPart, int> bodyPartHP)
    {
        // Helper: get max HP for a given body part
        int GetMaxHP(BodyPart bodyPart)
        {
            switch (bodyPart)
            {
                case BodyPart.Head: return RULES.RULE_HP_headMaxHP;
                case BodyPart.Body: return RULES.RULE_HP_bodyMaxHP;
                case BodyPart.Arms: return RULES.RULE_HP_armsMaxHP;
                case BodyPart.Legs: return RULES.RULE_HP_legsMaxHP;
                default: return 999;
            }
        }

        // Helper: pick a color based on HP relative to max
        Color GetColor(int hp, BodyPart bodyPart)
        {
            int maxHP = GetMaxHP(bodyPart);

            if (hp <= 0) return noHealth;
            if (hp == 1) return oneHealth;
            if (hp < maxHP) return damagedHealth;
            return fullHealth;
        }

        // Update each body part image if present in dictionary
        if (bodyPartHP.TryGetValue(BodyPart.Head, out int headHP))
            head.color = GetColor(headHP, BodyPart.Head);

        if (bodyPartHP.TryGetValue(BodyPart.Body, out int bodyHP))
            body.color = GetColor(bodyHP, BodyPart.Body);

        if (bodyPartHP.TryGetValue(BodyPart.Arms, out int armLHP))
            armL.color = GetColor(armLHP, BodyPart.Arms);

        if (bodyPartHP.TryGetValue(BodyPart.Arms, out int armRHP))
            armR.color = GetColor(armRHP, BodyPart.Arms);

        if (bodyPartHP.TryGetValue(BodyPart.Arms, out int legLHP))
            legL.color = GetColor(legLHP, BodyPart.Arms);

        if (bodyPartHP.TryGetValue(BodyPart.Arms, out int legRHP))
            legR.color = GetColor(legRHP, BodyPart.Arms);
    }
}
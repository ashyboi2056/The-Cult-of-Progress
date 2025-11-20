using System;
using NaughtyAttributes;
using UnityEngine;

public class PLAYER_Health : DEBUGMonoBehaviour
{
    [SerializeField]
    [OnValueChanged("OnHeadDamage")]
    private int HP_head = RULES.RULE_HP_headMaxHP;
    [SerializeField]
    [OnValueChanged("OnBodyDamage")]
    private int HP_body = RULES.RULE_HP_bodyMaxHP;
    [SerializeField]
    [OnValueChanged("OnArmsDamage")]
    private int HP_arms = RULES.RULE_HP_armsMaxHP;
    [SerializeField]
    [OnValueChanged("OnLegsDamage")]
    private int HP_legs = RULES.RULE_HP_legsMaxHP;

    [Space(5)]

    [SerializeField]
    [OnValueChanged("OnSoulDamage")]
    private int HP_soul = RULES.RULE_HP_soulMaxHP;

    private void OnHeadDamage()
    {
        
    }

    private void OnBodyDamage()
    {
        
    }

    private void OnArmsDamage()
    {
        
    }

    private void OnLegsDamage()
    {
        
    }

    private void OnSoulDamage()
    {
        
    }

    [Button("Heal All Body Parts!")]
    public void HealAll()
    {
        Heal(1000, BodyPart.All);
    }

    public void Heal(int AMOUNT, BodyPart TARGET)
    {
        switch (TARGET)
        {
            case BodyPart.None:
                if (debug){ Debug.Log("No body part selected for Heal."); }
                break;



            case BodyPart.Head:
                if (HP_head + AMOUNT >= RULES.RULE_HP_headMaxHP) { HP_head = RULES.RULE_HP_headMaxHP; }
                else { HP_head += AMOUNT; }
                break;
            case BodyPart.Body:
                if (HP_body + AMOUNT >= RULES.RULE_HP_bodyMaxHP) { HP_body = RULES.RULE_HP_bodyMaxHP; }
                else { HP_body += AMOUNT; }
                break;
            case BodyPart.Arms:
                if (HP_arms + AMOUNT >= RULES.RULE_HP_armsMaxHP) { HP_arms = RULES.RULE_HP_armsMaxHP; }
                else { HP_arms += AMOUNT; }
                break;
            case BodyPart.Legs:
                if (HP_legs + AMOUNT >= RULES.RULE_HP_legsMaxHP) { HP_legs = RULES.RULE_HP_legsMaxHP; }
                else { HP_legs += AMOUNT; }
                break;

            case BodyPart.Soul:
                if (HP_soul + AMOUNT >= RULES.RULE_HP_soulMaxHP) { HP_soul = RULES.RULE_HP_soulMaxHP; }
                else { HP_soul += AMOUNT; }
                break;

            case BodyPart.All:
                //Heal Head
                if (HP_head + AMOUNT >= RULES.RULE_HP_headMaxHP) { HP_head = RULES.RULE_HP_headMaxHP; }
                else { HP_head += AMOUNT; }
                //Heal Body
                if (HP_body + AMOUNT >= RULES.RULE_HP_bodyMaxHP) { HP_body = RULES.RULE_HP_bodyMaxHP; }
                else { HP_body += AMOUNT; }
                //Heal Arms
                if (HP_arms + AMOUNT >= RULES.RULE_HP_armsMaxHP) { HP_arms = RULES.RULE_HP_armsMaxHP; }
                else { HP_arms += AMOUNT; }
                //Heal Legs
                if (HP_legs + AMOUNT >= RULES.RULE_HP_legsMaxHP) { HP_legs = RULES.RULE_HP_legsMaxHP; }
                else { HP_legs += AMOUNT; }

                //Heal Soul
                if (HP_soul + AMOUNT >= RULES.RULE_HP_soulMaxHP) { HP_soul = RULES.RULE_HP_soulMaxHP; }
                else { HP_soul += AMOUNT; }
                break;



            default:
                Debug.LogWarning("Unknown body part!");
                break;
        }
    }

    // DEBUG TOOLS //

    [Space(20)]
    [SerializeField]
    [ShowIf("debug")]
    [OnValueChanged("DEBUGResetDamageTest")]
    private bool DEBUGShowDamageTests;

    [SerializeField]
    [ShowIf(EConditionOperator.And, "debug", "DEBUGShowDamageTests")]
    private BodyPart DEBUGQueryTargetDamageTestPart = BodyPart.None;

    [SerializeField]
    [ShowIf(EConditionOperator.And, "debug", "DEBUGShowDamageTests")]
    private int DEBUGQueryDamageAmount = 0;

    [ShowIf(EConditionOperator.And, "debug", "DEBUGShowDamageTests")]
    [Button("DEBUG_Damage!")]
    private void DEBUGDamage()
    {
        if (DEBUGQueryDamageAmount == 0)
        {
            return;
        }
        if (DEBUGQueryDamageAmount < 0)
        {
            //Heal(Math.Abs(DEBUGQueryDamageAmount), DEBUGQueryTargetDamageTestPart);

            Debug.Log("Damage set to Negative integer!");

            return;
        }

        switch (DEBUGQueryTargetDamageTestPart)
        {
            case BodyPart.None:
                if (debug){ Debug.Log("No body part selected for Damage Test."); }
                break;



            case BodyPart.Head:
                if (HP_head - DEBUGQueryDamageAmount <= 0) { HP_head = 0; }
                else { HP_head -= DEBUGQueryDamageAmount; }
                break;
            case BodyPart.Body:
                if (HP_body - DEBUGQueryDamageAmount <= 0) { HP_body = 0; }
                else { HP_body -= DEBUGQueryDamageAmount; }
                break;
            case BodyPart.Arms:
                if (HP_arms - DEBUGQueryDamageAmount <= 0) { HP_arms = 0; }
                else { HP_arms -= DEBUGQueryDamageAmount; }
                break;
            case BodyPart.Legs:
                if (HP_legs - DEBUGQueryDamageAmount <= 0) { HP_legs = 0; }
                else { HP_legs -= DEBUGQueryDamageAmount; }
                break;

            case BodyPart.Soul:
                if (HP_soul - DEBUGQueryDamageAmount <= 0) { HP_soul = 0; }
                else { HP_soul -= DEBUGQueryDamageAmount; }
                break;

            case BodyPart.All:
                //Damage Head
                if (HP_head - DEBUGQueryDamageAmount <= 0) { HP_head = 0; }
                else { HP_head -= DEBUGQueryDamageAmount; }
                //Damage Body
                if (HP_body - DEBUGQueryDamageAmount <= 0) { HP_body = 0; }
                else { HP_body -= DEBUGQueryDamageAmount; }
                //Damage Arms
                if (HP_arms - DEBUGQueryDamageAmount <= 0) { HP_arms = 0; }
                else { HP_arms -= DEBUGQueryDamageAmount; }
                //Damage Legs
                if (HP_legs - DEBUGQueryDamageAmount <= 0) { HP_legs = 0; }
                else { HP_legs -= DEBUGQueryDamageAmount; }

                //Damage Soul
                if (HP_soul - DEBUGQueryDamageAmount <= 0) { HP_soul = 0; }
                else { HP_soul -= DEBUGQueryDamageAmount; }
                break;



            default:
                Debug.LogWarning("Unknown body part!");
                break;
        }
    }

    private void DEBUGResetDamageTest()
    {
        DEBUGQueryTargetDamageTestPart = BodyPart.None;
        DEBUGQueryDamageAmount = 0;
    }

    ////
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
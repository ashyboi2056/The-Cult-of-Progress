using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;

public class MAP_LadderPoint : DEBUGMonoBehaviour
{
    public static List<MAP_LadderPoint> allExitLadders = new List<MAP_LadderPoint>();

    [Tooltip("Is Ladder in Key Location?")]
    [OnValueChanged("OnExitLadderValueChanged")]
    [SerializeField]
    private bool isExitLadder;

    [HideIf("isExitLadder")]
    private int entranceLadderID;

    private void OnExitLadderValueChanged()
    {
        SetupLadder();
    }

    private void SetupLadder()
    {
        //Is Exit Ladder
        if (isExitLadder){ SetupExitLadder(); }

        //Is Entrance Ladder
        else
        { 
            allExitLadders.Remove(this); 
            SetupEntranceLadder();
        }
    }

    private void SetupEntranceLadder()
    {
        //Set Unique Ladder ID
        //DEPENDENT ON EVEN NUMBER OF EXITS AND ENTRANCES
        entranceLadderID = transform.GetSiblingIndex();
    }
    
    private void SetupExitLadder()
    {
        //Add to Array if not Present!
        if (!allExitLadders.Contains(this)){ allExitLadders.Add(this); }
        
        entranceLadderID = -1;
    }

    public MAP_LadderPoint FindMyEntranceLadder()
    {
        if (!isExitLadder){ return null; }

        foreach (MAP_LadderPoint ladderPoint in FindObjectsByType<MAP_LadderPoint>(FindObjectsSortMode.None))
        {
            if (ladderPoint.entranceLadderID == allExitLadders.IndexOf(this)){ return ladderPoint; }
        }

        return null;
    }

    private void Awake()
    {
        //Redundancy//
        SetupLadder();

        //DEBUG
        if (debug){ Debug.Log(name + "'s ID: " + entranceLadderID); }
    }

    public void TeleportPlayer(GameObject player)
    {
        Vector2 targetPosition;

        //FIND Target Postion
        if (isExitLadder){ targetPosition = FindMyEntranceLadder().transform.position; }
        else { targetPosition = allExitLadders[entranceLadderID].transform.position; }

        //MOVE Player
        player.transform.position = targetPosition;
    }
}
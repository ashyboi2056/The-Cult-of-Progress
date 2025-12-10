using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class MAP_BaseExitManager : DEBUGMonoBehaviour
{
    public MAP_LadderPoint BETA_BaseLadderPoint;

    [SerializeField] private List<Transform> unusedBaseExitPositions = new List<Transform>();
    [SerializeField] GameObject exitLadderPrefab;

    void Awake()
    {
        SpawnBaseExit(BETA_BaseLadderPoint);
    }

    public void SpawnBaseExit(MAP_LadderPoint entranceLadder)
    {
        int randomExitInt = Random.Range(0,unusedBaseExitPositions.Count());
        Transform chosenBaseExit = unusedBaseExitPositions[randomExitInt];

        GameObject newExitLadder = Instantiate(exitLadderPrefab,chosenBaseExit);

        entranceLadder.SetupLinkedLadder(newExitLadder);
        newExitLadder.GetComponent<MAP_LadderPoint>().SetupLinkedLadder(entranceLadder.gameObject);

        unusedBaseExitPositions.RemoveAt(randomExitInt);
    }
}
using System.Linq;
using UnityEngine;

public class MAP_LadderPointSpawn : DEBUGMonoBehaviour
{
    static int targetSpawns = RULES.RULE_MAP_locationCount;

    static MAP_LadderPointSpawn[] allSpawns;

    static bool setupRun = false;

    public GameObject LPEntrancePrefab;
    public Transform LPEntranceParent;

    void Awake()
    {
        if (setupRun){ return; }

        allSpawns = FindObjectsByType<MAP_LadderPointSpawn>(FindObjectsSortMode.None);

        if (debug){ Debug.Log("Running Ladder Spawn!"); }

        for (int i = targetSpawns; i != 0; i--)
        {
            if (debug){ Debug.Log("Spawning!"); }
            allSpawns[Random.Range(0,allSpawns.Count()-1)].SpawnLadder();
        }

        setupRun = true;
    }

    void SpawnLadder()
    {
        if (targetSpawns != 0)
        {
            if (debug){ Debug.Log("Instantiating!"); }
            GameObject newLadder = Instantiate(LPEntrancePrefab,LPEntranceParent);

            newLadder.transform.position = transform.position;
        }
    }
}
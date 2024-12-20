using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public List<GameObject> TrapTypeList;
    // Start is called before the first frame update
    private void Start()
    {
        RoadBlockObjectPool.Instance.trapManagerCaller += OnRoadBlockSpawn;
    }

    private void OnRoadBlockSpawn(GameObject RoadPiece)
    {
        Transform SpawnPos = RoadPiece.GetComponent<RoadPiece>().TrapPos;
        if (CheckSpawnCondition())
        {
            SpawnTrap(SpawnPos);
        }
        
    }

    private bool CheckSpawnCondition()
    {
        int ranValue = UnityEngine.Random.Range(0, 100);
        return ranValue < 37;
    }

    private void SpawnTrap(Transform Pos)
    {
        if (TrapTypeList.Count == 0)
            return;

        int index = UnityEngine.Random.Range(0, TrapTypeList.Count - 1);
        Instantiate(TrapTypeList[index],Pos);
    }
}

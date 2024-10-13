using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class RoadBlockObjectPool : ObjectPool
{
    private Player player;
    private static RoadBlockObjectPool instance;
    public static RoadBlockObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("RoadBlockObjectPool").AddComponent<RoadBlockObjectPool>();
                instance.SetPrefab("Prefabs/RoadPiece");
                instance.player = FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    public override void Activate(GameObject gameObject)
    {
        player.SubscribeSwerve(gameObject.transform);
        
    }
    public override void Deactivate(GameObject gameObject)
    {
        player.UnsubscribeSwerve(gameObject.transform);
        Hide(gameObject);
    }

}
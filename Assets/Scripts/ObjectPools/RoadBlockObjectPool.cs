using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class RoadBlockObjectPool : ObjectPool
{
    private Player player;

    public delegate void TrapManagerCaller(GameObject gameObject);
    public TrapManagerCaller trapManagerCaller;
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
        base.Activate(gameObject);
        SwerveManager.instance.SubscribeSwerve(gameObject.transform);
        trapManagerCaller?.Invoke(gameObject);

    }
    public override void Deactivate(GameObject gameObject)
    {
        base.Deactivate(gameObject);

        SwerveManager.instance.UnsubscribeSwerve(gameObject.transform);
    }
    

}
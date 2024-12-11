using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUI : MonoBehaviour
{
    [SerializeField] Transform SpeedBar;

    Player player;

    float MaxSpeed;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        MaxSpeed = player.InitDataSO.SpeedLimit;
    }
    private void Update()
    {
        SpeedBar.localScale = new Vector3(player.Speed / MaxSpeed, 1, 1);
    }
}

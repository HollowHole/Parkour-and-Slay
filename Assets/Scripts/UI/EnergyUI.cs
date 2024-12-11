using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] GameObject energyPrefab;

    private void Start()
    {
        CardManager cardManager = CardManager.Instance;

        cardManager.OnEnergyChange += OnEnergyChange;

        OnEnergyChange(cardManager.EnergyCnt);
    }
    void OnEnergyChange(int energyCnt)
    {
        int diff = energyCnt - transform.childCount;
        while (diff < 0)
        {
            Destroy(transform.GetChild(0).gameObject);
            diff++;
        }
        while(diff > 0)
        {
            diff--;
            Instantiate(energyPrefab, transform);
        }
    }
}

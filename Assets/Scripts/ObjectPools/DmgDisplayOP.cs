using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class DmgDisplayOP : ObjectPool
{
    

    private static DmgDisplayOP instance;
    public static DmgDisplayOP Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("DmgDisplayOP").AddComponent<DmgDisplayOP>();
                instance.SetPrefab("Prefabs/DmgDisplayer");
            }
            return instance;
        }
    }
    public void ShowDmgAt(float dmg,Vector3 pos)
    {
        GameObject go = Spawn(pos,Quaternion.identity);

        go.GetComponentInChildren<TextMeshProUGUI>().text = Mathf.Round(dmg).ToString();
    }
    public override void Activate(GameObject gameObject)
    {
        base.Activate(gameObject);
        StartCoroutine(RiseAndFade(gameObject));
    }
    IEnumerator RiseAndFade(GameObject go)
    {
        float FadeTime = Global.DmgDisplayerFadeTime;
        float RiseDis = Global.DmgDisplayerRiseDistance;
        float timer = FadeTime;
        while (timer>0)
        {
            timer -= Time.deltaTime;
            go.transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one/3, 1- timer/FadeTime);
            Vector3 TargetPos = go.transform.position;
            TargetPos.y += Time.deltaTime / FadeTime * RiseDis;
            go.transform.position = TargetPos;
            yield return null;
        }
        Despawn(go);
    }
}

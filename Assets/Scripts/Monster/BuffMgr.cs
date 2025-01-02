using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BuffMgr : MonoBehaviour
{
    private List<Buff> BuffList = new List<Buff>();

    public void AddBuff(Buff buff)
    {
        Buff[] sameBuff = BuffList.Where((_buff) => { return buff.UISprite == _buff.UISprite; }).ToArray();
        if(sameBuff.Length > 0 )
        {
            sameBuff.First().Finish();
        }
        BuffList.Add(buff);

        //Debug.Log("buff added!");

        buff.Init(transform);
        ICanShowBuffUI i = GetComponent<ICanShowBuffUI>();
        if(i != null )
        {
            buff.myUI=i.ShowThisUI(buff.UISprite);
        }
    }
    public void HandleBuffEffect()
    {
        for (int i = BuffList.Count - 1; i >= 0; i--)
        {
            
            BuffList[i].Update();
            if (BuffList[i].isOver)
            {
                BuffList.RemoveAt(i);
            }
           

        }
    }

    public void ClearAllBuff()
    {
        foreach(Buff buff in BuffList)
        {
            buff.Finish();
        }
    }
}
//public class Buff
//{
//    public float LastTime;
//    public bool isOver => LastTime <= 0;

//    public Action CountDown;
//    public Action<Transform> HandleInitEffect;

//    public Action<Transform> HandleLastingEffect;

//    public Action<Transform> HandleFinishEffect;
//    public Buff(float _lastTime = 1f)
//    {
//        LastTime = _lastTime;
//        CountDown += () => { LastTime -= Time.deltaTime; };
//        HandleLastingEffect += (_) => { CountDown?.Invoke(); };
//    }
    

//}
public abstract class Buff
{
    private float LastTime;
    public Sprite UISprite;
    Transform target;
    public GameObject myUI;
    public bool isOver => LastTime <= 0;
    public bool isPersistant = false;
    public Buff(Sprite uiSprite)
    {
        UISprite = uiSprite;
        LastTime = 1;
        isPersistant = true;
    }
    public Buff(Sprite uiSprite, float lastTime)
    {
        LastTime = lastTime;
        UISprite = uiSprite;    
    }
    public virtual void CountDown()
    {
        if(!isPersistant)
        LastTime -= Time.deltaTime;
    }
    public void Init(Transform _target)
    {
        target = _target;
        
        HandleInitEffect(target);
    }
    public void Update()
    {
        CountDown();
        if (isOver)
        {
            HandleFinishEffect(target);
            return;
        }
        HandleLastingEffect(target);
        
        
    }
    protected virtual void HandleInitEffect(Transform target) { }

    protected virtual void HandleLastingEffect(Transform target) { }


    protected virtual void HandleFinishEffect(Transform target) {
        if(myUI != null)
        {
            BuffUIZone.HelpDestroy(myUI);
        }
    }
    public void Finish()
    {
        LastTime = 0;
    }


}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffMgr : MonoBehaviour
{
    private List<Buff> BuffList = new List<Buff>();

    public void AddBuff(Buff buff)
    {
        BuffList.Add(buff);
        buff.Init(transform);
    }
    public void HandleBuffEffect()
    {
        for (int i = BuffList.Count - 1; i >= 0; i--)
        {
            
            BuffList[i].Update(transform);
            if (BuffList[i].isOver)
            {
                BuffList.RemoveAt(i);
            }
           

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
    public bool isOver => LastTime <= 0;

    public Buff(float lastTime = 1f)
    {
        LastTime = lastTime;
    }
    public virtual void CountDown()
    {
        LastTime -= Time.deltaTime;
    }
    public void Init(Transform target)
    {
        HandleInitEffect(target);
    }
    public void Update(Transform target)
    {
        if (isOver)
        {
            HandleFinishEffect(target);
            return;
        }
        HandleLastingEffect(target);
        CountDown();
        
    }
    protected virtual void HandleInitEffect(Transform target) { }

    protected virtual void HandleLastingEffect(Transform target) { }


    protected virtual void HandleFinishEffect(Transform target) { }
    protected void Finish()
    {
        LastTime = 0;
    }


}


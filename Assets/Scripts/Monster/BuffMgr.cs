using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

public class BuffMgr : MonoBehaviour
{
    private List<Buff> BuffList = new List<Buff>();

    public void AddBuff(Buff buff)
    {
        Buff[] sameBuff = BuffList.Where((_buff) => { return buff.BuffName == _buff.BuffName; }).ToArray();
        if(sameBuff.Length > 0 )
        {
            Debug.Log("override " + sameBuff.First().BuffName);
            sameBuff.First().Finish();
        }
        BuffList.Add(buff);

        //Debug.Log("buff added!");
        ICanShowBuffUI i = GetComponent<ICanShowBuffUI>();
        if(i != null )
        {
            buff.myUI=i.ShowThisUI(buff.UISprite,buff.BuffName);
        }
        buff.Init(transform);
    }
    public void HandleBuffEffect()
    {
        //StringBuilder allBuff =new StringBuilder();
        for (int i = BuffList.Count - 1; i >= 0; i--)
        {
            //allBuff.Append(BuffList[i].BuffName + " ");

            BuffList[i].Update();
            if (BuffList[i].isOver)
            {
                //Debug.Log(BuffList[i].BuffName + "finished!");
                BuffList.RemoveAt(i);
            }

        }
        //Debug.Log(allBuff.ToString());
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
    public String BuffName;
    Transform target;
    public BuffUI myUI;
    public bool isOver => LastTime <= 0;
    /// <summary>
    /// 只在被强制结束才会消失的Buff
    /// </summary>
    public bool isPersistant = false;
    public Buff(string buffname,Sprite uiSprite)
    {
        BuffName = buffname;
        UISprite = uiSprite;
        LastTime = 1;
        isPersistant = true;
    }
    public Buff(string buffname,Sprite uiSprite, float lastTime)
    {
        BuffName = buffname;

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

        UpdateBuffUI();
    }

    protected virtual void UpdateBuffUI()
    {
        if (myUI != null && !isPersistant)
        {
            myUI.lastTime = LastTime.ToString() + "s";
        }
    }

    protected virtual void HandleInitEffect(Transform target) { }

    protected virtual void HandleLastingEffect(Transform target) { }


    protected virtual void HandleFinishEffect(Transform target) {
        if(myUI != null)
        {
            BuffUIZone.HelpDestroy(myUI.gameObject);
        }
    }
    public void Finish()
    {
        LastTime = 0;
    }


}


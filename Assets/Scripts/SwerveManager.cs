using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveManager : MonoBehaviour
{
    [SerializeField] private List<Transform> Objects2Swerve = new List<Transform>();

    private Global.SwerveDirection _swerveDirection;

    public Action OnSwerveBegin;
    public Action OnSwerveEnd;

    public Action OnSwerveZonePassed;

    public static SwerveManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SubscribeSwerve(Transform tf)
    {
        Objects2Swerve.Add(tf);
    }
    public void UnsubscribeSwerve(Transform tf)
    {
        Objects2Swerve.Remove(tf);
    }
    public void Swerve(Global.SwerveDirection swerveDirection, Vector3 swerveCenter)
    {

        //启动携程
        _swerveDirection = swerveDirection;

        StartCoroutine(SwerveAllObjects(swerveDirection, swerveCenter));

    }
    private IEnumerator SwerveAllObjects(Global.SwerveDirection swerveDirection, Vector3 swerveCenter)
    {
        Debug.Log("Swerve begin!");
        OnSwerveBegin?.Invoke();

        Debug.Log(swerveCenter);

        float swerveAngleLeft = 90;
        Vector3 axis = transform.up;
        while (swerveAngleLeft > 0)
        {
            float swerveAngle = 500 * Time.deltaTime;
            swerveAngle = swerveAngle < swerveAngleLeft ? swerveAngle : swerveAngleLeft;
            swerveAngleLeft -= swerveAngle;
            if (swerveDirection == Global.SwerveDirection.RIGHT) swerveAngle = -swerveAngle;

            foreach (Transform t in Objects2Swerve)
            {
                t.RotateAround(swerveCenter, axis, swerveAngle);
            }

            yield return null;

        }
        /*bool breakFlag = false;
        while (!breakFlag)
        {
            Vector3 offset = Vector3.zero;
            foreach(Transform t in Objects2Swerve)
            {
                Vector3 pos = t.position;

                pos.x *=  (1 - Time.deltaTime);
                if (Mathf.Abs(t.position.x) < 0.15f)
                {
                    pos.x = 0;
                    breakFlag = true;
                }
                pos.x = 0;
                breakFlag = true;

                offset = t.position - pos;
                t.position = pos;
            }
            //玩家的位置也要修改
            transform.position -= offset;
            yield return null;
        }*/

        OnSwerveEnd?.Invoke();

        OnSwerveZonePassed?.Invoke();

    }
}

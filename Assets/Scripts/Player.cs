using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    public float Speed;
    public float LRMoveSpeed;
    private Rigidbody rb;
    [SerializeField]private List<Transform> Objects2Swerve = new List<Transform>();
    Camera cam;
    private Global.SwerveDirection _swerveDirection;
    private void Start()
    {
        cam=FindObjectOfType<Camera>();
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        float vx = 0;
        vx = Input.GetAxis("Horizontal") * LRMoveSpeed;
        rb.velocity = new Vector3(vx, 0, 0);

    }
    public void SubscribeSwerve(Transform tf)
    {
        Objects2Swerve.Add(tf);
    }
    public void UnsubscribeSwerve(Transform tf)
    {
        Objects2Swerve.Remove(tf);
    }
    public void Swerve(Global.SwerveDirection swerveDirection)
    {
        
        //启动携程
        _swerveDirection = swerveDirection;
        
        StartCoroutine(SwerveAllObjects(swerveDirection));
        
    }
    private IEnumerator SwerveAllObjects(Global.SwerveDirection swerveDirection)
    {
        Debug.Log("Swerve begin!");
        float swerveAngleLeft = 90;
        Vector3 axis = transform.up;
        while (swerveAngleLeft>0)
        {
            float swerveAngle = 10 * 10 * Time.deltaTime;
            swerveAngleLeft -= swerveAngle;
            if (swerveDirection == Global.SwerveDirection.RIGHT) swerveAngle = -swerveAngle;

            foreach (Transform t in Objects2Swerve)
            {
                t.RotateAround(transform.position,axis, swerveAngle);
            }
            
            yield return null;
            
        }
        bool breakFlag = false;
        while (!breakFlag)
        {
            Vector3 offset = Vector3.zero;
            foreach(Transform t in Objects2Swerve)
            {
                Vector3 pos = t.position;

                /*pos.x *=  (1 - Time.deltaTime);
                if (Mathf.Abs(t.position.x) < 0.15f)
                {
                    pos.x = 0;
                    breakFlag = true;
                }*/
                pos.x = 0;
                breakFlag = true;

                offset = t.position - pos;
                t.position = pos;
            }
            //玩家的位置也要修改
            transform.position -= offset;
            

            yield return null;
        }
        
        FindObjectOfType<RoadSpawn>().EnableSpawn();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwerveZone : MonoBehaviour
{
    private Global.SwerveDirection _swerveDirection;
    public void SetSwerveDirection(Global.SwerveDirection swerveDirection)
    {
        _swerveDirection = swerveDirection;
    }
    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            bool moveLeft = Input.GetAxis("Horizontal") < 0;
            bool moveRight = Input.GetAxis("Horizontal") > 0;
            //bool moveRight = !moveLeft;


            if (moveLeft && _swerveDirection == Global.SwerveDirection.LEFT)
            {
                //Vector3 swerveCenter = transform.parent.position;
                Vector3 swerveCenter = transform.parent.position - new Vector3(4, 0, 3);
                Debug.Log(swerveCenter);
                //swerveCenter.x = 0;
                SwerveManager.instance.Swerve(_swerveDirection,swerveCenter);
                Hide();

            }
            else if(moveRight && _swerveDirection == Global.SwerveDirection.RIGHT)
            {
                //Vector3 swerveCenter = transform.parent.position;
                Vector3 swerveCenter = transform.parent.position - new Vector3(-4, 0, 3);
                Debug.Log(swerveCenter);
                //swerveCenter.x = 0;

                SwerveManager.instance.Swerve(_swerveDirection, swerveCenter);
                Hide() ;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Hide();

            SwerveManager.instance.OnSwerveZonePassed?.Invoke();
        }
    }
    public void Hide()
    {
        transform.SetParent(SwerveManager.instance.transform,true);
        transform.position = SwerveManager.instance.transform.position;
    }
}

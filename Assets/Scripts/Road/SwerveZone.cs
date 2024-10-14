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


            if (moveLeft && _swerveDirection == Global.SwerveDirection.LEFT)
            {
                Vector3 swerveCenter = transform.parent.position;
                Debug.Log(swerveCenter);
                swerveCenter.x = 0;
                other.GetComponent<Player>().Swerve(_swerveDirection,swerveCenter);
                Destroy(gameObject);

            }
            else if(moveRight && _swerveDirection == Global.SwerveDirection.RIGHT)
            {
                Vector3 swerveCenter = transform.parent.position;
                Debug.Log(swerveCenter);
                swerveCenter.x = 0;

                other.GetComponent<Player>().Swerve(_swerveDirection, swerveCenter);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}

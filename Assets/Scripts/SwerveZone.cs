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
            if (moveLeft && _swerveDirection == Global.SwerveDirection.LEFT)
            {
                other.GetComponent<Player>().Swerve(_swerveDirection);
                Destroy(gameObject);

            }
            else if(!moveLeft && _swerveDirection == Global.SwerveDirection.RIGHT)
            {
                other.GetComponent<Player>().Swerve(_swerveDirection);
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

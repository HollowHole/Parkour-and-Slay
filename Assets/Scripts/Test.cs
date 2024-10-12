using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update

    void Update()
    {
        Vector3 vector3 = transform.position;
        vector3.y += 0.02f;
        transform.position = vector3;
    }
}

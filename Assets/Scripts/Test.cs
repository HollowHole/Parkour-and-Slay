using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    HighlightableObject h;
    private void Start()
    {
        h = GetComponent<HighlightableObject>();
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            h.ConstantOnImmediate(Color.cyan);
        }
        
    }
}

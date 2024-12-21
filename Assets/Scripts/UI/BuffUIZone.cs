using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffUIZone : MonoBehaviour
{
    public static BuffUIZone Instance;
    //List<string> BuffIDList = new List<string>();
    [SerializeField]GameObject BuffUIProto;
    private void Awake()
    {
        Instance = this;
        
    }
    public GameObject AddBuffUI(Sprite sprite)
    {
        GameObject ui = Instantiate(BuffUIProto, transform);
        ui.GetComponent<Image>().sprite = sprite;
        //BuffIDList.Add(ID);
        return ui;
    }
    public static void HelpDestroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }




}

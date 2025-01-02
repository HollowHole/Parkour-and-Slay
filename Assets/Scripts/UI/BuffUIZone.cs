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
    public BuffUI AddBuffUI(Sprite sprite, string Buffname)
    {
        GameObject uiGo = Instantiate(BuffUIProto, transform);
        BuffUI buffUI = uiGo.GetComponent<BuffUI>();

        buffUI.GetComponent<Image>().sprite = sprite;
        buffUI.SetBuffName(Buffname);
        //BuffIDList.Add(ID);
        return buffUI;
    }
    public static void HelpDestroy(GameObject gameObject)
    {
        Destroy(gameObject);
    }




}

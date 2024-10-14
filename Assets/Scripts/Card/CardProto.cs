using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class CardProto : MonoBehaviour
{
    public RectTransform CDGrey;
    private RectTransform myRectTransform;
    public float coolDownTime = 3f;
    [SerializeField]private float coolDownTimer = 0;//0��ʾ��ȴ���
    private void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
    }
    private void FixedUpdate()
    {
        if( coolDownTimer >0)
        {
            coolDownTimer -= Time.deltaTime;
        }
    }
    public void OnClick()
    {
        if (CDReady())
        {
            CardManager.Instance.Choose(this);
        }
    }
    public void OnPick()
    {
        //�޸�Scale��position
        Vector3 targetScale = new Vector3(1.3f, 1.3f, 1.3f);
        myRectTransform.localScale = targetScale;
        
    }
    public void Unpick()
    {
        //Scale��position�Ļ�ȥ
        myRectTransform.localScale = new Vector3(1,1,1);
    }
    public void OnUse()
    {
        CDStart();
    }
    private void CDStart()
    {
        coolDownTimer = coolDownTime;
    }
    private bool CDReady()
    {
        return coolDownTimer <=0;
    }
    private void Update()
    {
        //����CDʱ��ɫ���ֵĴ�С

        float height = coolDownTimer / coolDownTime * myRectTransform.rect.height;
        float width = CDGrey.rect.width;
        Vector2 targetSize = new Vector2(width,height);
        CDGrey.sizeDelta = targetSize;
        Debug.Log(CDGrey.rect);
    }
}

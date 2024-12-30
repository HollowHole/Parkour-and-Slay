using UnityEngine;
public class CardFlash : CardProto
{
    CardFlashSO m_cardSO;
    protected override void Awake()
    {
        m_cardSO = cardSO as CardFlashSO;
        base.Awake(); 
    }
    public override void OnUse()
    {
        base.OnUse();
        Vector3 movement =  Player.Instance.GetComponent<Rigidbody>().velocity.normalized * m_cardSO.FlashDistance;

        Vector3 targetPos = Player.Instance.transform.position + movement;
        targetPos.y = Mathf.Clamp(targetPos.y,0,targetPos.y);
        targetPos.x = Mathf.Clamp(targetPos.x, -3, 3);

        Player.Instance.transform.position = targetPos;
    }
}

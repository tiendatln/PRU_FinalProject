using UnityEngine;

public class TurnOnAttackMap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private GameObject attackMap;
    private void Awake()
    {
        attackMap = GameObject.FindWithTag("BossAttackMap5");
        attackMap.SetActive(false);
    }


    public void AttackMapDownOn()
    {
        attackMap.SetActive(true);
        Invoke("AttackMapDownOff", 2f);
    }

    public void AttackMapDownOff()
    {
        attackMap.SetActive(false);
    }

}

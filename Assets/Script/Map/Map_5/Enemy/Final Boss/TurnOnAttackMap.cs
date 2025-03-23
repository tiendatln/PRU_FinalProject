using UnityEngine;

public class TurnOnAttackMap : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public AudioClip FireAttack;
    private GameObject attackMap;
    private void Awake()
    {
        attackMap = GameObject.FindWithTag("BossAttackMap5");
        attackMap.SetActive(false);
    }


    public void AttackMapDownOn()
    {
        attackMap.SetActive(true);
        AudioManager.Instance.playSFXSound(FireAttack);
        Invoke("AttackMapDownOff", 0.5f);
    }

    public void AttackMapDownOff()
    {
        attackMap.SetActive(false);
    }

}

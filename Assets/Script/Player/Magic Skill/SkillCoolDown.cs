using UnityEngine;
using UnityEngine.UI;

public class SkillCoolDown : MonoBehaviour
{

    public Image CoolDownMagic;
    public Image CoolDownArrow;
    public GameObject player;
    private PlayerController playerController;
    //private bool isCoolDown =false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController = GameObject.Find("Character").gameObject.GetComponent<PlayerController>();
        CoolDownMagic.fillAmount = playerController.PlayerSpawnMagicSkill.MagicCoolDownSkill;
        CoolDownArrow.fillAmount = playerController.PlayerSpawnMagicSkill.ArrowCoolDownSkill;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(playerController.PlayerSpawnMagicSkill.MagicCoolDownSkill);
        if (playerController.PlayerSpawnMagicSkill.MagicCoolDownSkill >= 0)
        {
            CoolDownMagic.fillAmount = playerController.PlayerSpawnMagicSkill.MagicCoolDownSkill / playerController.PlayerSpawnMagicSkill.SetTimeMagicSkill ;
        }
        if (playerController.PlayerSpawnMagicSkill.ArrowCoolDownSkill >= 0)
        {
            CoolDownArrow.fillAmount = playerController.PlayerSpawnMagicSkill.ArrowCoolDownSkill / playerController.PlayerSpawnMagicSkill.SetTimeArrowSkill;
        }
    }
}

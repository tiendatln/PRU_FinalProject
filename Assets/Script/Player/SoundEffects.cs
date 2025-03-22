using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Attack3;
    public AudioClip BowShoot;
    public AudioClip MagicSkill;
    public AudioClip FootStep;


    public void playAttack1Sound()
    {
        AudioManager.Instance.playSFXSound(Attack1);
    }

    public void playAttack2Sound()
    {
        AudioManager.Instance.playSFXSound(Attack2);
    }

    public void playAttack3Sound()
    {
        AudioManager.Instance.playSFXSound(Attack3);
    }

    public void playBowShootSound()
    {
        AudioManager.Instance.playSFXSound(BowShoot);
    }

    public void playMagicSkill()
    {
        AudioManager.Instance.playSFXSound(MagicSkill);
    }

    public void playFootStepSound()
    {
        AudioManager.Instance.playSFXSound(FootStep);
    }

    public void playJumpSound()
    {

    }
}

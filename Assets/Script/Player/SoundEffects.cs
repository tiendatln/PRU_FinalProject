using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip Attack1;
    public AudioClip Attack2;
    public AudioClip Attack3;
    public AudioClip BowShoot;
    public AudioClip MagicSkill;
    public AudioClip FootStep;

    public AudioSource AudioSource;

    public void playAttack1Sound()
    {
        AudioSource.clip = Attack1;
        AudioSource.Play();
    }

    public void playAttack2Sound()
    {
        AudioSource.clip = Attack2;
        AudioSource.Play();
    }

    public void playAttack3Sound()
    {
        AudioSource.clip = Attack3;
        AudioSource.Play();
    }

    public void playBowShootSound()
    {
        AudioSource.clip = BowShoot;
        AudioSource.Play();
    }

    public void playMagicSkill()
    {
        AudioSource.clip = MagicSkill;
        AudioSource.Play();
    }

    public void playFootStepSound()
    {
        AudioSource.clip = FootStep;
        AudioSource.Play();
    }
}

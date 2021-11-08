using UnityEngine;

public class SFX : MonoBehaviour
{   
    // Audio for certain player/enemy actions
    [SerializeField] AudioSource playerSwing;
    [SerializeField] AudioSource enemySwing;
    [SerializeField] AudioSource throwKunai;
    [SerializeField] AudioSource drinkPotion;
    [SerializeField] AudioSource run;
    [SerializeField] AudioSource jump;
    [SerializeField] AudioSource playerGrunt;
    [SerializeField] AudioSource enemyGrunt;
    [SerializeField] AudioSource slide;

    
    public void PlayPlayerSlashSFX() {
        playerSwing.Play();
    }

    public void PlayEnemySlashSFX() {
        enemySwing.Play();
    }

    public void PlayThrowKunaiSFX() {
        throwKunai.Play();
    }

    public void PlayDrinkPotionSFX() {
        drinkPotion.Play();
    }

    public void PlayRunSFX() {
        run.Play();
    }

    public void PlayJumpSFX() {
        jump.Play();
    }

    public void PlayPlayerGruntSFX() {
        playerGrunt.Play();
    }

    public void PlayEnemyGruntSFX() {
        enemyGrunt.time = 0.350f;
        enemyGrunt.Play();
    }

    public void PlaySlideSFX() {
        slide.Play();
    }

}

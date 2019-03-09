using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerHealth : MonoBehaviour{

    public static int currentHealth = 300;                                 // The current health the player has.
    public Slider healthSlider;                            // Reference to the UI's health bar.
    public Image damageImage;                        // Reference to an image to flash on the screen on being hurt.
    public AudioClip deathClip;                                 // The audio clip to play when the player dies.
    public float flashSpeed = 5f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The color the damageImage is set to, to flash.
    public static bool damaged;                                                   // True when the player gets damaged.
    public static bool healed;

    Animator anim;                                                  // Reference to the Animator component.
    AudioSource playerAudio;                                    // Reference to the AudioSource component.
    PlayerMovement playerMovement;                         // Reference to the player's movement.

    // PlayerShooting playerShooting;                           // Reference to the PlayerShooting script.
    private bool isDead;                                                      // Whether the player is dead.
    private int timeUpdate = 0;

    public int MyInt; 
    private float timer;

    public GameObject ItemImage;
    public GameObject dieimage;

    public void Awake()
    {
        // Setting up the references.
        anim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
     //   playerMovement = GetComponent<PlayerMovement>();
        // playerShooting = GetComponentInChildren<PlayerShooting>();

        // Set the initial health of the player.
        healthSlider.value = currentHealth;

        damaged = false;
        healed = false;
        dieimage.SetActive(false);
        isDead = false;
    }

   
   public void Update(){
     
        timer += Time.deltaTime;
        if (timer > 1f && currentHealth >= 0)
        {
            currentHealth -= 1;
            healthSlider.value = currentHealth;
            timer = 0f;
        }

        if (currentHealth <= 0 && !isDead){
            // ... it should die.
            Death();
        }

        //healing
        if(healed){
            ItemImage.gameObject.SetActive(true);
            Invoke("Remove_Function", 2.5f);

            if (timer > 0.1f)
            {
                if(currentHealth + 30 > 300){
                    currentHealth = 300;
                }
                else{
                    currentHealth += 30;
                }

                healthSlider.value = currentHealth;
                timer = 0f;

                healed = false;
            }

                    }
        // If the player has just been damaged...
        if (damaged){
            
            timer += Time.deltaTime;
            if (timer > 0.1f)
            {
                currentHealth -= 10;
                healthSlider.value = currentHealth;
                timer = 0f;
            }

            // Play the hurt sound effect.
            playerAudio.Play();

            // If the player has lost all it's health and the death flag hasn't been set yet...
            if (healthSlider.value <= 0 && !isDead)
            {
                // ... it should die.
                Death();
            }

            // ... set the color of the damageImage to the flash color.
            damageImage.color = flashColour;
        }
        else{
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

    }

    //public void TakeDamage()
    //{
    //    timer2 += Time.deltaTime;
    //    if (timer2 > 1f)
    //    {
    //        currentHealth -= 10;
    //        healthSlider.value = currentHealth;
    //        timer2 = 0f;
    //    }

    //    // Play the hurt sound effect.
    //    playerAudio.Play();

    //    // If the player has lost all it's health and the death flag hasn't been set yet...
    //    if (healthSlider.value <= 0 && !isDead)
    //    {
    //        // ... it should die.
    //        Death();
    //    }
    //}

    void Death()
    {
        // Set the death flag so this function won't be called again.
        isDead = true;
        dieimage.SetActive(true);
        Time.timeScale = 0.0F;

        // Turn off any remaining shooting effects.
        // playerShooting.DisableEffects();

        // Tell the animator that the player is dead.
        //  anim.SetTrigger("Die");

        // Set the audiosource to play the death clip and play it (this will stop the hurt sound from playing).
        //playerAudio.clip = deathClip;
        //playerAudio.Play();

        dieimage.SetActive(true);

        // Turn off the movement and shooting scripts.
       // playerMovement.enabled = false;
        // playerShooting.enabled = false;
    }

    public void RestartLevel ()
    {
        SceneManager.LoadScene (0);
    }

    private void Remove_Function()
    {
        ItemImage.gameObject.SetActive(false);
    }
}

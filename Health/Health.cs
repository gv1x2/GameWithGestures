using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }

    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;  //invulnerabilitate
    [SerializeField] private int numberOffFlashes;
    private SpriteRenderer spriteRend; //change color of player when invurnerable

    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent <Animator>();
        spriteRend = GetComponent <SpriteRenderer>();
    }

    
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth); //scadem numarul de vieti

        if (currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            //iframe
            StartCoroutine(Invunerability());
        }
        else
        {

            //Player death
            if (!dead)
            {
                anim.SetTrigger("die");
                
                //Deactivate all attached components classes
                foreach (Behaviour component in components)
                    component.enabled = false;
                
                dead = true;  
            }
        }
    }
    public void AddHealth(float _value) 
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth); //crestem numarul de inimi
    }

    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(10, 11, true); //layer 10 PLayer si Layer 11 Enemy,true coliziunile vor fi ignorate
        //invunerabity duration
        for (int i = 0; i < numberOffFlashes ; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); // 0.5f putin transparent transparent
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2) );
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOffFlashes * 2));

        }
        Physics2D.IgnoreLayerCollision(10, 11, false);

    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invunerability());

        foreach (Behaviour component in components)
            component.enabled = true;
    }
}

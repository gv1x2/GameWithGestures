using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("FireTrapTimers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered; // cand capcana este declansata
    private bool active; // cand capcana este activata poate rani player-ul



    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (!triggered)
                StartCoroutine(ActivateFiretrap()); //declanseaza fiertrap

            if (active)
                collision.GetComponent<Health>().TakeDamage(damage);

        }
           
    }

    private IEnumerator ActivateFiretrap()
    {
        // schimba sprite-ul in rosu sa notifice player-ul
        triggered = true;
        spriteRend.color = Color.red; 

        //Asteapta pentu delay ,activa trapa, turn on animation, reeturneaza culoarea in starea initiala
        yield return new WaitForSeconds(activationDelay);
        spriteRend.color = Color.white;

        anim.SetBool("activated", true);

        // asteapta until x seconds,deactivate trap and rest all variables and animator
        active = true;
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        anim.SetBool("activated", false);

    }

}

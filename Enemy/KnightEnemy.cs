using Unity.VisualScripting;
using UnityEngine;

public class KnightEnemy : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerlayer;

    [Header("Health Settings")]
    [SerializeField] private int maxHits = 3;

    public GameObject knightEnemy;
    private int currentHits;
    private bool isDead = false;

    private float cooldownTimer = Mathf.Infinity;

    //references
    private Health playerHealth;
    private Animator anim;
    private EnemyPatrol enemyPatrol;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        if (isDead) return;

        cooldownTimer += Time.deltaTime;
        //Attack only when player in sight?
        if(PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //attack
                cooldownTimer = 0;
                anim.SetTrigger("knightAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();

    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance, 
            new Vector3(boxCollider.bounds.size.x * range,boxCollider.bounds.size.y, boxCollider.bounds.size.z) 
            ,0 ,Vector2.left ,0 , playerlayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();

        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        //If Player still in range damage him
        if (PlayerInSight())
        {
            playerHealth.TakeDamage(damage);
        }
    }
    public void TakeDamage()
    {
        if (isDead) return;

        currentHits++;

        if (currentHits >= maxHits)
        {
            Die();
        }
    }

    private void Die()
    {
        isDead = true;
        anim.SetTrigger("die");
        Invoke(nameof(DisableEnemy), 0.5f);
    }

    private void DisableEnemy()
    {
        knightEnemy.SetActive(false);
    }
}

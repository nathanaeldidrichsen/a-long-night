using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    public float stoppingDistance = 1.7f;
    [SerializeField] private Transform target;
    private Health targetHealth;
    private Animator anim;
    public float attackInterval = 1f;
    private float attackTimer = 0f;
    private float repositionDelay = 0.5f; // Delay before repositioning after attack
    private float repositionTimer = 0f;

    private enum State { MoveToTarget, Attack, RepositionAfterAttack }
    [SerializeField] private State currentState = State.MoveToTarget;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        target = Hearth.Instance.transform; // Assuming Hearth.Instance is valid and set elsewhere
    }

    void Update()
    {
        anim.SetFloat("moveX", Mathf.Abs(rb.velocity.x));

        if (target == null)
        {
            target = Hearth.Instance.transform;
        }

        float distance = Mathf.Abs(target.position.x - transform.position.x);

        switch (currentState)
        {
            case State.MoveToTarget:
                if (distance > stoppingDistance)
                {
                    MoveTowardsTarget();
                }
                else
                {
                    rb.velocity = Vector2.zero;
                    currentState = State.Attack;
                    attackTimer = 0f;
                }
                break;

            case State.Attack:
                rb.velocity = Vector2.zero; // Ensure the enemy stops moving while attacking
                attackTimer += Time.deltaTime;
                if (attackTimer >= attackInterval)
                {
                    Attack();
                    currentState = State.RepositionAfterAttack;
                    repositionTimer = 0f;
                }
                break;

            case State.RepositionAfterAttack:
                rb.velocity = Vector2.zero; // Ensure the enemy stops moving immediately after the attack
                repositionTimer += Time.deltaTime;
                if (repositionTimer >= repositionDelay)
                {
                    MoveAwayFromTarget();
                    if (distance > stoppingDistance + 0.1f)
                    {
                        currentState = State.MoveToTarget;
                    }
                }
                break;
        }
    }

    void Attack()
    {
        anim.SetTrigger("Attack");
        // Debug.Log("Attacking " + target.name);
    }

    void MoveTowardsTarget()
    {
        if (target == null)
            return;

        Vector2 direction = target.position - transform.position;
        direction.Normalize();
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); // Only move on the x-axis
        FlipEnemy(direction.x);
    }

    void MoveAwayFromTarget()
    {
        if (target == null)
            return;

        Vector2 direction = transform.position - target.position;
        direction.Normalize();
        rb.velocity = new Vector2(direction.x * moveSpeed, rb.velocity.y); // Only move on the x-axis
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && currentState != State.Attack)
        {
            // Set new target and start attacking
            target = other.transform;
            targetHealth = other.gameObject.GetComponent<Health>();
        }

        //Make sure it's not a crate
        if (other.gameObject.CompareTag("PlayerStructure") && currentState != State.Attack)
        {
            if(!other.gameObject.GetComponent<Building>().isCrate &&  other.gameObject.GetComponent<Building>().isDestroyable)
            // Set new target and start attacking
            target = other.transform;
            targetHealth = other.gameObject.GetComponent<Health>();
        }
    }

    void FlipEnemy(float directionX)
    {
        // Flip enemy sprite based on movement direction
        if (directionX > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (directionX < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void OnDrawGizmos()
    {
        // Draw stopping distance gizmo
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, stoppingDistance);
    }
}

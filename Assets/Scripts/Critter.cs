using System.Collections;
using UnityEngine;

public class Critter : MonoBehaviour
{
    public enum State
    {
        Idle,
        Moving,
        Attacked
    }

    public State currentState;
    public GameObject moveToTarget;
    public float moveSpeed = 2f;
    public float attackDistance = 10f;
    private Animator anim;

    private Rigidbody2D rb;
    private Transform playerTransform;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        playerTransform = Player.Instance.transform;
        StartCoroutine(StateMachine());
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                // Do nothing
                break;
            case State.Moving:
                MoveTowardsTarget();
                break;
            case State.Attacked:
                MoveAwayFromPlayer();
                break;
        }
    }

    IEnumerator StateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case State.Idle:
                    yield return new WaitForSeconds(Random.Range(2, 10));
                    currentState = State.Moving;
                    break;
                case State.Moving:
                    yield return new WaitForSeconds(15);
                    currentState = State.Idle;
                    break;
                case State.Attacked:
                    // Wait for some time before returning to idle or moving state
                    yield return new WaitForSeconds(1);
                    if (Vector3.Distance(transform.position, playerTransform.position) > attackDistance)
                    {
                        currentState = State.Moving;
                    }
                    break;
            }
        }
    }

    void MoveTowardsTarget()
    {
        if (moveToTarget != null)
        {
            anim.SetFloat("moveX", Mathf.Abs(rb.velocity.x));

            Vector2 direction = ((Vector2)moveToTarget.transform.position - rb.position).normalized;
            rb.velocity = direction * moveSpeed;

            // Flip the critter's local scale based on the direction it's moving
            if (rb.velocity.x > 0)
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            else if (rb.velocity.x < 0)
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            if (Vector2.Distance(rb.position, moveToTarget.transform.position) < 0.1f)
            {
                SetNewMoveToTarget();
            }
        }
    }

    void SetNewMoveToTarget()
    {
        float randomX = Random.Range(-5f, 5f);
        moveToTarget.transform.localPosition = new Vector2(randomX, moveToTarget.transform.localPosition.y);
    }

    void MoveAwayFromPlayer()
    {
        Vector2 direction = ((Vector2)transform.position - (Vector2)playerTransform.position).normalized;
        rb.velocity = direction * moveSpeed;

        anim.SetFloat("moveX", Mathf.Abs(rb.velocity.x));

        // Flip the critter's local scale based on the direction it's moving
        if (rb.velocity.x > 0)
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else if (rb.velocity.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);

        if (Mathf.Abs(transform.position.x - playerTransform.position.x) > attackDistance)
        {
            SetNewMoveToTarget();
            currentState = State.Moving;
        }
    }

    public void OnAttacked()
    {
        currentState = State.Attacked;
    }
}

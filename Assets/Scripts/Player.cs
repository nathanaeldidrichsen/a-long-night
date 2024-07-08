using UnityEngine;

public class Player : MonoBehaviour
{
    // Speed at which the player moves
    public float moveSpeed = 5f;

    // Reference to the player's Rigidbody2D component
    private Rigidbody2D rb;

    // Variable to store the horizontal movement input
    private float moveInput;

    // Variable to store the player's original local scale
    private Vector3 originalScale;
    [SerializeField] private Animator anim;

    // Singleton instance
    public static Player Instance { get; private set; }

    void Awake()
    {
        // Implement the singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Get the Rigidbody2D component attached to the player
        rb = GetComponent<Rigidbody2D>();

        // Store the player's original local scale
        originalScale = transform.localScale;
    }

    void Update()
    {
        // Get horizontal movement input
        moveInput = Input.GetAxis("Horizontal");

        // Check if the player is moving left or right and flip the local scale accordingly
        if (moveInput > 0)
        {
            // Moving right
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }
        else if (moveInput < 0)
        {
            // Moving left
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }

        anim.SetFloat("moveX", Mathf.Abs(moveInput));

        HandleInput();
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.Play("attack");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.Play("build");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.Play("mine");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.Play("chop");
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement to the player's Rigidbody2D
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }
}

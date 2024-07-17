using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    private Vector3 originalScale;
    private Animator anim;
    public int coins;
    private bool inventoryIsOpen;
    public GameObject inventory;
    public static Player Instance { get; private set; }

    void Awake()
    {
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
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        originalScale = transform.localScale;
    }

    void Update()
    {
        moveInput = Input.GetAxis("Horizontal");
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(-originalScale.x, originalScale.y, originalScale.z);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z);
        }

        anim.SetFloat("moveX", Mathf.Abs(moveInput));

        HandleInput();
    }

    public void GetCoin()
    {
        coins++;
        HUD.Instance.UpdateHUD(coins.ToString());
    }

    public void SpendCoins(int amount)
    {
        coins -= amount;
        HUD.Instance.UpdateHUD(coins.ToString());
    }

    public void HandleInput()
    {
        if (Input.GetMouseButtonDown(0) && !inventoryIsOpen)
        {
            anim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.Play("Build");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            anim.Play("Mine");
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.Play("Chop");
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventory.SetActive(!inventory.activeSelf);
            inventoryIsOpen = inventory.activeSelf;
        }

    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

    }


}

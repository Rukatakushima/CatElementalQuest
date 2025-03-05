using UnityEngine;
using Photon.Pun;
public class Movement : MonoBehaviour
{
    private PhotonView view;
    private Rigidbody2D rb;
    [SerializeField] private Transform childTransform;
    private Animator animator;
    public float moveSpeed, jumpForce;

    private bool isGrounded;
    public bool IsFacingRight = true;
    // private bool isFacingRight = true;
    // public bool IsFacingRight { get => isFacingRight; private set => isFacingRight = value; }

    private bool isSliding = false;
    [SerializeField] private float slideForce = 8f;
    [SerializeField] private float maxSlideSpeed = 25f;
    [SerializeField] private float slideDecay = 0.95f;
    private Vector2 slideDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();

        if (childTransform == null)
            childTransform = transform.Find("PlayerSprite");

        animator = childTransform.GetComponent<Animator>();
    }

    private void Start()
    {
        if (view.Owner.IsLocal)
            Camera.main.GetComponent<CameraFollower>().player = gameObject.transform;
    }

    private void Update()
    {
        if (view.IsMine)
        {
            UpdateFacingDirection();
            Move();
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (isSliding)
            ApplySlideForce();
        else if (slideDirection.magnitude > 0.1f)
            ApplySlideDecay();
    }

    private void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        view.RPC("RPC_UpdateWalkingAnimation", RpcTarget.All, moveInput != 0);
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    [PunRPC]
    private void RPC_UpdateWalkingAnimation(bool isWalking) => animator.SetBool("IsWalking", isWalking);

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void UpdateFacingDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if ((horizontalInput > 0 && !IsFacingRight) || (horizontalInput < 0 && IsFacingRight))
            view.RPC("RPC_UpdateFacingDirection", RpcTarget.All, !IsFacingRight);
    }

    [PunRPC]
    private void RPC_UpdateFacingDirection(bool facingRight)
    {
        if (IsFacingRight == facingRight) return;

        IsFacingRight = facingRight;
        FlipSprite();
    }

    private void FlipSprite()
    {
        Vector2 spriteScale = childTransform.localScale;
        spriteScale.x *= -1;
        childTransform.localScale = spriteScale;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    public void StartSliding(float slideForce)
    {
        isSliding = true;
        this.slideForce = slideForce;
        slideDirection = rb.velocity.normalized;
    }

    public void StopSliding() => isSliding = false;

    private void ApplySlideForce()
    {
        rb.AddForce(slideDirection * slideForce, ForceMode2D.Force);

        if (rb.velocity.magnitude > maxSlideSpeed)
            rb.velocity = rb.velocity.normalized * maxSlideSpeed;
    }

    private void ApplySlideDecay()
    {
        slideDirection *= slideDecay;
        rb.velocity = slideDirection * rb.velocity.magnitude;

        if (slideDirection.magnitude <= 0.1f)
        {
            slideDirection = Vector2.zero;
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }
}
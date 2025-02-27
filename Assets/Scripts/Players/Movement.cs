using UnityEngine;
using Photon.Pun;
public class Movement : MonoBehaviour
{
    private PhotonView view;
    private Rigidbody2D rb;
    private Transform childTransform;
    public float moveSpeed, jumpForce;

    private bool isGrounded, previousFacingRight;
    public bool isFacingRight = true;

    private bool isSliding = false;
    [SerializeField] private float slideForce = 8f;
    [SerializeField] private float maxSlideSpeed = 25f;
    [SerializeField] private float slideDecay = 0.95f;
    private Vector2 slideDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();

        childTransform = transform.Find("PlayerSprite");
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
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }

    private void UpdateFacingDirection()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            if (!isFacingRight)
            {
                isFacingRight = true;
                view.RPC("RPC_UpdateFacingDirection", RpcTarget.All, isFacingRight);
            }
        }
        else if (horizontalInput < 0)
        {
            if (isFacingRight)
            {
                isFacingRight = false;
                view.RPC("RPC_UpdateFacingDirection", RpcTarget.All, isFacingRight);
            }
        }

        FlipSprite();
    }

    [PunRPC]
    private void RPC_UpdateFacingDirection(bool facingRight)
    {
        isFacingRight = facingRight;
        FlipSprite();
    }

    private void FlipSprite()
    {
        if (isFacingRight != previousFacingRight)
        {
            Vector2 spriteScale = childTransform.localScale;
            spriteScale.x *= -1;
            childTransform.localScale = spriteScale;
        }
        previousFacingRight = isFacingRight;
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
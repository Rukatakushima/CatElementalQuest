using UnityEngine;
using Photon.Pun;
public class Movement : MonoBehaviour
{
    PhotonView view;
    private Rigidbody2D rb;
    private Transform childTransform;
    public float moveSpeed, jumpForce;

    private bool isGrounded, previousFacingRight;
    public bool isFacingRight = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        view = GetComponent<PhotonView>();

        childTransform = transform.Find("PlayerSprite");
        if (childTransform == null)
            Debug.LogError("PlayerSprite (transform) wasn't found");
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
            Move();
            Jump();

            UpdateFacingDirection();
        }
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
            isFacingRight = true;

        else if (horizontalInput < 0)
            isFacingRight = false;

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
}
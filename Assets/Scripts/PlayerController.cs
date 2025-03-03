using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb2d;
    SurfaceEffector2D surfaceEffector2D;

    [SerializeField] float torqueAmount = 1f;
    [SerializeField] float boostSpeed = 30f;
    [SerializeField] float baseSpeed = 20f;

    [SerializeField] float dashForce = 10f;
    [SerializeField] float dashCooldown = 1f;

    // ⚡ Biến Energy
    [SerializeField] float energyBoostForce = 5f;

    [SerializeField] float energyVerticalForce = 0f; // Giảm tốc độ bay xuống thấp hơn (từ 0.1f xuống 0.05f)

    [SerializeField] float energyDuration = 2f;
    bool isUsingEnergy = false;

    bool canMove = true;
    bool canDash = true;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        surfaceEffector2D = FindFirstObjectByType<SurfaceEffector2D>();
    }

    void Update()
    {
        if (!canMove) return;

        RotatePlayer();
        RespondToBoost();
        HandleDash();
    }

    public void DisableControls()
    {
        canMove = false;
    }

    void RotatePlayer()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb2d.AddTorque(torqueAmount);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rb2d.AddTorque(-torqueAmount);
        }
    }

    void RespondToBoost()
    {
        surfaceEffector2D.speed = Input.GetKey(KeyCode.UpArrow) ? boostSpeed : baseSpeed;
    }

    void HandleDash()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canDash)
        {
            rb2d.AddForce(transform.right * dashForce, ForceMode2D.Impulse);
            canDash = false;
            Invoke(nameof(ResetDash), dashCooldown);
        }
    }

    void ResetDash()
    {
        canDash = true;
    }

    // ⚡ Kích hoạt Energy khi chạm vào lon
    public void ActivateEnergy()
    {
        if (!isUsingEnergy)
        {
            isUsingEnergy = true;
            rb2d.AddForce(new Vector2(energyBoostForce, energyVerticalForce), ForceMode2D.Impulse); // Lực đẩy ngang + bay lên (ít hơn)
            Invoke(nameof(DeactivateEnergy), energyDuration);
        }
    }

    // ⚡ Kết thúc hiệu ứng Energy
    void DeactivateEnergy()
    {
        isUsingEnergy = false;
    }

    // ⚡ Xử lý khi chạm vào lon Energy
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Energy"))
        {
            ActivateEnergy();
            Destroy(collision.gameObject); // Xóa lon energy sau khi dùng
        }
    }
}
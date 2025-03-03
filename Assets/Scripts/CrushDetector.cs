using UnityEngine;
using UnityEngine.SceneManagement;

public class CrushDetector : MonoBehaviour
{
    [SerializeField] float loadDelay = 0.5f;
    [SerializeField] ParticleSystem crushEffect;
    [SerializeField] AudioClip crushSFX;
    [SerializeField] GameObject gameOverCanvas; // Đảm bảo gán trong Inspector
    bool hasCrushed = false;

    void Start()
    {
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (gameObject.CompareTag("Player") && other.CompareTag("Ground") && !hasCrushed)
        {
            HandleCrush();
        }
        else if ((gameObject.CompareTag("Player") || gameObject.CompareTag("Skateboard")) &&
                 other.CompareTag("Spike") && !hasCrushed)
        {
            HandleCrush();
        }
    }

    void HandleCrush()
    {
        hasCrushed = true;
        var playerController = FindAnyObjectByType<PlayerController>();
        if (playerController != null)
        {
            playerController.DisableControls();
        }
        if (crushEffect != null)
        {
            crushEffect.Play();
        }
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && crushSFX != null)
        {
            audioSource.PlayOneShot(crushSFX);
        }

        Invoke("ShowGameOver", loadDelay);
    }

    void ShowGameOver()
    {
        if (gameOverCanvas != null)
        {
            // Hiển thị Canvas
            gameOverCanvas.SetActive(true);

            // Dừng hoàn toàn thời gian game
            Time.timeScale = 0f;

            // Vô hiệu hóa input (nếu bạn dùng Input System cũ)
            // Nếu dùng Input System mới, bạn cần disable qua PlayerController
            Cursor.visible = true; // Hiện con trỏ chuột để click nút
            Cursor.lockState = CursorLockMode.None; // Mở khóa con trỏ
            PauseMenu pauseMenu = Object.FindFirstObjectByType<PauseMenu>();
            if (pauseMenu != null)
            {
                pauseMenu.enabled = false; // Tắt script PauseMenu
                Debug.Log("PauseMenu disabled to prevent Escape input.");
            }
        }
        else
        {
            Debug.LogError("GameOverCanvas chưa được gán trong Inspector!");
        }
    }

    public void RetryButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (gameOverCanvas != null)
        {
            gameOverCanvas.SetActive(false);
        }
        hasCrushed = false;
       
    }

    public void QuitButton()
    {
        Time.timeScale = 1f; // Reset timescale trước khi thoát
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
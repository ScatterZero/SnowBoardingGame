using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // Gán Panel của menu vào đây
    private bool isPaused = false;

    void Start()
    {
        Debug.Log("PauseMenu script is running!");

        // Đảm bảo GameObject luôn bật, chỉ ẩn UI
        pauseMenuUI.SetActive(true);
        HideMenu();
    }

    void Update()
    {
        Debug.Log("Update is running... isPaused = " + isPaused);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC Pressed! isPaused = " + isPaused);
            if (isPaused)
                ResumeGame();
            else
                PauseGame();
        }
    }

    public void ResumeGame()
    {
        Debug.Log("PAuse " + isPaused);

        HideMenu();
        Time.timeScale = 1f; // Tiếp tục game
        isPaused = false;
    }

    public void PauseGame()
    {
        ShowMenu();
        Time.timeScale = 0f; // Dừng game
        isPaused = true;
    }

    public void QuitGame()
    {
        Debug.Log("QUIT " + isPaused);
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.SetString("LastLevel", "Level1");
        PlayerPrefs.Save();
        Time.timeScale = 1f; // Đặt lại thời gian trước khi thoát
        SceneManager.LoadScene("MainMenu"); // Hoặc Application.Quit() để thoát game
    }

    private void ShowMenu()
    {
        pauseMenuUI.SetActive(true);
        CanvasGroup canvasGroup = pauseMenuUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    private void HideMenu()
    {
        CanvasGroup canvasGroup = pauseMenuUI.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }
}

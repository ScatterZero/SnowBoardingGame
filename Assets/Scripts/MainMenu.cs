using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button[] levelButtons; // Nút chọn Level
    public GameObject Help; // Hướng dẫn chơi
    public GameObject chooseLevelCanvas; // Canvas chọn màn chơi
    public GameObject mainMenuCanvas; // Canvas chính của menu

    void Start()
    {
        int unlockedLevel = PlayerPrefs.GetInt("LevelUnlocked", 1); // Mặc định mở Level 1

        // Kiểm tra và kích hoạt các nút Level
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i] != null)
            {
                levelButtons[i].interactable = (i + 1 <= unlockedLevel); // Cập nhật trạng thái nút
                int levelIndex = i + 1;
                levelButtons[i].onClick.RemoveAllListeners(); // Xóa sự kiện cũ để tránh trùng lặp
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
            }
        }
    }

    void OnEnable()
    {
        UpdateLevelButtons(); // Cập nhật khi Menu xuất hiện
    }

    // Khi bấm Start, mở Canvas chọn màn
    public void StartGame()
    {
        mainMenuCanvas.SetActive(false);
        chooseLevelCanvas.SetActive(true);
    }

    // Load Level theo chỉ số
    public void LoadLevel(int levelIndex)
    {

        SceneManager.LoadScene(levelIndex);
    }

    public void HowtoPlay()
    {
        if (Help != null)
        {
            Help.SetActive(!Help.activeSelf);
        }
    }

    public void CloseHelp()
    {
        if (Help != null)
        {
            Help.SetActive(false);
        }
    }

    public void QuitGame()
    {
        PlayerPrefs.DeleteAll();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void UpdateLevelButtons()
    {
        int unlockedLevel = PlayerPrefs.GetInt("LevelUnlocked", 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            if (levelButtons[i] != null)
            {
                levelButtons[i].interactable = (i + 1 <= unlockedLevel); // Cập nhật trạng thái nút
            }
        }
    }
}

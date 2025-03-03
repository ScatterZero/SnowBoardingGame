using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;
    private int score = 0; // Biến lưu điểm số tạm thời trong scene
    public Text scoreText; // UI hiển thị điểm
    public Button nextButton; // Nút Next

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Giữ ScoreManager khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Lắng nghe sự kiện load scene
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // Hủy đăng ký sự kiện khi bị destroy
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        string currentScene = scene.name;

        if (currentScene.StartsWith("Level")) // Nếu là màn chơi, reset điểm và hiển thị scoreText
        {
            score = 0;
            PlayerPrefs.SetInt("TotalScore", 0);
            PlayerPrefs.Save();
            UpdateScoreUI();

            if (scoreText != null) scoreText.gameObject.SetActive(true);
        }
        else // Nếu là Menu hoặc GameCompleted, ẩn scoreText
        {
            if (scoreText != null) scoreText.gameObject.SetActive(false);
        }

        CheckNextButton(); // Cập nhật trạng thái nút Next
    }

    public void AddScore(int amount)
    {
        score += amount;
        PlayerPrefs.SetInt("TotalScore", score);
        PlayerPrefs.Save();
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Coin " + score + "/10";
        }
        else
        {
            Debug.LogError("⚠️ ScoreText chưa được gán! Kéo UI Text vào ScoreManager.");
        }
    }

    public void NextScene()
    {
        string lastLevel = PlayerPrefs.GetString("LastLevel", "Level1");
        Match match = Regex.Match(lastLevel, @"\d+");

        if (match.Success)
        {
            int nextLevel = int.Parse(match.Value) + 1;
            string nextSceneName = "Level" + nextLevel;

            if (!Application.CanStreamedLevelBeLoaded(nextSceneName))
            {
                SceneManager.LoadScene("GameCompleted");
            }
            else
            {
                PlayerPrefs.SetString("LastLevel", nextSceneName);
                PlayerPrefs.Save();
                SceneManager.LoadScene(nextSceneName);
            }
        }
    }

    public void BackToMainMenu()
    {
        score = 0;
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.SetString("LastLevel", "Level1");
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }

    void CheckNextButton()
    {
        if (nextButton == null) return;

        string lastLevel = PlayerPrefs.GetString("LastLevel", "Level1");
        Match match = Regex.Match(lastLevel, @"\d+");

        if (match.Success)
        {
            int nextLevel = int.Parse(match.Value) + 1;
            string nextSceneName = "Level" + nextLevel;

            nextButton.gameObject.SetActive(Application.CanStreamedLevelBeLoaded(nextSceneName));
        }
    }
}

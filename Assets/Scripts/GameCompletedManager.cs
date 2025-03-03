using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;

public class GameCompleteManager : MonoBehaviour
{
    public Text scoreText;
    public Button nextButton;
    public GameObject messageCanvas;  // Canvas thông báo "SCENE NOT AVAILABLE"

    void Start()
    {
        Time.timeScale = 1;

        int finalScore = PlayerPrefs.GetInt("TotalScore", 0);
        if (scoreText != null)
        {
            scoreText.text = "Coin " + finalScore + "/10";
        }

        UnlockNextLevel();
    }

    private bool isTransitioning = false;

    public void NextScene()
    {
        if (isTransitioning) return; // Nếu đang chuyển scene thì bỏ qua
        isTransitioning = true;

        string lastLevel = PlayerPrefs.GetString("LastLevel", "Level1");
        if (int.TryParse(Regex.Match(lastLevel, "\\d+").Value, out int nextLevel))
        {
            string nextSceneName = "Level" + (nextLevel + 1);
            PlayerPrefs.SetString("LastLevel", nextSceneName);
            PlayerPrefs.Save();

            if (Application.CanStreamedLevelBeLoaded(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                ShowMessage();
            }
        }
    }


    public void BackToMainMenu()
    {
        PlayerPrefs.SetInt("TotalScore", 0);
        PlayerPrefs.SetString("LastLevel", "Level1");
        PlayerPrefs.Save();
        SceneManager.LoadScene("MainMenu");
    }

    void UnlockNextLevel()
    {
        string lastLevel = PlayerPrefs.GetString("LastLevel", "Level1");
        if (int.TryParse(Regex.Match(lastLevel, "\\d+").Value, out int currentLevel))
        {
            int nextLevel = currentLevel + 1;
            int unlockedLevel = PlayerPrefs.GetInt("LevelUnlocked", 1);

            if (nextLevel > unlockedLevel)
            {
                PlayerPrefs.SetInt("LevelUnlocked", nextLevel);
                PlayerPrefs.Save();
            }
        }
    }

    void ShowMessage()
    {
        if (messageCanvas != null)
        {
            messageCanvas.SetActive(true);  // Hiển thị Canvas chứa thông báo
        }
    }
}

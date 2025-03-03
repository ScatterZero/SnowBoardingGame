using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    [SerializeField] float loadDelay = 1f;
    [SerializeField] ParticleSystem finishEffect;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            finishEffect.Play();
            GetComponent<AudioSource>().Play();

            UnlockNextLevel(); // Mở khóa level tiếp theo

            Invoke("LoadNextScene", loadDelay);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene("GameCompleted"); // Đổi tên Scene cần load

    }

    void UnlockNextLevel()
    {
        int currentLevel = SceneManager.GetActiveScene().buildIndex;
        int unlockedLevel = PlayerPrefs.GetInt("LevelUnlocked", 1);

        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("LevelUnlocked", currentLevel + 1);
            PlayerPrefs.Save();
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockLevel : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Khi nhân vật chạm vào vạch đích
        {
            int currentLevel = SceneManager.GetActiveScene().buildIndex; // Lấy ID màn hiện tại
            int unlockedLevel = PlayerPrefs.GetInt("LevelUnlocked", 1);

            if (currentLevel >= unlockedLevel) // Nếu Level chưa được mở khóa
            {
                PlayerPrefs.SetInt("LevelUnlocked", currentLevel + 1); // Mở khóa màn tiếp theo
                PlayerPrefs.Save(); // Lưu lại
            }

            // 🔥 Gọi sự kiện để cập nhật menu
            FindFirstObjectByType<MainMenu>().UpdateLevelButtons();
        }
    }
}

using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;
    public AudioSource audioSource; // Tham chiếu đến AudioSource
    public AudioClip pickupSound;   // Tham chiếu đến âm thanh nhặt xu

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            ScoreManager.Instance.AddScore(coinValue);
            AudioSource playerAudio = other.GetComponent<AudioSource>();
            if (playerAudio != null)
            {
                playerAudio.PlayOneShot(pickupSound); // Phát âm thanh từ Player
            }

            Destroy(gameObject);
        }
    }
}

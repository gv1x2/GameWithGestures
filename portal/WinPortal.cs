using UnityEngine;

public class WinPortal : MonoBehaviour
{
    public GameObject winScreen; // UI Panel sau imagine

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.SetActive(true); // Arată Win Screen-ul
            Time.timeScale = 0f; // Oprește jocul
            
        }
    }
}

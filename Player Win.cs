using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

public class PlayerWin : MonoBehaviour
{
    public GameObject winScreen;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Win")
        {
            Time.timeScale = 0f;//Opreste jocul
            winScreen.SetActive(true);
        }

    }
}

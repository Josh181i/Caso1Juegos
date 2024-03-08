using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void RestartButton()
    {
        Time.timeScale = 1f;

        FindObjectOfType<PlayerController>().RestartGame();

     
        gameObject.SetActive(false);
    }
}

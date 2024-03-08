using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    [SerializeField] public float totalHealth = 100;
    public float currentHealth;
    public Image healthBarImage1; 
    public Image healthBarImage2; 
    public Text gameOverText; 

    private void Awake()
    {
        currentHealth = totalHealth;
        UpdateHealthBar();
        HideGameOverText(); 
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            TakeDamage(25f);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die(); 
        }
        UpdateHealthBar();
    }

    void UpdateHealthBar()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (healthBarImage1 != null && healthBarImage2 != null)
            {
                float healthRatio = currentHealth / totalHealth;
                healthBarImage1.fillAmount = healthRatio;

               
                
                healthBarImage2.fillAmount = 1 - healthRatio;
            }
            else
            {
                Debug.LogWarning("Una de las imágenes de la barra de vida no ha sido asignada en el Inspector.");
            }
        }
        else
        {
            if (healthBarImage1 != null)
            {
                float healthRatio = currentHealth / totalHealth;
                healthBarImage1.fillAmount = healthRatio;
            }
            else
            {
                Debug.LogWarning("La imagen de la barra de vida no ha sido asignada en el Inspector para el enemigo.");
            }
        }
    }

    void Die()
    {
        
        Debug.Log("El jugador ha muerto");
        if (gameObject.CompareTag("Player"))
        {
            ShowGameOverText();
            Invoke("RestartLevel", 3f); 
        }
        else
        {
            
            gameObject.SetActive(false);
        }
    }

    void RestartLevel()
    {
       
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    void ShowGameOverText()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(true);
        }
    }

    void HideGameOverText()
    {
        if (gameOverText != null)
        {
            gameOverText.gameObject.SetActive(false);
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int lives = 3;
    public int maxHealthPerLife = 10;
    public int currentHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
        currentHealth = maxHealthPerLife;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Invoke("UpdateLivesUI", 0.1f);
    }


    public void GainHealth(int amount)
    {
        currentHealth += amount;

        while (currentHealth >= maxHealthPerLife)
        {
            lives++;
            currentHealth -= maxHealthPerLife;
        }

        if (currentHealth == 0) currentHealth = 1;

        UpdateLivesUI();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            LoseLife();
        }

        UpdateLivesUI();
    }

    public void LoseLife()
    {
        lives--;

        if (lives > 0)
        {
            currentHealth = maxHealthPerLife; 
        }
        else
        {
            GameOver();
        }

        UpdateLivesUI();
    }

    // Actualizar o contador de vidas da interfaz
    public void UpdateLivesUI()
    {
        UIManager ui = FindUIManager();
        if (ui != null)
        {
            ui.UpdateLives(currentHealth, maxHealthPerLife, lives);
        }
    }

    private UIManager FindUIManager()
    {
        return Object.FindFirstObjectByType<UIManager>();
    }



    private void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void ResetGameState()
    {
        lives = 3;
        currentHealth = maxHealthPerLife;
        UpdateLivesUI();
        Debug.Log("Estado del juego reiniciado.");
    }
}

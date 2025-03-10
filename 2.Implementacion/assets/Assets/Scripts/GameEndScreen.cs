using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndScreen : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            ResetGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }
    }

    void ResetGame()
    {
        // Reiniciar la escena
        SceneManager.LoadScene("FirstLevel");

        // Llamar a ResetGameState() del GameManager si existe
        if (GameManager.instance != null)
        {
            GameManager.instance.ResetGameState();
        }
    }

    void QuitGame()
    {
        #if UNITY_WEBGL
            Debug.Log("Salir no es compatible en WebGL. Cierra la pestaña o recarga la página.");
        #elif UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}

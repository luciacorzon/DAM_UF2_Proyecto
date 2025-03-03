using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartScreen : MonoBehaviour
{
    public float blinkInterval = 0.5f; 

    private GameObject blinkingText;
    private bool isBlinking = true;

    void Start()
    {
        blinkingText = GameObject.FindGameObjectWithTag("BlinkingText");

        if (blinkingText != null)
        {
            StartCoroutine(BlinkText());
        }
        else
        {
            Debug.LogWarning("No se encontró un objeto con la etiqueta 'BlinkingText'.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            LoadNextScene();
        }
    }

    IEnumerator BlinkText()
    {
        while (isBlinking)
        {
            blinkingText.SetActive(!blinkingText.activeSelf); 
            yield return new WaitForSeconds(blinkInterval);
        }
    }

    void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public Slider healthSlider;
    public TextMeshProUGUI livesText;

    public void UpdateLives(int health, int maxHealthPerLife, int lives)
    {
        healthSlider.maxValue = maxHealthPerLife;
        healthSlider.value = Mathf.Clamp(health, 1, maxHealthPerLife);
        livesText.text = "x" + lives;
    }
}

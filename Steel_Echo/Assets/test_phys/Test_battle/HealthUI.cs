using UnityEngine;
using TMPro;

public class HealthUI : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI healthText;

    void Update()
    {
        if (playerHealth != null)
        {
            healthText.text = "HP: " + playerHealth.health;
        }
    }
}
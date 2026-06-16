using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    public GameObject deathText;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(RestartRoutine());
        }
    }

    IEnumerator RestartRoutine()
    {
        deathText.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}
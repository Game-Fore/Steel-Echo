using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class Death_pl : MonoBehaviour
{
    public GameObject deathText;
    public float restartDelay = 2f;

    private bool isDead = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            isDead = true;
            StartCoroutine(DeathSequence());
        }
    }

    IEnumerator DeathSequence()
    {
        deathText.SetActive(true);

        yield return new WaitForSeconds(restartDelay);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
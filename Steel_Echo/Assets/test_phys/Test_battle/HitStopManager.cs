using UnityEngine;
using System.Collections;

public class HitStopManager : MonoBehaviour
{
    public static HitStopManager instance;

    void Awake()
    {
        instance = this;
    }
    public void Stop(float duration)
    {
        StartCoroutine(DoStop(duration));
    }
    IEnumerator DoStop(float duration)
    {
        Time.timeScale = 0f;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1f;
    }
}
using System.Collections;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    public GameObject[] countDownItems;

    void Start()
    {
        foreach (GameObject g in countDownItems)
            g.SetActive(false);
    }

    public void PlayCountDown(IEnumerator callback)
    {
        StartCoroutine(EnumerateCountDown(callback));
    }

    IEnumerator EnumerateCountDown(IEnumerator callback)
    {
        yield return new WaitForSeconds(2);
        foreach (GameObject g in countDownItems)
        {
            g.SetActive(true);
            yield return new WaitForSeconds(1);
            g.SetActive(false);
        }

        yield return StartCoroutine(callback);
    }
}

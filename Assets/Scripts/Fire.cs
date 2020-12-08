using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.GetComponent<Foam>())
        {
            Debug.Log("Start Extingiush");
            StartCoroutine(Extingiush());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.GetComponent<Foam>())
        {
            Debug.Log("Stop Extingiush");
            StopAllCoroutines();
        }
    }

    private IEnumerator Extingiush()
    {
        Debug.Log("Extingiush");
        int time = GameManager.ExtinguishingTime;
        while (time > 0)
        {
            time--;
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
}

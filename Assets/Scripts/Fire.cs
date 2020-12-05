using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public Transform foam;
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.name);
        if (other.transform.name == foam.name)
        {
            StartCoroutine(Extingiush());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.name == foam.name)
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator Extingiush()
    {
        Debug.Log("Extingiush");
        float time = 3f;
        while (time > 0)
        {
            time--;
            yield return new WaitForSeconds(1f);
        }
        Destroy(gameObject);
    }
}

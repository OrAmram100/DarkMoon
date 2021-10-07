using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{
    Light theLight;
    private float minTime = 0.1f, maxTime = 1;

    // Start is called before the first frame update
    void Start()
    {
        theLight = GetComponent<Light>();
        StartCoroutine(makeLightFlicker());
    }
    IEnumerator makeLightFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minTime, maxTime));
            theLight.enabled = !theLight.enabled;
        }
    }
    // Update is called once per frame
   
}

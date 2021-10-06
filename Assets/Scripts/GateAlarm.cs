using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateAlarm : MonoBehaviour
{
    private AudioSource alarm;

    // Start is called before the first frame update
    void Start()
    {
        alarm = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            alarm.Play();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

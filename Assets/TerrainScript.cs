using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainScript : MonoBehaviour
{
    private AudioSource windSound;
    // Start is called before the first frame update
    void Start()
    {
        windSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        windSound.Play(); 
    }
}

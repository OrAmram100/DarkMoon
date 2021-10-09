using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTime : MonoBehaviour
{
    public int timer = 5;
    // Start is called before the first frame update

    private void Awake()
    {
        Destroy(gameObject, timer);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadShot : MonoBehaviour
{
    public GameObject blood;
    public GameObject head;
    // Start is called before the first frame update

    private void OnDisable()
    {
        head.SetActive(false);
        blood.SetActive(true);

    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

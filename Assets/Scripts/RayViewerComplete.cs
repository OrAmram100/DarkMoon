using UnityEngine;
using System.Collections;

public class RayViewerComplete : MonoBehaviour
{

    public float weaponRange = 50f;                       // Distance in Unity units over which the Debug.DrawRay will be drawn

    public GameObject fpsCam;                                // Holds a reference to the first person camera


    void Start()
    {
        // Get and store a reference to our Camera by searching this GameObject and its parents
        //  fpsCam = GetComponent<GameObject>();
    }


    void Update()
    {
        // Create a vector at the center of our camera's viewport
        Vector3 lineOrigin = fpsCam.GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        // Draw a line in the Scene View  from the point lineOrigin in the direction of fpsCam.transform.forward * weaponRange, using the color green
        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRange, Color.green);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class PlayerMotion : MonoBehaviour
{
    private float speed = 40, angularSpeed = 45;
    private CharacterController controller;
    private float rotationAboutY = 0, rotationAboutX = 0;
    public GameObject camera;
    private AudioSource stepSound;





    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        rotationAboutY += transform.localEulerAngles.y;
        stepSound = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }









    // Update is called once per frame
    void Update()
    {
        float dx, dy = -1/*kind of a gravity*/, dz;

        // rotation about Y
        rotationAboutY += Input.GetAxis("Mouse X") * angularSpeed * Time.deltaTime;
        transform.localEulerAngles = new Vector3(0, rotationAboutY, 0);

        // rotation about X
        rotationAboutX -= Input.GetAxis("Mouse Y") * angularSpeed * Time.deltaTime;
        camera.transform.localEulerAngles = new Vector3(rotationAboutX, 0, 0);

        // moving forward/backward/left/right
        dz = Input.GetAxis("Vertical"); // can be -1, 0 , 1
        dz *= speed * Time.deltaTime;

        dx = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

        Vector3 motion = new Vector3(dx, dy, dz); // in Local coordinates
        motion = transform.TransformDirection(motion);// change to Global coordinates
        controller.Move(motion);//in Global coordinates
        if (dz < -0.1 || dz > 0.1 || dx < -0.1 || dx > 0.1)
        {

            if (!stepSound.isPlaying)
                stepSound.Play();
        }




    }
}

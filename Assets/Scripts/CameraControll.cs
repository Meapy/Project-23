using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControll : MonoBehaviour
{
    // Start is called before the first frame update
    private int count = 0;
    void Start()
    {
        //get all the objects with the tag camera
        GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
        print(cameras);
        //set the main camera to the first camera in the array
        cameras[0].GetComponent<Camera>().enabled = true;
        //activate the audio listener
        cameras[0].GetComponent<AudioListener>().enabled = true;
        count = 0;
        //set the rest of the cameras to false
        for (int j = 1; j < cameras.Length; j++)
        {
            cameras[j].GetComponent<Camera>().enabled = false;
            //deactivate the audio listener for the rest of the cameras
            cameras[j].GetComponent<AudioListener>().enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if space is pressed, switch camera 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] cameras = GameObject.FindGameObjectsWithTag("Camera");
            cameras[count].GetComponent<Camera>().enabled = false;
            cameras[count].GetComponent<AudioListener>().enabled = false;
            count++;
            if (count >= cameras.Length)
            {
                count = 0;
            }
            cameras[count].GetComponent<Camera>().enabled = true;
            cameras[count].GetComponent<AudioListener>().enabled = true;
        }
    }
}

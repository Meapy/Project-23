using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListener : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioClip;
    public float volume = 0.5f;
    public bool loop = true;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();
    }

   //create a coroutine to play a sound every 5 seconds 
    IEnumerator PlaySound()
    {
        while (true)
        {
            audioSource.PlayOneShot(audioClip, volume);
            yield return new WaitForSeconds(5);
        }
    }
    void Update()
    {
        if (loop)
        {
            StartCoroutine(PlaySound());
            loop = false;
        }
    }


}

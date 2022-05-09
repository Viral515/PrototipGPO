using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    public GameObject frame;
    public GameObject[] otherFrames;
    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if(other.CompareTag("Player"))
        {
            frame.SetActive(true);
            foreach(GameObject frame in otherFrames)
            {
                frame.SetActive(false);
            }
        }*/
        if (other.CompareTag("Player"))
            SceneManager.LoadScene(2);
    }

}

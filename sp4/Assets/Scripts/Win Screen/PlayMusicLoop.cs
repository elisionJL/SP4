using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicLoop : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (!gameObject.GetComponent<AudioSource>().isPlaying && gameObject.GetComponent<AudioSource>().enabled == true)
            gameObject.transform.GetChild(0).gameObject.GetComponent<AudioSource>().enabled = true;
    }
}

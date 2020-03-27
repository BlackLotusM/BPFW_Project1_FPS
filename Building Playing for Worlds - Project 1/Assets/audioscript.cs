using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioscript : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource test;
    public GameObject lava;
    void Start()
    {
        AudioSource.PlayClipAtPoint(test.clip, lava.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (test.isPlaying)
        {
            AudioSource.PlayClipAtPoint(test.clip, lava.transform.position);
        }
    }
}

using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    AudioSource source;
    AudioClip clip;
    int nbpiste = 1;
    // Use this for initialization
    void Start()
    {
        source = this.GetComponent<AudioSource>();
        source.clip = Resources.Load<AudioClip>("Sound/Piste 1");
        source.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!source.isPlaying)
        {
            source.Stop();
            nbpiste++;
            if (nbpiste > 10)
            {
                nbpiste = 1;
            }
            source.clip = Resources.Load<AudioClip>("Sound/Piste " + nbpiste);
            source.Play();
        }

    }
}

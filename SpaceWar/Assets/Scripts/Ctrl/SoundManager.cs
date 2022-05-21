using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClip;

    void Play_BGM(){

    }

    public void Play_Attack(){
        audioSource.PlayOneShot(audioClip[1]);
    }
    public void Play_Spark(){
        audioSource.PlayOneShot(audioClip[2]);
    }

    public void Play_Siren(){
        audioSource.PlayOneShot(audioClip[3]);
    }
    public void Play_Door(){
        audioSource.PlayOneShot(audioClip[4]);
    }
    public void Play_Wrong(){
        audioSource.PlayOneShot(audioClip[7]);
    }

    public void Play_Interact(){
        audioSource.PlayOneShot(audioClip[6]);
    }
    public void Play_Correct(){
        audioSource.PlayOneShot(audioClip[8]);
    }
    public void Play_Fall(){
        audioSource.PlayOneShot(audioClip[9]);
    }
    public void Play_Stage(){
        audioSource.PlayOneShot(audioClip[10]);
    }

}

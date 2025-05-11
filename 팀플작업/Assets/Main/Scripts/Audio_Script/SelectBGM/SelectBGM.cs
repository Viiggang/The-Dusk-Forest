using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBGM : MonoBehaviour
{

    public AudioState Audiopath;
    protected void Start()
    {
        if (AudioMachine.Instance != null)
        {
            AudioMachine.Instance.ChangeAudioClip(Audiopath);
        }
    }
}

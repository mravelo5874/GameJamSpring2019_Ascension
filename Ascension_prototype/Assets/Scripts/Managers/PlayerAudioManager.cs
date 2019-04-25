using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{
    public AudioSource jump_audioSource;
    public AudioSource run_audioSource;
    public AudioSource etc_audioSource;

    public AudioClip footstep;
    public AudioClip hit;
    public AudioClip jump;
    public AudioClip walljump;
    public AudioClip land;
    public AudioClip punch;

    private bool is_running = false;

    public void play_run()
    {
        if (!is_running)
        {
            run_audioSource.clip = footstep;
            run_audioSource.loop = true;
            //run_audioSource.Play();
            is_running = true;
        }
    }

    public void pause_run()
    {
        run_audioSource.Pause();
        is_running = false;
    }

    public void play_hit()
    {
        etc_audioSource.clip = hit;
        etc_audioSource.Play();
    }

    public void play_jump()
    {
        jump_audioSource.clip = jump;
        jump_audioSource.Play();
    }

    public void play_walljump()
    {
        jump_audioSource.clip = walljump;
        jump_audioSource.Play();
    }

    public void play_land()
    {
        etc_audioSource.clip = land;
        etc_audioSource.Play();
    }

    public void play_punch()
    {
        etc_audioSource.clip = punch;
        etc_audioSource.Play();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip cancel;
    public AudioClip cool_select;
    public AudioClip elimination;
    public AudioClip go_horn;
    public AudioClip reach_goal;
    public AudioClip ready_horn;
    public AudioClip select;
    public AudioClip win_scene;

    public void play_cancel()
    {
        audioSource.clip = cancel;
        audioSource.Play();
    }

    public void play_cool_select()
    {
        audioSource.clip = cool_select;
        audioSource.Play();
    }

    public void play_elimination()
    {
        audioSource.clip = elimination;
        audioSource.Play();
    }

    public void play_go_horn()
    {
        audioSource.clip = go_horn;
        audioSource.Play();
    }

    public void play_reach_goal()
    {
        audioSource.clip = reach_goal;
        audioSource.Play();
    }

    public void play_ready_horn()
    {
        audioSource.clip = ready_horn;
        audioSource.Play();
    }

    public void play_select()
    {
        audioSource.clip = select;
        audioSource.Play();
    }

    public void play_win_scene()
    {
        audioSource.clip = win_scene;
        audioSource.Play();
    }
}

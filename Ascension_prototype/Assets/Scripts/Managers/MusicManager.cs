using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip titleSong;
    public AudioClip adrenalineSong;
    public AudioClip adrenalineSong_muffled;
    public AudioClip scoreboardSong;
    public AudioClip scoreboardSong_muffled;

    public void playTitleSong()
    {
        audioSource.clip = titleSong;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void playAdrenaline(bool muffled)
    {
        if (muffled)
            audioSource.clip = adrenalineSong_muffled;
        else
            audioSource.clip = adrenalineSong;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void playScoreboard(bool muffled)
    {
        if (muffled)
            audioSource.clip = scoreboardSong_muffled;
        else
            audioSource.clip = scoreboardSong;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void pauseMusic()
    {
        audioSource.Pause();
    }

    public void playMusic()
    {
        audioSource.Play();
    }
}

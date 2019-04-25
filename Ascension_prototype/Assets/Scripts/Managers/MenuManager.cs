using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuManager : MonoBehaviour
{
    private void Start()
    {
        StaticVariables.i.musicManager.playTitleSong();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameOptionsScene");
        StaticVariables.i.sfxManager.play_cool_select();
    }

    public void Settings()
    {
        StaticVariables.i.sfxManager.play_cancel();
    }

    public void ExitGame()
    {
        StaticVariables.i.sfxManager.play_cancel();
        Application.Quit();
    }
}

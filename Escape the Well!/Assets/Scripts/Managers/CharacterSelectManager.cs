using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    private void Awake()
    {
        
    }

    public void LoadGame()
    {
        SceneManager.LoadScene("LevelScene");
    }

}

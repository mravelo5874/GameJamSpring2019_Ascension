using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RSL_Manager : MonoBehaviour
{
    public List<string> levels;

    private void Awake()
    {
        int num = Random.Range(0, levels.Count - 1);

        SceneManager.LoadScene(levels[num]);
    }
}

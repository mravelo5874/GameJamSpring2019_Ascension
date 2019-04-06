using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingGameManager : MonoBehaviour
{
    public GameObject bar_object;
    public TextMeshProUGUI loading_text;
    public float time_to_load;
    private float timer;
    private float text_timer;
    public string scene_to_load;


    private void Awake()
    {
        timer = 0f;
        text_timer = 0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        text_timer += Time.deltaTime;

        // animate loading text
        if (text_timer >= 0f && text_timer < 0.333)
        {
            loading_text.text = "LOADING.";
        }
        else if (text_timer >= 0.333 && text_timer < 0.666)
        {
            loading_text.text = "LOADING..";

        }
        else if (text_timer >= 0.666 && text_timer < 1f)
        {
            loading_text.text = "LOADING...";

        }
        else if (text_timer > 1f)
        {
            text_timer = 0f;
        }

        // animate loading bar
        float _x = Mathf.Lerp(0f, 1f, timer / time_to_load);
        bar_object.transform.localScale = new Vector3(_x, 1f, 1f);

        if (timer >= time_to_load)
        {
            StartCoroutine(LoadScene());
        }
    }

    private IEnumerator LoadScene()
    {
        // load scene after small delay
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(scene_to_load);
    }
}

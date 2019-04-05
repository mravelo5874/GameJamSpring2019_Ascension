using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BarManager : MonoBehaviour
{
    public RectTransform node_rt;
    public TextMeshProUGUI text;
    private string player_name;

    public GameObject point_bar_object;
    public RectTransform pbo_rt;
    public RectTransform pb_rt;
    public Image point_bar_color;
    public GameObject delta_bar_object;
    public RectTransform dbo_rt;
    public RectTransform db_rt;


    private bool isLerp = false;
    private float total_lerp_time;
    private float lerp_timer = 0f;
    private bool lerpPointsBar = false;

    private int start_point_lerp;
    private int end_point_lerp;

    private void Awake()
    {
    }

    public void SetName(string _name)
    {
        player_name = _name;
    }

    public void SetPointBarColor(Color color)
    {
        point_bar_color.color = color;
    }

    public void SetPointBarScale(float num)
    {
        if (num < 0f)
        {
            num = 0f;
        }
        if (num > 1f)
        {
            num = 1f;
        }
        //Debug.Log(num);
        point_bar_object.transform.localScale = new Vector3(num, 1, 1);
    }

    public void SetDeltaBarScale(float num)
    {
        if (num < 0f)
        {
            num = 0f;
        }
        if (num > 1f)
        {
            num = 1f;
        }
        //Debug.Log(num);
        delta_bar_object.transform.localScale = new Vector3(num, 1, 1);
    }

    public void SetPointRangeToLerp(int start, int delta)
    {
        start_point_lerp = start;
        end_point_lerp = start + delta;
        if (end_point_lerp < 0)
        {
            end_point_lerp = 0;
        }

        text.text = " [" + player_name.ToString() + "] " + start_point_lerp.ToString() + "/" + StaticVariables.i.pointCount.ToString();
    }

    public void LerpPointBarOverTime(float delay_time, float lerp_time)
    {
        total_lerp_time = lerp_time;
        StartCoroutine(LerpBar(delay_time));

        lerpPointsBar = true;
    }

    public void LerpDeltaBarOverTime(float delay_time, float lerp_time)
    {
        total_lerp_time = lerp_time;
        StartCoroutine(LerpBar(delay_time));

        lerpPointsBar = false;
    }

    private IEnumerator LerpBar(float delay_time)
    {
        yield return new WaitForSeconds(delay_time);
        isLerp = true;
    }

    private void Update()
    {
        if (isLerp)
        {
            lerp_timer += Time.deltaTime;
            if (lerp_timer > total_lerp_time)
            {
                lerp_timer = total_lerp_time;
                isLerp = false;
            }
            
            // lerping the points bar (increase in player points)
            if (lerpPointsBar)
            {
                float x_scale = Mathf.Lerp(point_bar_object.transform.localScale.x, delta_bar_object.transform.localScale.x, lerp_timer / total_lerp_time);
                point_bar_object.transform.localScale = new Vector3(x_scale, 1, 1);
            }
            else // lerp delta bar (decrease in player points)
            {
                float x_scale = Mathf.Lerp(delta_bar_object.transform.localScale.x, point_bar_object.transform.localScale.x, lerp_timer / total_lerp_time);
                delta_bar_object.transform.localScale = new Vector3(x_scale, 1, 1);
            }

            int num = Mathf.RoundToInt(Mathf.Lerp(start_point_lerp, end_point_lerp, lerp_timer / total_lerp_time));
            text.text = " [" + player_name.ToString() + "] " + num + "/" + StaticVariables.i.pointCount.ToString();
        }
    }
}

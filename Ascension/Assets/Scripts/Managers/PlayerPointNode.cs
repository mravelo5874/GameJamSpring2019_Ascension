using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerPointNode : MonoBehaviour
{
    public Image img;
    public TextMeshProUGUI points_text;
    public TextMeshProUGUI name_text;
    public GameObject crown;

    private void Awake()
    {
        crown.SetActive(false);
    }

    public void DecorateNode(Color _color, int _points, string _name, bool _is_crown)
    {
        img.color = new Color(_color.r, _color.g, _color.b, (float)160 / 255);
        points_text.text = _points.ToString() + " pts";
        name_text.text = "[" + _name + "]";

        if (_is_crown)
        {
            crown.SetActive(true);
        }
    }
}

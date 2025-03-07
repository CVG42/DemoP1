using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] Text _strokes;

    void Start()
    {
        _strokes.text = "Golpes: " + 0;
    }

    void Update()
    {
        updateScore();
    }

    public void updateScore()
    {
        _strokes.text = "Golpes: " + GameManager.Instance.strokesAmount.ToString();
    }
}

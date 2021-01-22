using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionSelector : MonoBehaviour
{
    [SerializeField]
    private string optionName;
    private int index;

    [SerializeField]
    private string[] options;

    [SerializeField]
    private Button Left, Right;
    [SerializeField]
    private TextMeshProUGUI optionText;

    public Action onSelect;

    // Start is called before the first frame update
    void Start()
    {
        index = PlayerPrefs.GetInt(optionName,0);
        UpdateButtons();
        Left.onClick.AddListener(GoLeft);
        Right.onClick.AddListener(GoRight);
    }

    public void GoRight()
    {
        index++;
        UpdateButtons();
    }

    public void GoLeft()
    {
        index--;       
        UpdateButtons();
    }

    public bool UpdateButtons()
    {
        bool check = true;

        optionText.text = options[index];

        if (index == 0)
        {
            Left.gameObject.SetActive(false);
            check = false;
        }
        else
        {
            Left.gameObject.SetActive(true);
        }

        if (index == options.Length - 1)
        {
            Right.gameObject.SetActive(false);
            check = false;
        }
        else
        {
            Right.gameObject.SetActive(true);
        }

        PlayerPrefs.SetInt(optionName, index);

        onSelect.Invoke();

        return check;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

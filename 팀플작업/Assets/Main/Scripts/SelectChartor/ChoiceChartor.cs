using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceChartor : MonoBehaviour
{
    public Chatator chatator;
    Button button;
    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetChartorChoiceInfo);
    }
    void SetChartorChoiceInfo()
    {
        Select.Instance.SelectChatator=chatator;
    }
}

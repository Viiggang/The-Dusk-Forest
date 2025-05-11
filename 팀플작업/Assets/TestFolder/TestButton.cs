using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class TestButton : MonoBehaviour
{
    private Button mybutton;
    private void Awake()
    {
        mybutton = GetComponent<Button>();
    }
    void Start()
    {
        mybutton.onClick.AddListener(qwertyt);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void qwertyt()
    {
        Debug.Log("dsdsds");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelect : MonoBehaviour
{
    enum Player_Type
    {
        Warrior,
        Soul_Eter
    };

    public int Playernum = 0;

    public GameObject[] Player;


    public void Start()
    {
        for (int i = 0; i < Player.Length; i++)
        {
            Player[i].SetActive(false);
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Player[Playernum].SetActive(true);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPlayerScript : MonoBehaviour
{
    public GameObject[] Player;

    void Start()
    {
        Player = new GameObject[5];
        Player[0] = GameObject.Find("JlanButton");
        Player[1] = GameObject.Find("TeshiButton");
        Player[2] = GameObject.Find("TerukazuButton");
        Player[3] = GameObject.Find("AkiraButton");
        Player[4] = GameObject.Find("SupamanButton");
    }
    void Update()
    {

    }
    public void OnClickButton()
    {

    }
}

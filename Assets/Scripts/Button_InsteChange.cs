using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_InsteChange : MonoBehaviour
{
    public GameObject buttonNormal;
    public GameObject buttonInterstitial;
    int x = 0;


    public void OnResetButtonCount()
    {
        //x = ES3.Load("cancelkey", x);
        x += 1;
        CancelButton();


        //Debug.Log("きゃせるボタンの表示回数" + x);
    }


    public void OnResetButtonCountRest()
    {
        x = 0;
        CancelButton();

        //Debug.Log("きゃせるボタンの表示回数" + x);
    }



    void CancelButton()
    {
        if (x == 3)
        {
            buttonNormal.SetActive(false);
            buttonInterstitial.SetActive(true);
        }

        else
        {
            buttonNormal.SetActive(true);
            buttonInterstitial.SetActive(false);
        }

    }

}

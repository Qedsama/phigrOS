using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public GameObject exitui;
    public void exitclickno()
    {
        exitui.SetActive(false);
    }
    public void exitclickyes()
    {
        Application.Quit();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            exitui.SetActive(true);
        }
    }
}

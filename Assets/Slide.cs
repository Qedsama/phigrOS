using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slide : MonoBehaviour
{
    public GameObject Slidebar,randomsong;
    public GameObject[] song = new GameObject[10];
    public const int nsong = 6;
    double value;
    Scrollbar scrollbar;
    Dingshu dingshu;
    int num;
    double[] slide = new double[1000];
    System.Random pick = new System.Random();
    public int nowsong=0;
    public void Randompick()
    {
        int random=pick.Next(1, num);
        randomsong.transform.GetChild(1).GetComponent<Text>().text = dingshu.song[random].name;
        randomsong.transform.GetChild(2).GetComponent<Text>().text = dingshu.song[random].rank+dingshu.song[random].difficulty.ToString("F1");
        randomsong.transform.GetChild(0).GetComponent<Image>().sprite = dingshu.song[random].painting;
    }
    // Start is called before the first frame update
    void Start()
    {
        scrollbar = Slidebar.GetComponent<Scrollbar>();
        dingshu = GameObject.Find("Main Camera").GetComponent<Dingshu>();
        num = dingshu.num;
        for(int i = 0; i < num; i++){slide[i] = i*1.0/(num-nsong+1);}
    }

    // Update is called once per frame
    void Update()
    {
        value = scrollbar.value;
        float mouse = Input.GetAxis("Mouse ScrollWheel");
        if (mouse != 0f){
            scrollbar.value -= 1.0f * mouse/10;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {scrollbar.value += 2*1.0f  / (num - nsong + 1); }
        if (Input.GetKeyDown(KeyCode.UpArrow)) { scrollbar.value -= 2 *1.0f/ (num - nsong + 1); }
        int ii = 0; while (value > slide[ii]&&ii<num-1) { ii++; }
        nowsong = ii;
        for(int i = 0; i < nsong; i++)
        {
            song[i].transform.GetChild(1).GetComponent<Text>().text = dingshu.song[ii + i].name;
            song[i].transform.GetChild(2).GetComponent<Text>().text = dingshu.song[ii + i].rank+dingshu.song[ii + i].difficulty.ToString("F1");
            song[i].transform.GetChild(0).GetComponent<Image>().sprite = dingshu.song[ii + i].painting;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Songnow : MonoBehaviour
{
    Slide slide;
    public double dingshu=0;
    public GameObject songnow,music;
    public int nowsong=0;
    public void Clickbutton(int i)
    {
        nowsong = slide.nowsong+i;
        songnow.transform.GetChild(1).GetComponent<Text>().text = slide.song[i].transform.GetChild(1).GetComponent<Text>().text;
        songnow.transform.GetChild(2).GetComponent<Text>().text = slide.song[i].transform.GetChild(2).GetComponent<Text>().text;
        songnow.transform.GetChild(0).GetComponent<Image>().sprite = slide.song[i].transform.GetChild(0).GetComponent<Image>().sprite;
        dingshu = double.Parse(songnow.transform.GetChild(2).GetComponent<Text>().text.Substring(2));
        
    }
    // Start is called before the first frame update
    void Start()
    {
        slide = GameObject.Find("Scrollbar").GetComponent<Slide>();
    }
}

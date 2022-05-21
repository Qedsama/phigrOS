using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Serializable]
    public struct Players
    {
        public string name;
        public string password;
        public double rks;
        public Dingshu.Song[] b19;
        public Dingshu.Song maxphi;
        public Sprite avatar;
    }
    Players[] allplayer = new Players[100];
    public Players playernow;
    public bool login=false;
    public GameObject playername,playerpassword,error;
    public GameObject Nowname, Nowrks,Nowavatar;
    InputField pname, ppass;
    Text errortext,nowname,nowrks;
    Image nowavatar;
    void nowpre()
    {
        nowname.text = playernow.name;
        nowrks.text = playernow.rks.ToString("F2");
        nowavatar.sprite = playernow.avatar;
    }
    public void getallplayer()
    {
        if (!File.Exists(Application.dataPath + "\\player.json")) return;
        string fileUrl = Application.dataPath + "\\player.json";
        string[] playerdata = new string[100];
        using (StreamReader sr = File.OpenText(fileUrl))
        {
            int i = 0;
            Array.Clear(allplayer, 0, allplayer.Length);
            while (!sr.EndOfStream)
            {
                playerdata[i] = sr.ReadLine();
                allplayer[i] = JsonUtility.FromJson<Players>(playerdata[i]);
                i++;
            }
            sr.Close();
        }
    }
    public void playersignup()
    {
        Players newplayer=new Players();
        newplayer.name = pname.text;
        if (newplayer.name == null) return;
        newplayer.password = ppass.text;
        if (newplayer.password == null) return;
        getallplayer();
        for (int i = 0; i < allplayer.Length; i++)
        {
            if (allplayer[i].name == newplayer.name)
            {
                error.SetActive(true);
                errortext.text = "用户名重复，请更改";
                return;
            }
        }
        string json = JsonUtility.ToJson(newplayer);
        string fileUrl = Application.dataPath + "\\player.json";
        using (StreamWriter sw = new StreamWriter(fileUrl,true))
        {
            sw.WriteLine(json);
            sw.Close();
            sw.Dispose();
        }
        error.SetActive(true);
        errortext.text = "注册成功！";
        login = true;
        playernow = newplayer;
        nowpre();
    }
    public void playerlogin()
    {
        getallplayer();
        for (int i = 0; i < allplayer.Length; i++)
        {
            if (pname.text == allplayer[i].name && ppass.text == allplayer[i].password)
            {
                error.SetActive(true);
                errortext.text = "欢迎您，"+pname.text;
                playernow = allplayer[i];
                login = true;
                nowpre();
                return;
            }
            error.SetActive(true);
            errortext.text = "用户名或密码错误，请重试";
        }
    }
    void updaterks()
    {
        
    }
    void Start()
    {
        pname = playername.GetComponent<InputField>();
        ppass = playerpassword.GetComponent<InputField>();
        errortext = error.GetComponent<Text>();
        nowname = Nowname.GetComponent<Text>();
        nowrks = Nowrks.GetComponent<Text>();
        nowavatar = Nowavatar.GetComponent<Image>();
    }
}

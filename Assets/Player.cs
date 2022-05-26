using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Serializable]
    public class Players
    {
        public string name;
        public string password;
        public double rks;
        public Dingshu.Song[] b19s;
        public double[] b19;
        public Dingshu.Song maxphi;
        public Sprite avatar;
        public Players()
        {
            b19s = new Dingshu.Song[20];
            b19 = new double[20];
            print(this.b19.Length);
        }
    }
    Players[] allplayer = new Players[100];
    public Players playernow;
    public bool login=false;
    public GameObject playername,playerpassword,error;
    public GameObject Nowname, Nowrks,Nowavatar;
    int allnum = 0;
    InputField pname, ppass;
    Text errortext,nowname,nowrks;
    Image nowavatar;
    Client client;
    Dingshu dingshu;
    Slide slide;
    Calculate calculate;
    Songnow Songnow;
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
            for(int j = 0; j < allplayer.Length; j++)
            {
                allplayer[j] = new Players();
            }
            while (!sr.EndOfStream)
            {
                playerdata[i] = sr.ReadLine();
                if (playerdata[i] == null) break;
                allplayer[i] = JsonUtility.FromJson<Players>(playerdata[i]);
                i++;
            }
            allnum = i;
            sr.Close();
        }
    }
    public void pushallplayer()
    {
        for(int i = 0; i < allnum; i++)
        {
            string json = JsonUtility.ToJson(allplayer[i]);
            string fileUrl = Application.dataPath + "\\player.json";
            if(i>0)
            using (StreamWriter sw = new StreamWriter(fileUrl, true))
            {
                sw.WriteLine(json);
                sw.Close();
                sw.Dispose();
            }
            else
            using (StreamWriter sw = new StreamWriter(fileUrl))
            {
                sw.WriteLine(json);
                sw.Close();
                sw.Dispose();
            }
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
        print("allplayer.Length:" + allplayer.Length);
        for (int i = 0; i < allnum; i++)
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
        getallplayer();
        playernow = newplayer;
        nowpre(); client.loginsend(playernow);
    }
    public void playerlogin()
    {
        getallplayer();
        for (int i = 0; i < allnum; i++)
        {
            if (pname.text == allplayer[i].name && ppass.text == allplayer[i].password)
            {
                error.SetActive(true);
                errortext.text = "欢迎您，"+pname.text;
                playernow = allplayer[i];
                login = true;
                nowpre(); client.loginsend(playernow);
                return;
            }
            error.SetActive(true);
            errortext.text = "用户名或密码错误，请重试";
        }
    }
    public void updaterks()
    {
        Dingshu.Song songnow = dingshu.song[slide.nowsong];
        client.rksupdatesend(songnow);
        getallplayer();
        for(int i = 0; i < allnum; i++)
        {
            print("debug:" + allplayer[i].b19.Length);
            if (allplayer[i].name == playernow.name)
            {
                if (calculate.biaoxian == Songnow.dingshu)
                {
                    allplayer[i].maxphi = songnow;
                }
                int jj = -1;
                for(int j = 0; j < 19; j++)
                {
                    if (calculate.biaoxian > allplayer[i].b19[j])
                    {
                        jj++;
                    }
                }
                if (jj == -1) return;
                for(int j = 0; j < jj; j++)
                {
                    allplayer[i].b19s[j] = allplayer[i].b19s[j + 1];
                    allplayer[i].b19[j] = allplayer[i].b19[j + 1];
                }
                allplayer[i].b19s[jj] = songnow;
                allplayer[i].b19[jj] = calculate.biaoxian;
                double sum = 0;
                for (int j = 0; j < 19; j++) sum += allplayer[i].b19[j];
                sum += allplayer[i].maxphi.difficulty;
                allplayer[i].rks = sum / 20;
            }
        }
        pushallplayer();
    }
    void Start()
    {
        pname = playername.GetComponent<InputField>();
        ppass = playerpassword.GetComponent<InputField>();
        errortext = error.GetComponent<Text>();
        nowname = Nowname.GetComponent<Text>();
        nowrks = Nowrks.GetComponent<Text>();
        nowavatar = Nowavatar.GetComponent<Image>();
        client = GetComponent<Client>();
        dingshu = GetComponent<Dingshu>();
        slide = GameObject.Find("Scrollbar").GetComponent<Slide>();
        calculate = GetComponent<Calculate>();
        Songnow = GetComponent<Songnow>();
    }
}

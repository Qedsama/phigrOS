using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dingshu : MonoBehaviour
{
    [Serializable]
    public struct Song
    {
        public double difficulty;
        public string name;
        public string compuser;
        public string rank;
        public Sprite painting;
        public int num;
        public AudioClip mp3;
    }
    TextAsset nam, diff,rank;
    string[] songname, songdiff,songrank=new string[1000];
    public Song[] song = new Song[1000];
    public int num;
    // Start is called before the first frame update
    void Awake()
    {
        nam = Resources.Load("name") as TextAsset;
        diff = Resources.Load("diff") as TextAsset;
        rank = Resources.Load("rank") as TextAsset;
        songrank = rank.text.Split('\n');
        songname = nam.text.Split('\n');
        songdiff = diff.text.Split('\n');
        int n = songname.Length;
        num = n;
        for (int i = 0; i < n; i++)
        {
            song[i].difficulty = double.Parse(songdiff[i]);
            song[i].name = songname[i];
            song[i].compuser = "ÉÐÎ´µ¼Èë";
            song[i].rank = songrank[i];
            song[i].num = i;
            song[i].painting = Resources.Load<Sprite>("P"+(i+1).ToString());
            song[i].mp3 = Resources.Load<AudioClip>("M"+name);
        }
    }
}

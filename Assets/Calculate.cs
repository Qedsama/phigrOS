using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Calculate : MonoBehaviour
{
    public GameObject input, output,accinput;
    InputField inputfield,acci;
    Text outputfield;
    Songnow songnow;
    string nam, lastname;
    string acc1, acc2;double acc=0;
    // Start is called before the first frame update
    void Start()
    {
        inputfield = input.GetComponent <InputField> ();
        outputfield = output.GetComponent<Text>();
        acci = accinput.GetComponent<InputField>();
        songnow = GetComponent<Songnow>();
    }
    double Calc(double dingshu,double acc)
    {
        if (acc <= 55) return 0;
        if (acc > 100) return 0;
        double x = (acc - 55) / 45;
        return x*x*dingshu;
    }
    void Print(double biaoxian)
    {
        outputfield.text = "µ¥ÇúaccÎª£º"+biaoxian.ToString("F2");
    }
    // Update is called once per frame
    void Update()
    {
        acc1 = acci.text;
        if (acc2 != acc1)
        {
            acc2 = acc1;
            if(acc1.Length>0)acc=double.Parse(acc1);
        }
        nam = inputfield.text;
        if (nam != lastname)
        {
            lastname = nam;
        }
        double dingshu = songnow.dingshu;
        double biaoxian = Calc(dingshu, acc);
        Print(biaoxian);
    }
}

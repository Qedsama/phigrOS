using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Socket tcpClient;
    private string serverIP = "127.0.0.1";//服务器ip地址
    private int serverPort = 5000;//端口号
    public void loginsend(Player.Players playernow)
    {
        string json = JsonUtility.ToJson(playernow);
        tcpClient.Send(Encoding.UTF8.GetBytes(json));
    }
    public void rksupdatesend(Dingshu.Song songnow)
    {
        string json = JsonUtility.ToJson(songnow);
        tcpClient.Send(Encoding.UTF8.GetBytes(json));
    }
    public void initgetmessage()
    {

    }
    void Start()
    {
        //1、创建socket
        tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //2、建立一个连接请求
        IPAddress iPAddress = IPAddress.Parse(serverIP);
        EndPoint endPoint = new IPEndPoint(iPAddress, serverPort);
        tcpClient.Connect(endPoint);

        //3、接受、发送消息
        byte[] data = new byte[1024];
        int length = tcpClient.Receive(data);
        var message = Encoding.UTF8.GetString(data, 0, length);

        //发送消息
        string message2 = "Client Say To Hello";
        tcpClient.Send(Encoding.UTF8.GetBytes(message2));
    }
}

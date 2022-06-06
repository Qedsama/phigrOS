using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class Client : MonoBehaviour
{
    private Socket tcpClient;
    private string serverIP = "127.0.0.1";//������ip��ַ
    private int serverPort = 5000;//�˿ں�
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
        //1������socket
        tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        //2������һ����������
        IPAddress iPAddress = IPAddress.Parse(serverIP);
        EndPoint endPoint = new IPEndPoint(iPAddress, serverPort);
        tcpClient.Connect(endPoint);

        //3�����ܡ�������Ϣ
        byte[] data = new byte[1024];
        int length = tcpClient.Receive(data);
        var message = Encoding.UTF8.GetString(data, 0, length);

        //������Ϣ
        string message2 = "Client Say To Hello";
        tcpClient.Send(Encoding.UTF8.GetBytes(message2));
    }
}

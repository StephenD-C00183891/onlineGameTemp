using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public class Networking : MonoBehaviour
{
    public GameObject game;
    public GameObject player;
    public GameObject player2;

    string ip = "127.0.0.1";
    int port = 7000;

    string remote_ip = "127.0.0.1";
    int remote_port = 8000;

    int socketId;
    int remote_socketID;

    int connectionId;
    int channelId;

    public string dir = "";
    public string updatePos;
    public Vector2 updatedPos;

    ConnectionConfig config;

    void Start()
    {
        NetworkTransport.Init();

        config = new ConnectionConfig();
        channelId = config.AddChannel(QosType.Reliable);

        updatedPos = new Vector2();
    }

    public void Setup()
    {
        ip = "127.0.0.1";
        port = 8080;

       // remote_ip = "149.153.106.155";
        remote_ip = "127.0.0.1";
        remote_port = 8000;

       // Debug.Log("remote.port " + remote_port);
      //  Debug.Log("port " + port);

        HostTopology topology = new HostTopology(config, 10);
        socketId = NetworkTransport.AddHost(topology, port);
        Debug.Log("Socket Open. SocketID is: " + socketId);
    }

    public void SetupB()
    {
        ip = "127.0.0.1";
        port = 8000;

       // remote_ip = "127.0.0.1";
        remote_port = 8080;

       // Debug.Log("remote.port " + remote_port);
       // Debug.Log("port " + port);

        HostTopology topology = new HostTopology(config, 10);
        remote_socketID = NetworkTransport.AddHost(topology, port);
        Debug.Log("Socket Open. SocketID is: " + remote_socketID);
    }

    public void Connect()
    {
        byte error;
        connectionId = NetworkTransport.Connect(socketId, remote_ip, remote_port, 0, out error);

        Debug.Log("Connected to server. ConnectionId: " + connectionId);
    }

    void Recieve()
    {
        int recieveSocketId;
        int recieveConnectionId;
        int recieveChannelId;
        byte[] recieveBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;

        NetworkEventType recieveNetworkEvent = NetworkTransport.Receive(out recieveSocketId, out recieveConnectionId, out recieveChannelId, recieveBuffer, bufferSize, out dataSize, out error);

        switch (recieveNetworkEvent)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log("incoming connection event recieved");
                break;
            case NetworkEventType.DataEvent:

                Stream stream = new MemoryStream(recieveBuffer);
                BinaryFormatter formatter = new BinaryFormatter();

                dir = formatter.Deserialize(stream) as string;

                if(dir == "True")
                {
                    Debug.Log("Set to Input Based");
                    game.GetComponent<Game>().inputMsg = true;
                  
                }
                else if (dir == "False")
                {
                    Debug.Log("Set to State Based");
                    game.GetComponent<Game>().inputMsg = false;
                   
                }
                else if (game.GetComponent<Game>().inputMsg == false && dir != "True" && dir != "False")
                {
                    Debug.Log("Recieved Vector " + dir);
                    updatedPos = StringToVector2(dir);
                    player.GetComponent<Player>().setPosition(updatedPos);
                }
                else if (game.GetComponent<Game>().inputMsg == true && dir != "True" && dir != "False")
                {
                    Debug.Log("Recieved Direction " + dir);
                    player.GetComponent<Player>().RemoteMove(dir);
                }
             
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log("remote client event disconnected");
                break;
        }
    }

    public Vector2 StringToVector2(string sVector)
    {

        if (sVector.StartsWith("(") && sVector.EndsWith(")"))
        {
            sVector = sVector.Substring(1, sVector.Length - 2);
        }

        string[] sArray = sVector.Split(',');


        Vector2 result = new Vector2(float.Parse(sArray[0]), float.Parse(sArray[1]));

        return result;
    }


    void Update()
    {
        Recieve();
    }

    public void Send(string inputMsg)
    {
    //    Debug.Log("seinding");
            byte error = 0;
            byte[] buffer = new byte[100];

            Stream stream = new MemoryStream(buffer);
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, inputMsg);

            NetworkTransport.Send(socketId, connectionId, channelId, buffer, (int)stream.Position, out error);

    }
}

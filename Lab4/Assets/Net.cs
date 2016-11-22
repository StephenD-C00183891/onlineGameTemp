using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Collections;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;

public class Net : MonoBehaviour {

    public string myIP;
    public int myPort;

    public string remoteIP;
    public int remotePort;

    int maxConnections = 10;

    ConnectionConfig config;

    int channelId;
    int connectionId;
    int hostId;

    Game game;

    // Use this for initialization
    public void Start () {
        //Debug.Log("Me " + myIP + ":" + myPort);
        //Debug.Log("Remote " + remoteIP + ":" + remotePort);
        game = GetComponent<Game>();
        NetworkTransport.Init();
        config = new ConnectionConfig();
        channelId = config.AddChannel(QosType.Reliable);

    }
    
    public void SetupA()
    {
        game.SetPlayer1();
        //myIP = "127.0.0.1";
        myIP = "149.153.106.154";
        myPort = 8001;

        //remoteIP = "127.0.0.1";
        remoteIP = "149.153.106.155";
        remotePort = 8000;

        HostTopology topology = new HostTopology(config, maxConnections);
        hostId = NetworkTransport.AddHost(topology, myPort);
    }
    public void SetupB()
    {
        game.SetPlayer2();
        //myIP = "127.0.0.1";
        myIP = "149.153.106.155";
        myPort = 8000;

        //remoteIP = "127.0.0.1";
        remoteIP = "149.153.106.154";
        remotePort = 8001;

        HostTopology topology = new HostTopology(config, maxConnections);
        hostId = NetworkTransport.AddHost(topology, myPort);
    }
    public void Connect()
    {
        byte error;
        connectionId = NetworkTransport.Connect(hostId, remoteIP, remotePort, 0, out error);
        Debug.Log("Connected to server. ConnectionId: " + connectionId);
    }
    public void Send(string message)
    {
        string userMessage = message;
        byte error;
        byte[] buffer = new byte[1024];
        Stream stream = new MemoryStream(buffer);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, userMessage);

        channelId = 0;

        NetworkTransport.Send(hostId, connectionId, channelId, buffer, (int)stream.Position, out error);
    }
    public string Receive()
    {
        string message = "";
        int remoteSocketId;
        int remoteConnectionId;
        int remoteChannelId;
        byte[] recBuffer = new byte[1024];
        int bufferSize = 1024;
        int dataSize;
        byte error;
        
        NetworkEventType receivedData = NetworkTransport.Receive(out remoteSocketId, out remoteConnectionId, out remoteChannelId, recBuffer, bufferSize, out dataSize, out error);

        switch (receivedData)
        {
            case NetworkEventType.Nothing:
                break;
            case NetworkEventType.ConnectEvent:
                Debug.Log("incoming connection event received");
                break;
            case NetworkEventType.DataEvent:
                Stream stream = new MemoryStream(recBuffer);
                BinaryFormatter formatter = new BinaryFormatter();
                message = formatter.Deserialize(stream) as string;
                break;
            case NetworkEventType.DisconnectEvent:
                Debug.Log("remote client event disconnected");
                break;
        }
        return message;
    }
}

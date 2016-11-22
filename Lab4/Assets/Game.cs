using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {
    // Use this for initialization
    public Player redPlayer;
    public Player bluePlayer;

    FakeNet fakeNetwork;
    Net network;
    Message msg;

    int id;

    public bool localNet = false;
    public bool stateBased = false;

    float lastPacketSentTimeRed = 0;
    float lastPacketSentTimeBlue = 0;
    float currentTime = 0;

    void Start ()
    {
        network = GetComponent<Net>();
        //fakeNetwork = GetComponent<FakeNet>();
    }
    // Update is called once per frame
    void Update() {
        string message = Receive();
        currentTime = Time.time * 1000;
        //fakeNetwork.ProcessMessages();

        if (!stateBased)
        {
            if (redPlayer.remote == false)
                ProcessInput(bluePlayer, message);
            else
                ProcessInput(redPlayer, message);
        }
        else
        {
            if (redPlayer.remote == false)
                ProcessGame(redPlayer, bluePlayer, message);
            else
                ProcessGame(bluePlayer, redPlayer, message);
        }
    }
    public void SetPlayer1()
    {
        redPlayer.remote = false;
        bluePlayer.remote = true;
    }
    public void SetPlayer2()
    {
        redPlayer.remote = true;
        bluePlayer.remote = false;
    }
    void HandleInput(Player player)
    {
        string message = "";

        if (Input.GetKey(KeyCode.W))
        {
            player.Move(KeyCode.W);
            message += "W";
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.Move(KeyCode.S);
            message += "S";
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.Move(KeyCode.A);
            message += "A";
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.Move(KeyCode.D);
            message += "D";
        }

        if (!stateBased)
        {
            //fakeNetwork.Send(message);
            network.Send(message);
        }
    }
    string Receive()
    {
        string message = network.Receive();
        return message;
    }
    void ProcessGame(Player firstPlayer, Player secondPlayer, string message)
    {
        HandleInput(firstPlayer);

        string positionX = firstPlayer.GetPosition().x.ToString();
        string positionY = firstPlayer.GetPosition().y.ToString();

        string combined = positionX + "," + positionY;
        //fakeNetwork.Send(combined);
        if (currentTime - lastPacketSentTimeRed > 1000 / 30)
        {
            network.Send(combined);
            lastPacketSentTimeRed = currentTime;
        }
        else
        {
            firstPlayer.MoveByPosition(new Vector2(0, 0), false);
        }
        //network.Send(combined);

        string[] splitMessage;
        splitMessage = message.Split(',');

        float newPosX = System.Single.Parse(splitMessage[0]);
        float newPosY = System.Single.Parse(splitMessage[1]);

        secondPlayer.MoveByPosition(new Vector2(newPosX, newPosY));
    }
    void ProcessInput(Player player, string message)
    {
        if (message.Contains("W"))
            player.Move(KeyCode.W);
        if (message.Contains("S"))
            player.Move(KeyCode.S);
        if (message.Contains("A"))
            player.Move(KeyCode.A);
        if (message.Contains("D"))
            player.Move(KeyCode.D);
    }
}

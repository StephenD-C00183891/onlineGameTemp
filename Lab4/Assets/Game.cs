﻿using UnityEngine;
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

    float lastPacketSentTime = 0;
    float currentTime = 0;
    float timeSinceLastMsg = 0;
    const float FPS = (1000.0f / 30.0f) / 1000.0f;

    void Start ()
    {
        network = GetComponent<Net>();
        fakeNetwork = GetComponent<FakeNet>();
    }
    // Update is called once per frame
    void Update() {

        string message = Receive();

        fakeNetwork.ProcessMessages();

        currentTime = Time.time;
        timeSinceLastMsg += (Time.deltaTime * 1000);
        //Debug.Log(timeSinceLastMsg);

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
        string msg = "";

        if (Input.GetKey(KeyCode.W))
        {
            player.Move(KeyCode.W);
            msg += "W";
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.Move(KeyCode.S);
            msg += "S";
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.Move(KeyCode.A);
            msg += "A";
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.Move(KeyCode.D);
            msg += "D";
        }

        if (!stateBased)
        {
            network.Send(msg);
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
        if (currentTime - lastPacketSentTime > FPS)
        {
            fakeNetwork.Send(combined);
            lastPacketSentTime = currentTime;
        }
        //network.Send(combined);
        if (message != "")
        {
            string[] splitMessage;
            splitMessage = message.Split(',');

            float newPosX = float.Parse(splitMessage[0]);
            float newPosY = float.Parse(splitMessage[1]);

            if (timeSinceLastMsg <= 2000 )
            {
                Debug.Log("interpolate");
                //secondPlayer.MoveBy(new Vector2(newPosX, newPosY));
                secondPlayer.MoveByPosition(new Vector2(newPosX, newPosY));
                timeSinceLastMsg = 0;
            }
            else if (timeSinceLastMsg > 2000)
            {
                Debug.Log("snap");
                secondPlayer.MoveBy(new Vector2(newPosX, newPosY));
                timeSinceLastMsg = 0;
                
            }
        }
        secondPlayer.ManualUpdate(FPS);

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

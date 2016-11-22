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

    void Start ()
    {
        network = GetComponent<Net>();
        //fakeNetwork = GetComponent<FakeNet>();
    }
    // Update is called once per frame
    void Update() {
        string message = Receive();
        //fakeNetwork.ProcessMessages();

        if (!stateBased)
        {
            //if you're the red player
            if (redPlayer.remote == false)
            {
                if (message.Contains("W"))
                    bluePlayer.Move(KeyCode.W);
                if (message.Contains("S"))
                    bluePlayer.Move(KeyCode.S);
                if (message.Contains("A"))
                    bluePlayer.Move(KeyCode.A);
                if (message.Contains("D"))
                    bluePlayer.Move(KeyCode.D);
                    //bluePlayer.Move((KeyCode)System.Enum.Parse(typeof(KeyCode), message));
                HandleInput(redPlayer);
            }
            //if you're the blue player
            else
            {
                //if (message != "")
                //    redPlayer.Move((KeyCode)System.Enum.Parse(typeof(KeyCode), message));
                if (message.Contains("W"))
                    redPlayer.Move(KeyCode.W);
                if (message.Contains("S"))
                    redPlayer.Move(KeyCode.S);
                if (message.Contains("A"))
                    redPlayer.Move(KeyCode.A);
                if (message.Contains("D"))
                    redPlayer.Move(KeyCode.D);

                HandleInput(bluePlayer);
            }
        }
        else
        {
            //if you're the red player
            if (redPlayer.remote == false)
            {
                HandleInput(redPlayer);
                
                string positionX = redPlayer.GetPosition().x.ToString();
                string positionY = redPlayer.GetPosition().y.ToString();

                string combined = positionX + "," + positionY;
                //fakeNetwork.Send(combined);
                network.Send(combined);

                string[] splitMessage;
                splitMessage = message.Split(',');

                Vector2 newPosition = new Vector2();

                newPosition.x = float.Parse(splitMessage[0]);
                newPosition.y = float.Parse(splitMessage[1]);

                bluePlayer.MoveByPosition(newPosition);
            }
            //if you're the blue player
            else
            {
                HandleInput(bluePlayer);

                string positionX = bluePlayer.GetPosition().x.ToString();
                string positionY = bluePlayer.GetPosition().y.ToString();

                string combined = positionX + "," + positionY;
                //fakeNetwork.Send(combined);
                network.Send(combined);

                string[] splitMessage;
                splitMessage = message.Split(',');

                Vector2 newPosition = new Vector2();
                newPosition.x = float.Parse(splitMessage[0]);
                newPosition.y = float.Parse(splitMessage[1]);

                redPlayer.MoveByPosition(newPosition);
            }
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
}

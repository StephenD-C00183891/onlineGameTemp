using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public Player player;
    public Player player2;

    public FakeNet fakeNet;

    public bool inputMsg = false;
    public string playerCommand;
    
    void Start()
    {
        
    }

    void Update()
    {
        fakeNet.ProcessMessages();
        if (Input.GetKey("r"))
        {
            inputMsg = !inputMsg;

            fakeNet.Send(inputMsg.ToString());
        }
        if (inputMsg == true)
        {
            if (Input.GetKey("w"))
            {
                player.MoveUp();
                fakeNet.Send(KeyCode.W.ToString());
            }

            if (Input.GetKey("s"))
            {
                player.MoveDown();
                fakeNet.Send(KeyCode.S.ToString());
            }

            if (Input.GetKey("a"))
            {
                player.MoveLeft();
                fakeNet.Send(KeyCode.A.ToString());
            }

            if (Input.GetKey("d"))
            {
                player.MoveRight();
                fakeNet.Send(KeyCode.D.ToString());
            }

            if (Input.GetKey("left"))
            {
                fakeNet.Send(KeyCode.LeftArrow.ToString());
            }

            if (Input.GetKey("right"))
            {
                fakeNet.Send(KeyCode.RightArrow.ToString());
            }

            if (Input.GetKey("up"))
            {
                fakeNet.Send(KeyCode.UpArrow.ToString());
            }

            if (Input.GetKey("down"))
            {
                fakeNet.Send(KeyCode.DownArrow.ToString());
            }
        }
        else if (inputMsg == false)
        {
            if (Input.GetKey("w"))
            {
                player.MoveUp();
                fakeNet.Send(player.playerPos.ToString());
            }

            if (Input.GetKey("s"))
            {
                player.MoveDown();
                fakeNet.Send(player.playerPos.ToString());
            }

            if (Input.GetKey("a"))
            {
                player.MoveLeft();
                fakeNet.Send(player.playerPos.ToString());
            }

            if (Input.GetKey("d"))
            {
                player.MoveRight();
                fakeNet.Send(player.playerPos.ToString());
            }
        }
    }
}

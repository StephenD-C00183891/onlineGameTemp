using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;


public class Game : MonoBehaviour
{
    public GameObject player;
    public GameObject player2;
    public GameObject net;
    public GameObject fakeNet;

    public bool inputMsg = false;
    public string playerCommand;
    
    void Start()
    {
        
    }

    void Update()
    {

        fakeNet.GetComponent<FakeNet>().ProcessMessage();

        if (Input.GetKey("r"))
        {
            inputMsg = !inputMsg;

            fakeNet.GetComponent<FakeNet>().Send(inputMsg.ToString());
        }
        if (inputMsg == true)
        {
            if (Input.GetKey("w"))
            {
                player.GetComponent<Player>().MoveUp();
                fakeNet.GetComponent<FakeNet>().Send(KeyCode.W.ToString());
            }

            if (Input.GetKey("s"))
            {
                player.GetComponent<Player>().MoveDown();
                fakeNet.GetComponent<FakeNet>().Send(KeyCode.S.ToString());
            }

            if (Input.GetKey("a"))
            {
                player.GetComponent<Player>().MoveLeft();
                fakeNet.GetComponent<FakeNet>().Send(KeyCode.A.ToString());
            }

            if (Input.GetKey("d"))
            {
                player.GetComponent<Player>().MoveRight();
                fakeNet.GetComponent<FakeNet>().Send(KeyCode.D.ToString());
            }

            if (Input.GetKey("left"))
            {
                fakeNet.GetComponent<FakeNet>().Send(KeyCode.LeftArrow.ToString());
            }

            if (Input.GetKey("right"))
            {
                fakeNet.GetComponent<FakeNet>().Send(KeyCode.RightArrow.ToString());
            }

            if (Input.GetKey("up"))
            {
                fakeNet.GetComponent<FakeNet>().Send(KeyCode.UpArrow.ToString());
            }

            if (Input.GetKey("down"))
            {
                fakeNet.GetComponent<FakeNet>().Send(KeyCode.DownArrow.ToString());
            }
        }
        else if (inputMsg == false)
        {
            if (Input.GetKey("w"))
            {
                player.GetComponent<Player>().MoveUp();
                fakeNet.GetComponent<FakeNet>().Send(player.GetComponent<Player>().playerPos.ToString());
            }

            if (Input.GetKey("s"))
            {
                player.GetComponent<Player>().MoveDown();
                fakeNet.GetComponent<FakeNet>().Send(player.GetComponent<Player>().playerPos.ToString());
            }

            if (Input.GetKey("a"))
            {
                player.GetComponent<Player>().MoveLeft();
                fakeNet.GetComponent<FakeNet>().Send(player.GetComponent<Player>().playerPos.ToString());
            }

            if (Input.GetKey("d"))
            {
                player.GetComponent<Player>().MoveRight();
                fakeNet.GetComponent<FakeNet>().Send(player.GetComponent<Player>().playerPos.ToString());
            }
        }
    }
}

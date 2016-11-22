using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Message {


    //public char charArray[];
    public string charArray;
    public float timeStamp;

    public Message(string input, float time)
    {
        charArray = input;
        timeStamp = time;
    }
}

using UnityEngine;
using System.Collections;

public class Message {

    string content;
    float sendTimestamp;

    public Message()
    {
        
    }
    public void SetMessage(string message)
    {
        content = message;
    }
    public string GetMessage()
    {
        return content;
    }
    public void SetTime(float time)
    {
        sendTimestamp = time;
    }
    public float GetTime()
    {
        return sendTimestamp;
    }

}

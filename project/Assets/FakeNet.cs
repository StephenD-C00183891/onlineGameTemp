using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


public class FakeNet : MonoBehaviour {

    public int latency = 0;
    public int jitter = 0;
    public int packetLoss = 0;

    public GameObject netObj;
    
    public float currentTime;

   // int packetLossNo = UnityEngine.Random.Range
  //  Random rnd = new Random();

    List<Message> messages;

	// Use this for initialization
	void Start () {
        messages = new List<Message>();
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime * 1000;

       // ProcessMessage();
	}

    public void ProcessMessage()
    { 
        for (int i = 0; i < messages.Count; i++)
        {
            //Packet Loss
            packetLoss = UnityEngine.Random.Range(1, 10);
            //Debug.Log(messages[i]);
            if (currentTime >= messages[i].timeStamp && packetLoss > 3)
            {

                netObj.GetComponent<Networking>().Send(messages[i].charArray);
                messages.RemoveAt(i);
               // Debug.Log(messages.Count);
            }
            else
            {
                Debug.Log("Packet Lost");
            }
        }
    }

    public void Send(string inputMsg)
    {
        //Comment out for Latency, Leave in for Jitter.
        if (jitter > 0)
        {
            latency = UnityEngine.Random.Range(500, jitter);
        }
        float delayTime = currentTime + latency;
        messages.Add(new Message(inputMsg, delayTime));
    }
}

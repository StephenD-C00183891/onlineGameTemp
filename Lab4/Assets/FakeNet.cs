﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FakeNet : MonoBehaviour {

    float latency;
    float jitter;
    float packetLoss;
    float currentTime;
    float lastPacketSendTime;

    Net network;
    List<Message> packets;

    void Start()
    {
        network = GetComponent<Net>();
        packets = new List<Message>();
        latency = 0;
        jitter = 0;
        packetLoss = 0;
    }
    public void Send(string message)
    {
        Message m = new Message();
        float currentTime = Time.time * 1000;
        currentTime += latency + Random.Range(0, jitter);

        m.SetTime(currentTime);
        m.SetMessage(message);

        packets.Add(m);
    }
    public void ProcessMessages()
    {
        currentTime = Time.time * 1000;
        for (int i = 0; i < packets.Count; i++)
        {
            if (packets[i].GetTime() < currentTime)
            {
                int num = Random.Range(0, 101);

                if (num > packetLoss)
                {
                    network.Send(packets[i].GetMessage());
                    lastPacketSendTime = currentTime;
                }
                packets.RemoveAt(i);
            }
        }
    }

	// Update is called once per frame
	void Update () {
        
	}
}

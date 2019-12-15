using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO.Ports;
using System.Threading;

public class Send_Angle : MonoBehaviour
{
    public GameObject background;

    public string portName = "COM4";
    public int baudRate = 115200;



    private Queue rxQueue;

    private SerialPort stream;

    public void Start()
    {
        // Creates and starts the thread
        rxQueue = Queue.Synchronized(new Queue());
    }

    public void Send_trhead()
    {
        /*thread = new Thread(Send_Angles);
        thread.Start();*/
    }


    public void Send_Angles()
    {
        uint j = 2;
        int[] buffer = new int[60];
        Main m = background.GetComponent<Main>();
        GameObject[] clock_array = m.get_Array_Clock();
        buffer[0] = 369; //start signal
        buffer[1] = clock_array.Length;
        for (uint i = 0; i < clock_array.Length; i++)
        {
            GameObject parent = clock_array[i];
            Small_Hand_Clock sclock = parent.GetComponentInChildren<Small_Hand_Clock>();
            Big_Hand_Clock bclock = parent.GetComponentInChildren<Big_Hand_Clock>();
            buffer[j] = (int)sclock.angle +180 ; //cast to int+ get rid of negative angles
            j++;
            buffer[j] = (int)bclock.angle +180 ;
            j++;
        }
        buffer[j] = 420; //stop signal
        stream = new SerialPort(portName,
                                baudRate,
                                Parity.None,
                                8,
                                StopBits.One);
        //stream.ReadTimeout = 200;
        stream.Open();
        if(stream.IsOpen)
        {
            print("LE PORT EST OUVERT");
        }

        byte[] sendbytes = new byte[60];
        j = 0;
        int k = 0;
        for(k=0; k<buffer.Length; k++)
        {
            
            sendbytes[j] = (byte)(buffer[k]/256);
            j++;
            sendbytes[j] = (byte)(buffer[k] & 0xff);
            j++;
            if (buffer[k] == 420) break;
        }


    
        stream.Write(sendbytes, 0, (2*k)+2);

        stream.Close();

    }

    private void OnApplicationQuit()
    {
        stream.Close();
    }

    public void ThreadLoop()
    {
        // The code of the thread goes here...
        stream = new SerialPort(portName,
                                baudRate,
                                Parity.None,
                                8,
                                StopBits.One);
        //stream.ReadTimeout = 200;
        stream.Open();

        while (true)
        {
            //string result = ReadFromUC((timeout));
            string result = stream.ReadByte().ToString();
            if (result != null)
                rxQueue.Enqueue(result);
        }
    }

    public string ReadUART()
    {
        if (rxQueue.Count == 0)
            return null;

        return (string)rxQueue.Dequeue();
    }


}

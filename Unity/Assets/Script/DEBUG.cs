using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DEBUG : MonoBehaviour
{
    public void Test()
    {
        uint i;
        int nb;
        GameObject[] clock_array = GameObject.FindGameObjectsWithTag("clock");
        
        nb = clock_array.Length;//clock_array.Length;

        for (i = 0; i < nb; i++)
        {
            GameObject parent = clock_array[i];
            Small_Hand_Clock sclock = parent.GetComponentInChildren<Small_Hand_Clock>(); 
            Big_Hand_Clock bclock = parent.GetComponentInChildren<Big_Hand_Clock>();
            print("Clock " + (i + 1).ToString() + " big angle = " + (bclock.angle + 180f).ToString());
            print("Clock " + (i+1).ToString() + " small angle = " + (sclock.angle +180f).ToString());
             
           
        }
    }

}

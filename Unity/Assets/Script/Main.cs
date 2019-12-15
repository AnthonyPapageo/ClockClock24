using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{
    public uint i = 1;
    public bool[,] matrice = new bool[16,16];

    private void Update()
    {
        CheckSurroundings();
    }

    public void incrementCounter()
    {
        i++;
    }

    public void decrementCounter()
    {
        i--;
    }

    private void Start()
    {
        matrice[0,0] = true;
    }

    public void CheckSurroundings() //Check everywhere of need to disable the plus
    {
        GameObject[] clock_array = GameObject.FindGameObjectsWithTag("clock");
        uint x, y;
        for (uint i = 0; i < clock_array.Length; i++)
        {
            bool b_enable_right = true;
            bool b_enable_top = true;
            Text_Changer t_clock_to_check = clock_array[i].GetComponentInChildren<Text_Changer>(); //the one we want to check
            x = t_clock_to_check.x; //get the base clock coordonates
            y = t_clock_to_check.y;
            for (uint j = 0; j < clock_array.Length; j++)
            {
                Text_Changer t_clock = clock_array[j].GetComponentInChildren<Text_Changer>();
                if (t_clock.x == (x + 1) && (t_clock.y == y)) //there is a clock on the right
                {
                    b_enable_right = false;
                }
                if (t_clock.y == (y + 1) && (t_clock.x == x)) //there is a clock on top 
                {
                    b_enable_top = false;
                }
            }
            t_clock_to_check.EnableRightPlus(b_enable_right);
            t_clock_to_check.EnableTopPlus(b_enable_top);
        }
    }

    public uint getNextName()
    {
        uint result = 2; //first name created is Clock_2
        GameObject[] clock_array = GameObject.FindGameObjectsWithTag("clock");
        //first clock is clockUI
        for (uint i = 1; i<clock_array.Length; i++) //get a name
        {                   
            string temp = "Clock_" + (i+1).ToString();
            for(uint j = 1; j < clock_array.Length; j++) //compare with everyclock
            {
                if (!string.Equals(temp, clock_array[j]))
                {

                }
            }
            
                
        }

        return result;
    }

    public GameObject[] get_Array_Clock()
    {
        GameObject[] clock_array = GameObject.FindGameObjectsWithTag("clock");
        return clock_array;
    }

}

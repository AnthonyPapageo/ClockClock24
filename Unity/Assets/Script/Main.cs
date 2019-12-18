using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Main : MonoBehaviour
{
    public int i = 1;
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

    public int getNextName()
    {
        int temp = 1;
        bool found = false;
        GameObject[] clock_array = GameObject.FindGameObjectsWithTag("clock");
        //first clock is clockUI
        if(clock_array.Length == 1 )
        {
            return 2; //we create clock2
        }
        for (int i = 1; i<32; i++) //get a name
        {
            GameObject clock = GameObject.Find("Clock_" + i.ToString());
            if (clock == null)
            {
                temp = i;
                found = true;
                break;
            }
        }
        if(found) //we found a hole in the name
        {
            return temp;
        }
        else //it's the last clock
        {
            return clock_array.Length + 1;
        }

        
    }

    public GameObject[] get_Array_Clock()
    {
        GameObject[] clock_array = GameObject.FindGameObjectsWithTag("clock");
        return clock_array;
    }


}

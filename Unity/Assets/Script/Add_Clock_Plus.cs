using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Add_Clock_Plus : MonoBehaviour
{
    public GameObject sampleObject;
    public GameObject background;

    public GameObject c; //canvas

    public void AddObjectRight()
    {
   
        GameObject base_clock;
        Vector3 base_position;

        c = GameObject.FindWithTag("BaseCanvas");
        Main m = background.GetComponent<Main>();

 
        Transform base_transform = gameObject.transform.parent;
        base_clock = base_transform.gameObject;
        base_position = base_clock.transform.position;
        base_position += 100 * Vector3.right;  

        m.incrementCounter(); 


        var clock = Instantiate(sampleObject, base_position, Quaternion.identity, c.transform);
        clock.name = "Clock_" + m.i.ToString();
        Text_Changer t_newclock = clock.GetComponentInChildren<Text_Changer>();
        t_newclock.ChangeText(m.i.ToString());
        Text_Changer t_oldlock = base_clock.GetComponentInChildren<Text_Changer>();

        t_newclock.x = t_oldlock.x + 1; //add one on the right
        t_newclock.y = t_oldlock.y;
        m.matrice[t_newclock.x, t_newclock.y] = true;

        GameObject redcross = GetChildWithName(clock, "Delete");
        redcross.SetActive(true);

        m.CheckSurroundings();
        clock.transform.localScale = new Vector3(40f, 40f, 40f);

    }

    public void AddObjectTop()
    {
        GameObject base_clock;
        Vector3 base_position;

        c = GameObject.FindWithTag("BaseCanvas");
        Main m = background.GetComponent<Main>();
  
        Transform base_transform = gameObject.transform.parent;
        base_clock = base_transform.gameObject;
        base_position = base_clock.transform.position;
        base_position += 100 * Vector3.up;

        m.incrementCounter();


        var clock = Instantiate(sampleObject, base_position, Quaternion.identity, c.transform);
        clock.name = "Clock_" + m.i.ToString();
        Text_Changer t_newclock = clock.GetComponentInChildren<Text_Changer>();
        t_newclock.ChangeText(m.i.ToString());
        Text_Changer t_oldlock = base_clock.GetComponentInChildren<Text_Changer>();

        t_newclock.x = t_oldlock.x; //add one on the right
        t_newclock.y = t_oldlock.y + 1;
        m.matrice[t_newclock.x, t_newclock.y] = true;


        GameObject redcross = GetChildWithName(clock, "Delete");
        redcross.SetActive(true);

        m.CheckSurroundings();
        clock.transform.localScale = new Vector3(40f, 40f, 40f);
    }

    GameObject GetChildWithName(GameObject obj, string name)
    {
        Transform trans = obj.transform;
        Transform childTrans = trans.Find(name);
        if (childTrans != null)
        {
            return childTrans.gameObject;
        }
        else
        {
            return null;
        }
    }

    
}

    
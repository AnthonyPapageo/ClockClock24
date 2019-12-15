using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_Creator : MonoBehaviour
{
    public GameObject sampleObject;
    private uint i = 0;

    public Canvas c;

    public void AddObject()
    {
        i++;
        Text_Changer text = sampleObject.GetComponentInChildren<Text_Changer>();
        text.ChangeText("Clock_" + i.ToString());
        var clock = Instantiate(sampleObject, Vector3.zero, Quaternion.identity, c.transform);
        clock.name = "Clock_" + i.ToString(); ;

        clock.transform.localScale = new Vector3(40f, 40f, 40f);
        
    }

}

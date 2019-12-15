using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_Drag : MonoBehaviour
{
    public GameObject Clock;
    public GameObject Text;
    private void OnMouseDrag()
    {
        var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        pos.z = 0f;
        Clock.transform.position = pos;
        Text.transform.position = pos + (90 * Vector3.down) + (20 * Vector3.right) ;
    }
}

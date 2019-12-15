using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Big_Hand_Clock : MonoBehaviour
{
    public float speed = 90f;
    public float angle = 0f;

    private void OnMouseDrag()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, speed * Time.deltaTime);
    }
}

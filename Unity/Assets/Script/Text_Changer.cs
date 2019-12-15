using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Text_Changer : MonoBehaviour
{
    public Text txt;
    public GameObject background;
    public uint x;
    public uint y;

    public void ChangeText(string s)
    {
        txt.text = s;
    }

    public void Die()
    {
        Destroy(gameObject);
        Main m = background.GetComponent<Main>();
        m.matrice[x, y] = false;
        //m.decrementCounter();
    }

    public void EnableTopPlus(bool b)
    {
        Transform trans = gameObject.transform;
        Transform plus = trans.Find("Plus haut");
        plus.gameObject.SetActive(b);
    }

    public void EnableRightPlus(bool b)
    {
        Transform trans = gameObject.transform;
        Transform plus = trans.Find("Plus droite");
        plus.gameObject.SetActive(b);
    }



}
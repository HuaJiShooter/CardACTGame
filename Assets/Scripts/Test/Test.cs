using System;
using System.Collections;
using System.Collections.Generic;
using MyFrame.Single;
using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other);
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombHit : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemy")
        {
            Controller c = other.gameObject.GetComponent<Controller>();
            if (c) {
                c.decreaseHealth();
            }
        }
    }
}

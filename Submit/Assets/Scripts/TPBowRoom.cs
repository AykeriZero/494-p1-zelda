using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPBowRoom : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.transform.position = new Vector3(-15,-8,0);
            GameObject c = GameObject.Find("Main Camera");
            c.transform.position = new Vector3(-10,-8,-10);
        }
    }
}

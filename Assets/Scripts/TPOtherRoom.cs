using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPOtherRoom : MonoBehaviour
{
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            other.gameObject.transform.position = new Vector3(22.5f,60f,0);
            GameObject.Find("Main Camera").transform.position = new Vector3(23.48f,61.92f,-10f);
        }
    }
}

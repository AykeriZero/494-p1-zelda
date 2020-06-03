using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCustomRoom : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha4)){
            triggerCustom();
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "weapon") {
            triggerCustom();
        }
    }

    void triggerCustom() {
        GameObject.Find("Player").transform.position = new Vector3(-22.5f,0,0);
        GameObject c = GameObject.Find("Main Camera");
        c.transform.position = new Vector3(-22.38f,6.83f,-10);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerBlade : MonoBehaviour {
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            transform.GetChild(0).gameObject.GetComponent<BladeTrap>().start_blades();
        }
    }
}

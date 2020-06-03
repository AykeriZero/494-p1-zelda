using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonBallHit: MonoBehaviour {
    private DragonMovement gm;

    void Start() {
        gm = transform.parent.gameObject.GetComponent<DragonMovement>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == transform.parent.gameObject) {
            return;
        }
        gm.boom_active = false;
    }
}

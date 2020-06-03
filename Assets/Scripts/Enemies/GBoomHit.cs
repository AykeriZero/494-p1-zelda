using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GBoomHit : MonoBehaviour {
    private GoriyaMovement gm;

    void Start() {
        gm = transform.parent.gameObject.GetComponent<GoriyaMovement>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject == transform.parent.gameObject && gm.rebounding) {
            gm.boom_active = false;
            return;
        }

        if (other.gameObject == transform.parent.gameObject) {
            return;
        }

        gm.rebounding = true;
    }
}

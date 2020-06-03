using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrapTrigger : MonoBehaviour
{
    public Vector3 direction;
    public float distance;

    private BladeTrap bt;

    void Start() {
        bt = transform.parent.gameObject.GetComponent<BladeTrap>();
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Player") {
            bt.start_blades(direction, distance);
        }
    }
}

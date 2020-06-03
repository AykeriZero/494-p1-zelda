using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordBeamHit : MonoBehaviour
{
    public int damage = 1;

    private UseSword sword_data;

    // Start is called before the first frame update
    void Start() {
        sword_data = GameObject.Find("Weapons").GetComponent<UseSword>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Sword" || other.gameObject.name == "Player") {
            return;
        }
        if (other.gameObject.tag == "enemy") {
            Controller c = other.gameObject.GetComponent<Controller>();
            if (c) {
                c.decreaseHealth();
            }
        }

        sword_data.active_sword_beam = false;
    }
}

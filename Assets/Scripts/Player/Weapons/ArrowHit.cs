using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowHit : MonoBehaviour
{
    public bool active;
    public int damage = 1;

    // Start is called before the first frame update
    void Start() {
        active = true;
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Sword" || other.gameObject.name == "Player") {
            return;
        }
        if (other.gameObject.tag == "enemy") {
            other.gameObject.GetComponent<Controller>().decreaseHealth();
        }

        active = false;
    }
    
}

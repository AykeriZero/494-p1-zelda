using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangHit : MonoBehaviour
{
    public float stun_time = 1;
    private UseBoomerang boom_data;

    // Start is called before the first frame update
    void Start() {
        boom_data = GameObject.Find("Weapons").GetComponent<UseBoomerang>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.name == "Player" && boom_data.rebounding) {
            boom_data.active = false;
            return;
        }

        if (other.gameObject.name == "Player") {
            return;
        }

        if (other.gameObject.tag == "enemy") {
            Controller enemy_c = other.gameObject.GetComponent<Controller>();
            enemy_c.allowed_to_move = false;
            boom_data.stun(enemy_c);
            other.gameObject.GetComponent<Controller>().decreaseHealth();
        }

        boom_data.rebounding = true;

    }
}

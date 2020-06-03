using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseIce : MonoBehaviour {
    public Transform ice_prefab;

    private Controller player_c;
    private GameObject player;

    void Start() {
        player = GameObject.Find("Player");
        player_c = player.GetComponent<Controller>();
    }

    public void attack() {
        Instantiate(ice_prefab, player.transform.position + getIceDirection(), Quaternion.identity);
    }

    Vector3 getIceDirection() {
        switch (player_c.facing_direction) {
            case Controller.Direction.Left:
                return new Vector3(-1, 0, 0);
            case Controller.Direction.Right:
                return new Vector3(1, 0, 0);
            case Controller.Direction.Up:
                return new Vector3(0, 1, 0);
            case Controller.Direction.Down:
                return new Vector3(0, -1, 0);
            default:
                Debug.Log("Invalid Direction");
                return Vector3.zero;
        }
    }
}

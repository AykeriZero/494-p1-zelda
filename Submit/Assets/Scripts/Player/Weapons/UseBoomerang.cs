using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseBoomerang : MonoBehaviour {
    public bool rebounding;
    public bool active;
    public float speed = 8f;

    public float h_dist_from_center = 0.5f;
    public float v_dist_top = 0.5f;
    public float v_dist_bottom = 0.5f;

    private GameObject player_object;
    private Controller player_c;
    private GameObject boomerang;

    void Start() {
        player_object = GameObject.Find("Player");
        player_c = player_object.GetComponent<Controller>();
        boomerang = GameObject.Find("Boomerang");
    }

    public void attack() {
        if (!active) {
            StartCoroutine(throwBoomerang());
        }
    }

    IEnumerator throwBoomerang() {
        active = true;
        boomerang.SetActive(true);
        rebounding = false;

        positionBoomerang(boomerang);
        Vector3 for_dir = getBoomForwardDirection();

        while (!rebounding) {
            boomerang.transform.position = boomerang.transform.position + for_dir * speed * Time.deltaTime;
            yield return null;
        }


        while (active) {
            boomerang.transform.position = Vector3.MoveTowards(boomerang.transform.position,
                player_object.transform.position, Time.deltaTime * speed);
            yield return null;
        }
        boomerang.SetActive(false);
    }

    Vector3 getBoomForwardDirection() {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0) {
            return new Vector3(horizontal, vertical, 0);
        }

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

    void positionBoomerang(GameObject s) {
        switch (player_c.facing_direction) {
            case Controller.Direction.Left:
                s.transform.position = player_object.transform.position - new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Right:
                s.transform.position = player_object.transform.position + new Vector3(h_dist_from_center, 0, 0);
                break;
            case Controller.Direction.Up:
                s.transform.position = player_object.transform.position + new Vector3(0, v_dist_top, 0);
                break;
            case Controller.Direction.Down:
                s.transform.position = player_object.transform.position - new Vector3(0, v_dist_bottom, 0);
                break;
            default:
                Debug.Log("Invalid Facing Direction");
                break;
        }
    }

    public void stun(Controller c) {
        StartCoroutine(unstun(c));
    }

    IEnumerator unstun(Controller c) {
        yield return new WaitForSeconds(1);
        c.allowed_to_move = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : MonoBehaviour {

    public bool active;
    public float speed = 4f;

    private float dist_traveled;
    private float position;
    private bool swinging_forward;

    private Vector3 starting_position;

    void Start() {
        active = false;
        starting_position = transform.position;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            start_blades(new Vector3(0, -1, 0), 2);
        }
    }

    public void start_blades(Vector3 direction, float distance)
    {
        if (active) {
            return;
        }
        active = true;

        StartCoroutine(swing(direction, distance));
    }

    IEnumerator swing(Vector3 direction, float distance) {
        dist_traveled = 0;
        swinging_forward = true;
        set_position(direction);

        // swinging forward
        while (dist_traveled < distance) {
            update_position_dist_traveled(direction);
            transform.position = transform.position + direction * speed * Time.deltaTime;
            yield return null;
        }

        // swinging backward
        swinging_forward = false;
        while (dist_traveled > 0) {
            update_position_dist_traveled(direction);
            transform.position = transform.position - direction * speed * Time.deltaTime;
            yield return null;
        }

        // reset position to avoid any unintentional shifts
        transform.position = starting_position;
        active = false;
    }

    private void set_position(Vector3 direction) {
        if (direction.y == 0) {
            position = transform.position.x;
        } else if (direction.x == 0) {
            position = transform.position.y;
        } else {
            Debug.Log("Issue with Direction in BladeTrigger");
        }
    }

    private void update_position_dist_traveled(Vector3 direction) {
        float current_position = 1f;

        if (direction.y == 0) {
            current_position = transform.position.x;
        } else if (direction.x == 0) {
            current_position = transform.position.y;
        } else {
            Debug.Log("Issue with Direction in BladeTrigger");
        }

        if (swinging_forward) {
            dist_traveled += Mathf.Abs(position - current_position);
        } else {
            dist_traveled -= Mathf.Abs(position - current_position);
        }

        position = current_position;
    }
}

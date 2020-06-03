using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeTrap : MonoBehaviour {

    private float start_y;
    public bool active;

    void Start() {
        start_y = transform.position.y;
        active = false;
    }

    public void start_blades()
    {
        if (active) {
            return;
        }
        active = true;
        StartCoroutine(swing());
    }

    IEnumerator swing() {
        while (transform.position.y - start_y < 7) {
            transform.position = transform.position + new Vector3(0,1,0) * 4 * Time.deltaTime;
            yield return null;
        }

        while (transform.position.y - start_y > 0) {
            transform.position = transform.position - new Vector3(0,1,0) * 4 * Time.deltaTime;
            yield return null;
        }

        active = false;
    }
}

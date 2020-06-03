using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFlame : MonoBehaviour
{
    public float speed = 4f;

    public void throwFlame(Vector3 direction) {
        StartCoroutine(flamethrowing(direction));
    }

    IEnumerator flamethrowing(Vector3 direction) {
        while (true) {
            transform.position = transform.position + direction * speed * Time.deltaTime;
            yield return null;
        }
    }

    void OnCollisionEnter(Collision other) {
        Destroy(gameObject);
    }

}

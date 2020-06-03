using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowFireProjectiles : MonoBehaviour
{
    public Transform projectile_prefab;
    public Vector3 direction;
    public float projectile_time = 1f;

    void Start() {
        StartCoroutine(throwFlames());
    }

    IEnumerator throwFlames() {
        while (true) {
            Transform c = Instantiate(projectile_prefab, transform.position + direction * 1f, Quaternion.identity);
            c.gameObject.GetComponent<ThrowFlame>().throwFlame(direction);
            yield return new WaitForSeconds(projectile_time);
        }
    }

}

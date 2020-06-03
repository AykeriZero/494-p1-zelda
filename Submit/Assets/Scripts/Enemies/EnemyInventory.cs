using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInventory : MonoBehaviour {
    private Controller c;

    public GameObject holding;

    // Start is called before the first frame update
    void Start() {
        c = GetComponent<Controller>();
        c.onDeath += dropItem;
    }

    void dropItem() {
        if (holding != null) {
            Instantiate(holding, transform.position, Quaternion.identity);
        }
    }
}

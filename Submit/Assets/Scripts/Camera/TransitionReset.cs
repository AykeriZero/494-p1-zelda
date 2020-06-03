using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionReset : MonoBehaviour
{

    public void reset() {
        foreach (Transform child in transform) {
            TransitionCamera c = child.gameObject.GetComponent<TransitionCamera>();
            c.reset();
        }

    }

}

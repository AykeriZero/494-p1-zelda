using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRoomData: MonoBehaviour {
    public GameObject transition_north;
    public GameObject transition_west;
    public GameObject transition_east;
    public GameObject transition_south;

    void Start() {
        if (transition_north) {
            TransitionCamera n = transition_north.GetComponent<TransitionCamera>();
            n.transitionDownRightEnd += activate_enemies;
            n.transitionUpLeftBegin += deactivate_enemies;
        }
        if (transition_west) {
            TransitionCamera w = transition_west.GetComponent<TransitionCamera>();
            w.transitionDownRightEnd += activate_enemies;
            w.transitionUpLeftBegin += deactivate_enemies;
        }
        if (transition_east) {
            TransitionCamera e = transition_east.GetComponent<TransitionCamera>();
            e.transitionUpLeftEnd += activate_enemies;
            e.transitionDownRightBegin += deactivate_enemies;
        }
        if (transition_south) {
            TransitionCamera s = transition_south.GetComponent<TransitionCamera>();
            s.transitionUpLeftEnd += activate_enemies;
            s.transitionDownRightBegin += deactivate_enemies;
        }

        deactivate_enemies();
    }

    void activate_enemies() {
        foreach (Transform child in transform) {
            Controller c = child.gameObject.GetComponent<Controller>();
            if (c) {
                c.allowed_to_move = true;
            }
        }
    }

    void deactivate_enemies() {
        foreach (Transform child in transform) {
            Controller c = child.gameObject.GetComponent<Controller>();
            if (c) {
                c.allowed_to_move = false;
            }
        }
    }

}

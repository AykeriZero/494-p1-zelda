using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionCamera : MonoBehaviour
{
    public string transition_axis = "horizontal";
    public string transition_direction = "left";

    private string start_direction = "left";

    public delegate void VoidNoArgs();
    public VoidNoArgs transitionUpLeftBegin;
    public VoidNoArgs transitionUpLeftEnd;
    public VoidNoArgs transitionDownRightBegin;
    public VoidNoArgs transitionDownRightEnd;

    private float transition_speed = 2f;
    private float h_camera_transition_distance = 16f;
    private float v_camera_transition_distance = 16f;

    private Controller player;
    private GameObject camera_object;
    private GameObject player_object;

    // Start is called before the first frame update
    void Start() {
        start_direction = transition_direction;

        player_object = GameObject.Find("Player");
        player = player_object.GetComponent<Controller>();
        camera_object = GameObject.Find("Main Camera");

        TransitionData data = GameObject.Find("TransitionTriggers").GetComponent<TransitionData>();

        transition_speed = data.transition_speed;
        h_camera_transition_distance = data.h_camera_transition_distance;
        v_camera_transition_distance = data.v_camera_transition_distance;
    }

    void OnTriggerEnter(Collider other) {
        if (other.tag == "Player") {
            StartCoroutine(transition());
        }
    }

    IEnumerator transition() {
        player.stopInput();
        player.allowed_to_move = false;

        RunDelegatesBegin();

        Vector3 camera_current_position = camera_object.transform.position;
        Vector3 player_current_position = player_object.transform.position;

        switch(transition_axis) {
            case "horizontal":
                if (transition_direction == "left") {
                    while (player_object.transform.position.x > player_current_position.x - .8f) {
                        player_object.transform.position = player_object.transform.position - new Vector3(transition_speed * Time.deltaTime, 0, 0);
                        yield return null;
                    }
                    while (camera_object.transform.position.x > camera_current_position.x - h_camera_transition_distance) {
                        camera_object.transform.position = camera_object.transform.position - new Vector3(transition_speed * Time.deltaTime, 0, 0);
                        yield return null;
                    }
                    camera_object.transform.position = camera_current_position - new Vector3(h_camera_transition_distance, 0, 0);
                    while (player_object.transform.position.x > player_current_position.x - 3.5f) {
                        player_object.transform.position = player_object.transform.position - new Vector3(transition_speed * Time.deltaTime, 0, 0);
                        yield return null;
                    }
                    transition_direction = "right";
                } else {
                    while (player_object.transform.position.x < player_current_position.x + .8f) {
                        player_object.transform.position = player_object.transform.position + new Vector3(transition_speed * Time.deltaTime, 0, 0);
                        yield return null;
                    }
                    while (camera_object.transform.position.x < camera_current_position.x + h_camera_transition_distance) {
                        camera_object.transform.position = camera_object.transform.position + new Vector3(transition_speed * Time.deltaTime, 0, 0);
                        yield return null;
                    }
                    camera_object.transform.position = camera_current_position + new Vector3(h_camera_transition_distance, 0, 0);
                    while (player_object.transform.position.x < player_current_position.x + 3.5f) {
                        player_object.transform.position = player_object.transform.position + new Vector3(transition_speed * Time.deltaTime, 0, 0);
                        yield return null;
                    }
                    transition_direction = "left";
                }

                break;
            case "vertical":
                if (transition_direction == "down") {
                    while (player_object.transform.position.y > player_current_position.y - .8f) {
                        player_object.transform.position = player_object.transform.position - new Vector3(0, transition_speed * Time.deltaTime, 0);
                        yield return null;
                    }
                    while (camera_object.transform.position.y > camera_current_position.y - v_camera_transition_distance) {
                        camera_object.transform.position = camera_object.transform.position - new Vector3(0, transition_speed * Time.deltaTime, 0);
                        yield return null;
                    }
                    camera_object.transform.position = camera_current_position - new Vector3(0, v_camera_transition_distance, 0);
                    while (player_object.transform.position.y > player_current_position.y - 3) {
                        player_object.transform.position = player_object.transform.position - new Vector3(0, transition_speed * Time.deltaTime, 0);
                        yield return null;
                    }
                    transition_direction = "up";

                } else {
                    while (player_object.transform.position.y < player_current_position.y + .8f) {
                        player_object.transform.position = player_object.transform.position + new Vector3(0, transition_speed * Time.deltaTime, 0);
                        yield return null;
                    }
                    while (camera_object.transform.position.y < camera_current_position.y + v_camera_transition_distance) {
                        camera_object.transform.position = camera_object.transform.position + new Vector3(0, transition_speed * Time.deltaTime, 0);
                        yield return null;
                    }
                    camera_object.transform.position = camera_current_position + new Vector3(0, v_camera_transition_distance, 0);
                    while (player_object.transform.position.y < player_current_position.y + 3) {
                        player_object.transform.position = player_object.transform.position + new Vector3(0, transition_speed * Time.deltaTime, 0);
                        yield return null;
                    }
                    transition_direction = "down";
                }
                break;
            default:
                Debug.Log("Transition Error.");
                break;
        }
        RunDelegatesEnd();

        player.startInput();
        player.allowed_to_move = true;
    }

    void RunDelegatesBegin() {
        if (transition_direction == "left" || transition_direction == "up" ) {
            if(transitionUpLeftBegin != null) {
                transitionUpLeftBegin();
            }
        } else {
            if (transitionDownRightBegin != null) {
                transitionDownRightBegin();
            }
        }
    }

    void RunDelegatesEnd() {
        // Transition direction is switched at the end, use the opposite
        if (transition_direction == "left" || transition_direction == "up" ) {
            if(transitionDownRightEnd != null) {
                transitionDownRightEnd();
            }
        } else {
            if (transitionUpLeftEnd != null) {
                transitionUpLeftEnd();
            }
        }
    }

    public void reset() {
        transition_direction = start_direction;
    }
}

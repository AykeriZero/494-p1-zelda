using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMessage2 : MonoBehaviour
{
    public AudioClip text_sound;
    public GameObject transition_south;
    public SpriteRenderer letter_1;
    public SpriteRenderer letter_2;
    public SpriteRenderer letter_3;
    public SpriteRenderer letter_4;
    public SpriteRenderer letter_5;
    public SpriteRenderer letter_6;
    public SpriteRenderer letter_7;
    public SpriteRenderer letter_8;
    public SpriteRenderer letter_9;
    public SpriteRenderer letter_10;
    public SpriteRenderer letter_11;
    public SpriteRenderer letter_12;
    public SpriteRenderer letter_13;
    public SpriteRenderer letter_14;
    public SpriteRenderer letter_15;
    public SpriteRenderer letter_16;
    public SpriteRenderer letter_17;
    public float letter_time = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        if (transition_south)
        {
            TransitionCamera e = transition_south.GetComponent<TransitionCamera>();
            e.transitionUpLeftEnd += StartMessage;
            e.transitionDownRightEnd += DisableMessage;
        }
        DisableMessage();
    }


    void StartMessage()
    {
        AudioSource.PlayClipAtPoint(text_sound, Camera.main.transform.position);
        StartCoroutine(DisplayText());
    }

    IEnumerator DisplayText()
    {
        letter_1.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_2.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_3.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_4.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_5.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_6.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_7.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_8.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_9.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_10.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_11.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_12.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_13.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_14.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_15.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_16.enabled = true;
        yield return new WaitForSeconds(letter_time);
        letter_17.enabled = true;
    }

    void DisableMessage() {
        letter_1.enabled = false;
        letter_2.enabled = false;
        letter_3.enabled = false;
        letter_4.enabled = false;
        letter_5.enabled = false;
        letter_6.enabled = false;
        letter_7.enabled = false;
        letter_8.enabled = false;
        letter_9.enabled = false;
        letter_10.enabled = false;
        letter_11.enabled = false;
        letter_12.enabled = false;
        letter_13.enabled = false;
        letter_14.enabled = false;
        letter_15.enabled = false;
        letter_16.enabled = false;
        letter_17.enabled = false;
    }
}

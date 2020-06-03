using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    // Status
    public bool allowed_to_move;
    public enum Direction { Left, Right, Up, Down}
    public Direction facing_direction;

    public delegate void VoidNoArgs();
    public VoidNoArgs onDeath;
    public VoidNoArgs takeDamage;

    public AudioClip enemy_damage_sound;
    public int totalHealth = 1;

    public bool InSpecialEnemyRoom = false;      //set true for enemies in rooms that drop an item when all are defeated
    public DropItemWhenEnemiesDefeated roomDropItem;
    public DoorLockUntilEnemiesDefeated roomDoorUnlock;

    // ------------ PRIVATE MEMBER VARIABLES -----------

    // Input
    private bool allow_input;
    // Health
    public int health;  //current health; initialize to owner's total health

    GameObject owner;

    private SpriteRenderer sr;


    void Start()
    {
        health = totalHealth;
        owner = gameObject;
        allow_input = true;
        sr = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if(health <= 0) {
            if (onDeath != null) {
                onDeath();
            }
            Destroy(owner);
            if (InSpecialEnemyRoom)
            {
                if(roomDropItem != null)
                {
                    roomDropItem.enemiesDefeated++;
                }
                if(roomDoorUnlock != null)
                {
                    roomDoorUnlock.enemiesDefeated++;
                }
            }
        }
    }

    // ---------------- HEALTH INTERFACE --------------

    public void decreaseHealth() {
        AudioSource.PlayClipAtPoint(enemy_damage_sound, Camera.main.transform.position);
        health--;
        if(health != 0) //if gets hit and doesn't die, flash colors
        {
            StartCoroutine(EnemyTakeDamage());
        }
        if (takeDamage != null) {
            takeDamage();
        }

        
    }

    public void addHealth() {
        if(health != totalHealth) {
            health++;
        }
    }

    public int getHealth() {
        return health;
    }

    public bool atFullHeath() {
        return health == totalHealth;
    }

    // --------------- INPUT INTERFACE ----------------

    public void stopInput() { allow_input = false; }
    public void startInput() { allow_input = true; }
    public bool allowInput() { return allow_input; }

    //------------ COROUTINE ---------------
    IEnumerator EnemyTakeDamage() //change enemy sprite color when taking damage
    {
        sr.color = new Color(1f, .7f, .7f, 1f); //red
        yield return new WaitForSeconds(.1f);
        sr.color = new Color(1f, 1f, 1f, 1f);   //normal
        yield return new WaitForSeconds(.1f);
        sr.color = new Color(1f, .7f, .7f, 1f); //red
        yield return new WaitForSeconds(.1f);
        sr.color = new Color(1f, 1f, 1f, 1f);  //normal
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invincibility : MonoBehaviour
{
    //when the player presses 1, toggles "God Mode":
    //invincible and maximizes rupee, key, and bomb count
    public static bool God_Mode = false;
    public Inventory inventory;
    int temp_rupee_count = 0;
    int temp_key_count = 0;

    void Start() {
        inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if(God_Mode == false)
            {
                God_Mode = true;
                temp_rupee_count = inventory.GetRupees();
                temp_key_count = inventory.GetKeys();
            }
            else
            {
                God_Mode = false;
                inventory.SetKeys(temp_key_count);
                inventory.SetRupees(temp_rupee_count);
            }
        }
    }

    private void LateUpdate()
    {
        if (God_Mode)
        {
            inventory.SetRupees(255);
            inventory.SetKeys(255);
        }
    }

    public bool isGodMode()
    {
        return God_Mode;
    }
}

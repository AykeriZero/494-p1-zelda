using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    int rupee_count = 0;
    int key_count = 0;
    int bomb_count = 0;
    public bool hasBow = false;
    public bool hasBoomer = false;
    public bool hasIce = false;

    public void AddRupees(int num_rupees) {
        rupee_count += num_rupees;
    }

    public void AddKey(int num_keys) {
        key_count += num_keys;
    }

    public int GetRupees() {
        return rupee_count;
    }

    public int GetKeys() {
        return key_count;
    }

    public void SetRupees(int num) {
        rupee_count = num;
    }

    public void SetKeys(int num) {
        key_count = num;
    }

    public void AddBombs(int num_bombs)
    {
        bomb_count += num_bombs;
    }

    public int GetBombs()
    {
        return bomb_count;
    }

    public void SetBombs(int num_bombs)
    {
        bomb_count = num_bombs;
    }

    public void GetBow()
    {
        hasBow = true;
    }

    public void GetBoomer()
    {
        hasBoomer = true;
    }

    public void GetIce()
    {
        hasIce = true;
    }
}

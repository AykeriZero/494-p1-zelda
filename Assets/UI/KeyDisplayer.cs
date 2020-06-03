﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeyDisplayer : MonoBehaviour
{
    public Inventory inventory;
    Text text_component;

    // Use this for initialization
    void Start()
    {
        text_component = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory != null && text_component != null)
        {
            text_component.text = "x"+inventory.GetKeys().ToString();
        }
    }
}

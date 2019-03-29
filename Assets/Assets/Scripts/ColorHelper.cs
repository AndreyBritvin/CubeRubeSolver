﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHelper : MonoBehaviour {

    public Slider far;

    public Slider RedSlider;
    public Slider GreenSlider;
    public Slider BlueSlider;

    private float rValue;
    private float gValue;
    private float bValue;
	
	// Update is called once per frame
	void Update () {

        //Camera.main.transform.position = new Vector3(0,0, -(far.value));
       // int a = -Mathf.RoundToInt(far.value);
    //    Camera.main.transform.position = new Vector3(0f,0f,(-a));
        rValue = RedSlider.value;
        gValue = GreenSlider.value;
        bValue = BlueSlider.value;
        int div = 255;
        this.GetComponent<Camera>().backgroundColor = new Color(rValue/div, gValue/div, bValue/div);


    }
}

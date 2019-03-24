using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorHelper : MonoBehaviour {

    public Slider RedSlider;
    public Slider GreenSlider;
    public Slider BlueSlider;

    private float rValue;
    private float gValue;
    private float bValue;
	
	// Update is called once per frame
	void Update () {
        rValue = RedSlider.value;
        gValue = GreenSlider.value;
        bValue = BlueSlider.value;
        int div = 255;
        this.GetComponent<Camera>().backgroundColor = new Color(rValue/div, gValue/div, bValue/div);


    }
}

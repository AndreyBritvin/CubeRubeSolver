using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Buttons : MonoBehaviour {
    bool IsModePressed = false;
    bool IsSettingsPressed = false;
    public GameObject modePanel;
    public GameObject SettingsPanel;
    public static int qualShuffle = 5;
    public Slider sliderShuffle;
    public Slider sliderSens;
    public Slider sliderSpeed;

    public static int xSens = 15;
    public static int ySens = -15;

    public CubeManager cubeM;
    public void Mode()
    {
        if(!IsModePressed)
        {
            IsModePressed = true;
            modePanel.SetActive(true);
        }
        else
        {
            IsModePressed = false;
            modePanel.SetActive(false);
        }
    }

    public void Settings()
    {
        if (!IsSettingsPressed)
        {
            IsSettingsPressed = true;
            SettingsPanel.SetActive(true);
        }
        else
        {
            IsSettingsPressed = false;
            SettingsPanel.SetActive(false);
        }
    }

    public void  shuffleSlider()
    {
        qualShuffle = Mathf.RoundToInt(sliderShuffle.value);
        
    }

    public void sensSlider()
    {
        xSens = Mathf.RoundToInt(sliderSens.value);
        ySens = -Mathf.RoundToInt(sliderSens.value);

    }

    public void speedSlider()
    {


        int slVal = Mathf.RoundToInt(sliderSpeed.value);
        if(slVal == 4)
        {
            slVal = 5;
        }
        else if(slVal == 7)
        {
            slVal = 6;
        }
        else if (slVal == 8)
        {
            slVal = 9;
        }
        
        cubeM.speed = slVal;
    }


    public void shuffleButton()
    {
        StartCoroutine(cubeM.shuffle4());
    }

    public void buildButton()
    {
        StartCoroutine(cubeM.buildCube());
    }
}

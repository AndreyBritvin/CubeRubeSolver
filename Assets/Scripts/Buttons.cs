﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Buttons : MonoBehaviour {
    bool IsModePressed = false;
    bool IsSettingsPressed = false;
    bool IsDesignPressed = false;
    bool IsSolvePressed = false;
    public GameObject modePanel;
    public GameObject SettingsPanel;
    public static int qualShuffle = 5;
    public Slider sliderShuffle;
    public Slider sliderSens;
    public Slider sliderSpeed;
    public GameObject designPanel;
    public GameObject solvePanel;
    public GameObject colorPanel;


    bool IsColorPressed = false;

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

    public void solveButton()
    {

        if (!IsSolvePressed)
        {
            IsSolvePressed = true;
            solvePanel.SetActive(true);
        }
        else
        {
            IsSolvePressed = false;
            solvePanel.SetActive(false);
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
            IsColorPressed = false;
            colorPanel.SetActive(false);
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

    private void Resetmode()
    {
        IsDesignPressed = false;
        IsModePressed = false;
        IsSolvePressed = false;
        solvePanel.SetActive(false);
        modePanel.SetActive(false);
        
    }
    public void shuffleButton()
    {
        Resetmode();
        StartCoroutine(cubeM.shuffle4());
    }

    public void buildButton()
    {
        Resetmode();
        StartCoroutine(cubeM.buildCube());
    }

    public void resetCube()
    {
        cubeM.CreateCube();
    }

    public void design1()
    {
        Resetmode();
        StartCoroutine(cubeM.design_chess());
    }

    public void designActive()
    {
        if (!IsDesignPressed)
        {
            IsDesignPressed = true;
            designPanel.SetActive(true);
        }
        else
        {
            IsDesignPressed = false;
            designPanel.SetActive(false);
        }
    }

    public void design2()
    {
        Resetmode();
        StartCoroutine(cubeM.design_center());
    }

    public void design3()
    {
        Resetmode();
        StartCoroutine(cubeM.design_cubeincube());
    }

    public void design4()
    {
        Resetmode();
        StartCoroutine(cubeM.design_Unknown());
    }


    public void ColorActive()
    {
        if (!IsColorPressed)
        {
            IsColorPressed = true;
            colorPanel.SetActive(true);
        }
        else
        {
            IsColorPressed = false;
            colorPanel.SetActive(false);
        }
    }




    public void whiteK()//krest
    {
        Resetmode();
        cubeM.IsSolve = true;
        //print("I wonna BuildWhiteKrest!");
        cubeM.resetList();
        StartCoroutine(cubeM.BuildWhiteKrest(cubeM.sides));
       // cubeM.IsSolve = false;

    }

    public void whiteC()//color
    {
        Resetmode();
        cubeM.IsSolve = true;
        cubeM.resetList();
        StartCoroutine(cubeM.BuildWhiteCorner(cubeM.sides));
      //  cubeM.IsSolve = false;
    }

    public void ColourR()//colour rebra
    {
        Resetmode();
        cubeM.IsSolve = true;
        cubeM.resetList();
        StartCoroutine(cubeM.BuildColourRebra(cubeM.sides));
       // cubeM.IsSolve = false;
    }

    public void yellowK()//krest
    {
        Resetmode();
        cubeM.IsSolve = true;
        cubeM.resetList();
        StartCoroutine(cubeM.ReturnAndPlaceOnTruePlaceYellowRebra(cubeM.sides));
        //cubeM.IsSolve = false;
    }

    public void yellowC()//corners
    {
        Resetmode();
        cubeM.IsSolve = true;
        cubeM.resetList();
        StartCoroutine(cubeM.ReturnAndPlaceOnTruePlaceYellowCorners(cubeM.sides));
       // cubeM.IsSolve = false;
    }

    
}

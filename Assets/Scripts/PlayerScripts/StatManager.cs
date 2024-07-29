using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatManager : MonoBehaviour
{
    float fullHP = 12;
    float fullFP = 10;
    public float fullStamina = 11;

    public float HP;
    public float FP;
    public float stam;

    public Slider hpSlider;
    public Slider fpSlider;
    public Slider stamSlider;

    void Start()
    {
        hpSlider.maxValue = fullHP;
        fpSlider.maxValue = fullFP;
        stamSlider.maxValue = fullStamina;

        HP = fullHP;
        FP = fullFP;
        stam = fullStamina;

    }

    void Update()
    {
        hpSlider.value = HP;
        fpSlider.value = FP;
        stamSlider.value = stam;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsScript : MonoBehaviour
{
    private Slider Controls;
    float sens=0.5f;
    public String playerPrefsVar;
    void Start() {
        Controls = this.gameObject.GetComponent<Slider>();

    }
    void Update(){
        sens=Controls.value;
        PlayerPrefs.SetFloat(playerPrefsVar, sens);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeControls : MonoBehaviour
{
    private void Update()
    {
        FMODUnity.RuntimeManager.GetBus("bus:/").setVolume(PlayerPrefs.GetFloat("Volume", 0.5f));
    }
}

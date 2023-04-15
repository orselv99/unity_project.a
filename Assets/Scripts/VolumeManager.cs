using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance = null;

    private float masterVolume = 0f;
    private float backgroundVolume = 0f;
    private float effectVolume = 0f;

    private void Awake()
    {
        // ΩÃ±€≈Ê
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void SetMasterVolume(float value)
    {
        this.masterVolume = value;
    }
    public void SetBackgroundVolume(float value)
    {
        this.backgroundVolume = value;
    }
    public void SetEffectVolume(float value)
    {
        this.effectVolume = value;
    }
}

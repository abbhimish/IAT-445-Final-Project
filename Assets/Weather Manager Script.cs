using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public ParticleSystem rainParticleSystem;
    public float maxRainRate = 5000f; // Maximum particle emission rate
    public float maxFogDensity = 0.2f; // Maximum fog density value

    [Header("Stage Weather Intensity")]
    public float weatherIntensity = 0.0f; // Assign per stage (e.g., 0.0f for stage1-2, 0.10f for stage2-3)

    [Header("Optional Key Trigger")]
    public KeyCode triggerKey = KeyCode.None; // e.g., KeyCode.F1 for stage1-2, F2 for stage2-3

    private ParticleSystem.EmissionModule rainEmission;

    void Start()
    {
        if (rainParticleSystem != null)
        {
            rainEmission = rainParticleSystem.emission;
        }

        // Optional: Automatically trigger on Start
        SetWeather(weatherIntensity);
    }

    void Update()
    {
        // Press F1, F2... to manually trigger this stage's weather
        if (triggerKey != KeyCode.None && Input.GetKeyDown(triggerKey))
        {
            SetWeather(weatherIntensity);
        }
    }

    public void SetWeather(float intensity)
    {
        if (rainParticleSystem != null)
        {
            rainEmission.rateOverTime = maxRainRate * intensity;
        }

        RenderSettings.fogDensity = maxFogDensity * intensity;
        Debug.Log($"[{gameObject.name}] Weather set to intensity: {intensity}, Fog: {RenderSettings.fogDensity}");
    }
}

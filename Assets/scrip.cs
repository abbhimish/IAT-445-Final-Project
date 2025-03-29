using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public ParticleSystem rainParticleSystem;
    public float maxRainRate = 5000f; // Maximum particle emission rate
    public float maxFogDensity = 1f; // Maximum fog density value

    private ParticleSystem.EmissionModule rainEmission;

    void Start()
    {
        if (rainParticleSystem != null)
        {
            rainEmission = rainParticleSystem.emission;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SetWeather(0.10f); // 50% density
        }
        else if (Input.GetKeyDown(KeyCode.U))
        {
            SetWeather(0.35f); // 75% density
        }
        else if (Input.GetKeyDown(KeyCode.I))
        {
            SetWeather(0.60f); // 100% density
        }
    }

    void SetWeather(float intensity)
    {
        if (rainParticleSystem != null)
        {
            rainEmission.rateOverTime = maxRainRate * intensity;
        }
        RenderSettings.fogDensity = maxFogDensity * intensity;
    }
}

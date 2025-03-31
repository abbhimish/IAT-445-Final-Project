using UnityEngine;
using Sydewa;

public class StageController : MonoBehaviour
{
    public LightingManager lightingManager;
    public float startTime = 3.5f;
    public float targetTime = 8f;
    public float transitionDuration = 5f;

    private bool isTransitioning = false;
    private float transitionTimer = 0f;

    void Start()
    {
        if (lightingManager != null)
        {
            lightingManager.IsDayCycleOn = false;
            lightingManager.TimeOfDay = startTime;
        }
    }

    void Update()
    {
        //Manually start the experience
        if (Input.GetKeyDown(KeyCode.Q) && !isTransitioning)
        {
            transitionTimer = 0f;
            isTransitioning = true;
        }

        if (isTransitioning)
        {
            transitionTimer += Time.deltaTime;
            float t = Mathf.Clamp01(transitionTimer / transitionDuration);
            float newTime = Mathf.Lerp(startTime, targetTime, t);
            lightingManager.TimeOfDay = newTime;

            if (t >= 1f)
            {
                isTransitioning = false;
            }
        }
    }
}

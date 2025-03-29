using UnityEngine;
using Sydewa; // 加上你 LightingManager 的命名空间

public class StageController : MonoBehaviour
{
    public LightingManager lightingManager;
    public float startTime = 3.5f;
    public float targetTime = 8f;
    public float transitionDuration = 5f; // 时间变化持续 5 秒

    private bool isTransitioning = false;
    private float transitionTimer = 0f;

    void Start()
    {
        if (lightingManager != null)
        {
            lightingManager.IsDayCycleOn = false; // 不让它自动播放
            lightingManager.TimeOfDay = startTime;
        }
    }

    void Update()
    {
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

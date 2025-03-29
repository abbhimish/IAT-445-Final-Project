using UnityEngine;
using Sydewa; // ������ LightingManager �������ռ�

public class StageController : MonoBehaviour
{
    public LightingManager lightingManager;
    public float startTime = 3.5f;
    public float targetTime = 8f;
    public float transitionDuration = 5f; // ʱ��仯���� 5 ��

    private bool isTransitioning = false;
    private float transitionTimer = 0f;

    void Start()
    {
        if (lightingManager != null)
        {
            lightingManager.IsDayCycleOn = false; // �������Զ�����
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

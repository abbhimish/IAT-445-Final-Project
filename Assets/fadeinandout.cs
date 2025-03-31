using UnityEngine;

public class StageFadeEffect : MonoBehaviour
{
    public float duration = 1f;
    public bool destroyOnComplete = false;

    private float timer = 0f;
    private Renderer[] renderers;

    private bool isFadingIn = false;
    private bool isFadingOut = false;

    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>(true);

        // 将所有材质模式自动转为透明
        foreach (var rend in renderers)
        {
            foreach (var mat in rend.materials)
            {
                if (mat.HasProperty("_Mode")) // only standard shader
                {
                    mat.SetFloat("_Mode", 2);
                    mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                    mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                    mat.SetInt("_ZWrite", 0);
                    mat.DisableKeyword("_ALPHATEST_ON");
                    mat.EnableKeyword("_ALPHABLEND_ON");
                    mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                    mat.renderQueue = 3000;
                }
            }
        }
    }

    public void FadeIn()
    {
        isFadingIn = true;
        isFadingOut = false;
        timer = 0f;
        SetAlpha(0f);
        gameObject.SetActive(true);
    }

    public void FadeOut()
    {
        isFadingOut = true;
        isFadingIn = false;
        timer = 0f;
        SetAlpha(1f);
    }

    void Update()
    {
        if (isFadingIn)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            SetAlpha(t);

            if (t >= 1f) isFadingIn = false;
        }

        if (isFadingOut)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            SetAlpha(1f - t);

            if (t >= 1f)
            {
                isFadingOut = false;
                if (destroyOnComplete) Destroy(gameObject);
                else gameObject.SetActive(false);
            }
        }
    }

    void SetAlpha(float alpha)
    {
        foreach (var rend in renderers)
        {
            foreach (var mat in rend.materials)
            {
                if (mat.HasProperty("_Color"))
                {
                    Color color = mat.color;
                    color.a = alpha;
                    mat.color = color;

                    
                    if (mat.HasProperty("_Mode"))
                    {
                        mat.SetFloat("_Mode", 2); // Transparent
                        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                        mat.SetInt("_ZWrite", 0);
                        mat.DisableKeyword("_ALPHATEST_ON");
                        mat.EnableKeyword("_ALPHABLEND_ON");
                        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
                        mat.renderQueue = 3000;
                    }
                }
            }
        }
    }
}

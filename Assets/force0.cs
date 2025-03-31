using UnityEngine;

public class CodeInitializer : MonoBehaviour
{
    void Start()
    {
        TextMesh textMesh = GetComponent<TextMesh>();
        if (textMesh != null)
        {
            textMesh.text = "0000";
        }
    }
}

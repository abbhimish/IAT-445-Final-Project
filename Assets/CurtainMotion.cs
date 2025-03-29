using UnityEngine;

public class WindEffect : MonoBehaviour
{
    public Vector3 windDirection = new Vector3(1, 0, 0); // Wind direction
    public float windStrength = 5f; // Adjust strength per curtain
    private Cloth cloth;

    void Start()
    {
        cloth = GetComponent<Cloth>();
    }

    void Update()
    {
        if (cloth != null)
        {
            cloth.externalAcceleration = windDirection * windStrength;
        }
    }
}

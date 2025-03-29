using UnityEngine;

public class WindController : MonoBehaviour
{
    private WindZone windZone;
    private Cloth[] clothObjects; // Array to store all cloth objects

    void Start()
    {
        windZone = GetComponent<WindZone>();
        clothObjects = FindObjectsOfType<Cloth>(); // Find all Cloth objects in the scene
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) { SetWind(1f, 0.5f); } // Mild Breeze
        if (Input.GetKeyDown(KeyCode.W)) { SetWind(3f, 1f); }   // Light Wind
        if (Input.GetKeyDown(KeyCode.E)) { SetWind(6f, 2f); }   // Moderate Wind
        if (Input.GetKeyDown(KeyCode.R)) { SetWind(10f, 4f); }  // Strong Wind
        if (Input.GetKeyDown(KeyCode.T)) { SetWind(20f, 8f); }  // Storm
    }

    void SetWind(float main, float turbulence)
    {
        windZone.windMain = main;
        windZone.windTurbulence = turbulence;

        // Apply external force to all cloth objects
        Vector3 windForce = new Vector3(main, 0, 0); // Wind moves along X-axis
        foreach (Cloth cloth in clothObjects)
        {
            cloth.externalAcceleration = windForce;
        }
    }
}

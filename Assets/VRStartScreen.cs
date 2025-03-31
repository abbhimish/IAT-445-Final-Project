using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;
using UnityEngine.SceneManagement;

public class VRStartScreen : MonoBehaviour
{
    public Canvas uiCanvas;   // Reference to the UI Canvas.
    public Text promptText;    // Reference to the prompt text on the canvas.
    public float fadeDuration = 2f; // Duration for fade effect.
    private bool isFading = false;
    private float fadeTimer = 0f;

    // For fade in and fade out effect.
    private Image fadeImage;

    void Start()
    {
        fadeImage = uiCanvas.GetComponentInChildren<Image>(); // Assuming the Canvas has an Image component for the fade effect.
        fadeImage.color = new Color(0, 0, 0, 1); // Set initial color to black.
        promptText.text = "Press any button or 'Q' to begin experience"; // Set the prompt text.
    }

    void Update()
    {
        // Check for VR button input (using XR input or any button press).
        if (!isFading)
        {
            if (IsVRButtonPressed() || Input.GetKeyDown(KeyCode.Q)) // Check if any VR button or Q key is pressed.
            {
                StartFadeOut(); // Start the fade-out effect when button is pressed.
            }
        }

        // Handle fade effect over time.
        if (isFading)
        {
            fadeTimer += Time.deltaTime;
            float t = Mathf.Clamp01(fadeTimer / fadeDuration);

            // Fade out to white.
            fadeImage.color = new Color(1, 1, 1, t); // Lerp from black to white.

            if (t >= 1f)
            {
                // Once fade is complete, trigger scene transition or load the environment.
                LoadEnvironment();
            }
        }
    }

    bool IsVRButtonPressed()
    {
        // Use XR Input system to detect any VR button press.
        // Example check for any primary button (trigger, grip, etc.) on the XR controller.
        InputDevice device = InputDevices.GetDeviceAtXRNode(XRNode.RightHand); // Get the right-hand controller (can also use LeftHand)
        if (device.isValid)
        {
            bool primaryButtonPressed;
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out primaryButtonPressed) && primaryButtonPressed)
            {
                return true; // Button is pressed.
            }
        }

        return false; // No VR button pressed.
    }

    void StartFadeOut()
    {
        isFading = true;
    }

    void LoadEnvironment()
    {
        // Here you can either load a new scene or transition into the VR experience.
        SceneManager.LoadScene("SampleScene"); // Replace "YourSceneName" with the actual scene you want to load.
    }
}

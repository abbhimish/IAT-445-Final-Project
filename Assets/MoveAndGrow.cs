using System.Collections;
using UnityEngine;

public class MoveAndGrow : MonoBehaviour
{
    public Transform player;  // Assign the player object in the Inspector
    public float moveSpeed = 2f;  // Speed at which it moves towards the player
    public float growthRate = 0.5f; // Rate at which it grows
    public float maxSize = 3f; // Maximum scale it can grow to

    private Vector3 originalScale;
    private bool isActive = false;

    void Start()
    {
        originalScale = transform.localScale;
        isActive = false;
    }

    void OnEnable()  // Triggers when the object becomes active in the scene
    {
        StartCoroutine(GrowAndMove());
    }

    IEnumerator GrowAndMove()
    {
        isActive = true;

        while (isActive)
        {
            if (player != null)
            {
                // Move towards the player
                transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }

            // Gradually increase size
            if (transform.localScale.x < maxSize)
            {
                transform.localScale += Vector3.one * (growthRate * Time.deltaTime);
            }

            yield return null; // Wait for the next frame
        }
    }
}

using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public Transform player; // Assign the player in the Inspector
    public float moveSpeed = 5f;
    public float scaleSpeed = 1f;
    public float maxScale = 2f;

    private Vector3 originalScale;
    private bool isMoving = false;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5))
        {
            isMoving = true;
        }

        if (isMoving)
        {
            // Move towards player
            transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);

            // Increase scale
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale * maxScale, scaleSpeed * Time.deltaTime);

            // Optional: Stop movement when close enough
            if (Vector3.Distance(transform.position, player.position) < 0.5f)
            {
                isMoving = false;
            }
        }
    }
}

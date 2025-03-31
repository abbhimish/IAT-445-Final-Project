using UnityEngine;

public class StageMoveEffect : MonoBehaviour
{
    public float duration = 1f;
    public float moveDistance = 5f;
    public bool destroyOnComplete = false;

    private Vector3 startPos;
    private Vector3 targetPos;
    private float timer = 0f;
    private bool isMovingUp = false;
    private bool isMovingDown = false;

    public void MoveUp()
    {
        timer = 0f;
        isMovingUp = true;
        isMovingDown = false;
        startPos = transform.position - Vector3.up * moveDistance;
        targetPos = transform.position;
        transform.position = startPos;
        gameObject.SetActive(true);
    }

    public void MoveDown()
    {
        timer = 0f;
        isMovingDown = true;
        isMovingUp = false;
        startPos = transform.position;
        targetPos = transform.position - Vector3.up * moveDistance;
    }

    void Update()
    {
        if (isMovingUp)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            if (t >= 1f) isMovingUp = false;
        }

        if (isMovingDown)
        {
            timer += Time.deltaTime;
            float t = Mathf.Clamp01(timer / duration);
            transform.position = Vector3.Lerp(startPos, targetPos, t);
            if (t >= 1f)
            {
                isMovingDown = false;
                if (destroyOnComplete) Destroy(gameObject);
                else gameObject.SetActive(false);
            }
        }
    }
}

using UnityEngine;

public class EnemyShipController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private float rotationSpeed = 45.0f;

    [SerializeField]
    private float reverseDuration = 1.0f;

    [SerializeField]
    private float changeDirectionCooldown = 3.0f;

    private bool isReversing = false;
    private float reverseTimer = 0.0f;

    private bool isChangingDirection = false;
    private float changeDirectionTimer = 0.0f;
    private Quaternion startRotation;
    private Quaternion targetRotation;

    private void Update()
    {
        if (isReversing)
        {
            reverseTimer -= Time.deltaTime;

            if (reverseTimer <= 0.0f)
            {
                isReversing = false;
            }
            else
            {
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

            if (!isChangingDirection && Random.Range(0, 100) < 1)
            {
                isChangingDirection = true;
                changeDirectionTimer = 0.0f;
                startRotation = transform.rotation;
                targetRotation = Quaternion.Euler(Random.Range(-90, 90), Random.Range(0, 360), Random.Range(-90, 90));
            }

            if (isChangingDirection)
            {
                changeDirectionTimer += Time.deltaTime;
                float t = changeDirectionTimer / changeDirectionCooldown;
                transform.rotation = Quaternion.Lerp(startRotation, targetRotation, t);

                if (changeDirectionTimer >= changeDirectionCooldown)
                {
                    isChangingDirection = false;
                }
            }

            if (IsCollidingWithAreaLimit())
            {
                isReversing = true;
                reverseTimer = reverseDuration;
            }
        }
    }

    private bool IsCollidingWithAreaLimit()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1.0f);

        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("AreaLimit"))
            {
                return true;
            }
        }

        return false;
    }
}

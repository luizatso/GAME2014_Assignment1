using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float rotationSpeed;

    [SerializeField] private float moveAwayDuration = 0.5f;

    private Rigidbody2D rb;

    private PlayerAwarenessController playerAwarenessController;

    private Vector2 targetDirection;
    private float changeDirectionCooldown;

    private bool turningAway;
    private float moveAwayTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAwarenessController =
            GetComponent<PlayerAwarenessController>();

        targetDirection = transform.up;
    }

    private void FixedUpdate()
    {
        if (turningAway)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;

            RotateTowardsTarget();

            float angleDifference = Mathf.Abs(
                Mathf.DeltaAngle(rb.rotation, GetTargetAngle())
            );

            if (angleDifference < 1f)
            {
                turningAway = false;
                moveAwayTimer = moveAwayDuration;
            }

            return;
        }

        if (moveAwayTimer > 0f)
        {
            moveAwayTimer -= Time.fixedDeltaTime;
        }
        else
        {
            UpdateTargetDirection();
        }

        RotateTowardsTarget();
        SetVelocity();
    }

    private void UpdateTargetDirection()
    {
        HandleRandomDirectionChange();
        HandlePlayerTargeting();
    }

    private void HandleRandomDirectionChange()
    {
        changeDirectionCooldown -= Time.fixedDeltaTime;

        if (changeDirectionCooldown <= 0f)
        {
            float angleChange = Random.Range(-90f, 90f);

            Quaternion rotation = Quaternion.AngleAxis(
                angleChange,
                Vector3.forward
            );

            targetDirection = rotation * targetDirection;

            changeDirectionCooldown = Random.Range(1f, 5f);
        }
    }

    private void HandlePlayerTargeting()
    {
        if (playerAwarenessController != null &&
            playerAwarenessController.AwareOfPlayer &&
            playerAwarenessController.DirectionToPlayer.sqrMagnitude > 0f)
        {
            targetDirection =
                playerAwarenessController.DirectionToPlayer;
        }
    }

    private float GetTargetAngle()
    {
        return Mathf.Atan2(targetDirection.y, targetDirection.x)
            * Mathf.Rad2Deg - 90f;
    }

    private void RotateTowardsTarget()
    {
        float rotation = Mathf.MoveTowardsAngle(
            rb.rotation,
            GetTargetAngle(),
            rotationSpeed * Time.fixedDeltaTime
        );

        rb.SetRotation(rotation);
    }

    private void SetVelocity()
    {
        float angle = rb.rotation * Mathf.Deg2Rad;

        Vector2 forward = new Vector2(
            -Mathf.Sin(angle),
            Mathf.Cos(angle)
        );

        rb.linearVelocity = forward * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        if (collision.contactCount == 0)
        {
            return;
        }

        // Point away from the surface the enemy touched.
        targetDirection = collision.GetContact(0).normal.normalized;

        turningAway = true;
        moveAwayTimer = 0f;

        rb.linearVelocity = Vector2.zero;
        rb.angularVelocity = 0f;

        changeDirectionCooldown = Random.Range(1f, 5f);
    }
}
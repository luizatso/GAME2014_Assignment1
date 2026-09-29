using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Tilemap ground;
    [SerializeField] private float smoothSpeed = 5f;

    private Camera cameraComponent;
    private Vector3 minBounds;
    private Vector3 maxBounds;

    private void Start()
    {
        cameraComponent = GetComponent<Camera>();

        if (ground == null)
        {
            return;
        }

        ground.CompressBounds();

        Bounds bounds = ground.localBounds;
        minBounds = ground.transform.TransformPoint(bounds.min);
        maxBounds = ground.transform.TransformPoint(bounds.max);
    }

    private void LateUpdate()
    {
        if (player == null || ground == null)
        {
            return;
        }

        float halfHeight = cameraComponent.orthographicSize;
        float halfWidth = halfHeight * cameraComponent.aspect;

        Vector3 targetPosition = new Vector3(
            player.position.x,
            player.position.y,
            transform.position.z
        );

        Vector3 nextPosition = Vector3.Lerp(
            transform.position,
            targetPosition,
            1f - Mathf.Exp(-smoothSpeed * Time.deltaTime)
        );

        nextPosition.x = ClampAxis(
            nextPosition.x, minBounds.x, maxBounds.x, halfWidth
        );

        nextPosition.y = ClampAxis(
            nextPosition.y, minBounds.y, maxBounds.y, halfHeight
        );

        transform.position = nextPosition;
    }

    private float ClampAxis(
        float position, float min, float max, float halfViewSize)
    {
        if (max - min <= halfViewSize * 2f)
        {
            return (min + max) / 2f;
        }

        return Mathf.Clamp(
            position,
            min + halfViewSize,
            max - halfViewSize
        );
    }
}
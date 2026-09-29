using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float timeBetweenShots = 0.2f;
    [SerializeField] private Transform gunOffset;

    private float lastShotTime = float.NegativeInfinity;
    private InputAction attackAction;

    private void Awake()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions.FindAction("Player/Attack", true);
    }

    // Update is called once per frame
    void Update()
    {
        bool wantsToShoot = attackAction.IsPressed()
            || attackAction.WasPressedThisFrame();

        if (wantsToShoot && Time.time - lastShotTime >= timeBetweenShots)
        {
            FireBullet();
            lastShotTime = Time.time;
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            gunOffset.position,
            transform.rotation
        );

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = bulletSpeed * transform.up;
    }
}
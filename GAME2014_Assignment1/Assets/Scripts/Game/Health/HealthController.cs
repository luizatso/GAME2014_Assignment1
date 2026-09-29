using UnityEngine;
using UnityEngine.Events;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float maximumHealth = 100f;

    public float RemainingHealthPercentage
    {
        get
        {
            return maximumHealth > 0f
                ? Mathf.Clamp01(currentHealth / maximumHealth)
                : 0f;
        }
    }

    public bool IsInvincible { get; set; }

    public UnityEvent OnDied = new UnityEvent();
    public UnityEvent OnDamaged = new UnityEvent();
    public UnityEvent OnHealthChanged = new UnityEvent();

    public void TakeDamage(float damageAmount)
    {
        if (currentHealth <= 0f || IsInvincible || damageAmount <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Clamp(
            currentHealth - damageAmount,
            0f,
            maximumHealth
        );

        OnHealthChanged.Invoke();

        if (currentHealth <= 0f)
        {
            OnDied.Invoke();
        }
        else
        {
            OnDamaged.Invoke();
        }
    }

    public void AddHealth(float amountToAdd)
    {
        if (currentHealth <= 0f ||
            currentHealth >= maximumHealth ||
            amountToAdd <= 0f)
        {
            return;
        }

        currentHealth = Mathf.Clamp(
            currentHealth + amountToAdd,
            0f,
            maximumHealth
        );

        OnHealthChanged.Invoke();
    }
}
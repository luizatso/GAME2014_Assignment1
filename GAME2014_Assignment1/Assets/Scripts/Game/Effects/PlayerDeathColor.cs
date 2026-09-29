using UnityEngine;

public class PlayerDeathColor : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    private bool isDead;

    public void TurnRed()
    {
        isDead = true;
        ApplyDeathColor();
    }

    private void LateUpdate()
    {
        if (isDead)
        {
            ApplyDeathColor();
        }
    }

    private void ApplyDeathColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
        }
    }
}
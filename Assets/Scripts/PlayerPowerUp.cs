using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    private SkateboardMovement movementScript; // Reference to the movement script
    private bool isInvincible = false;

    void Start()
    {
        movementScript = GetComponent<SkateboardMovement>();
        if (movementScript == null)
        {
            Debug.LogError("SkateboardMovement script is missing!");
        }
    }

    public void ActivatePowerUp(PowerUp.PowerUpType powerUpType, float duration)
    {
        switch (powerUpType)
        {
            case PowerUp.PowerUpType.SpeedBoost:
                StartCoroutine(SpeedBoost(duration));
                break;

            case PowerUp.PowerUpType.JumpBoost:
                StartCoroutine(JumpBoost(duration));
                break;

            case PowerUp.PowerUpType.Invincibility:
                StartCoroutine(Invincibility(duration));
                break;
        }
    }

    private IEnumerator SpeedBoost(float duration)
    {
        float originalSpeed = movementScript.acceleration;
        movementScript.acceleration *= 2; // Double the speed
        yield return new WaitForSeconds(duration);
        movementScript.acceleration = originalSpeed; // Reset speed
    }

    private IEnumerator JumpBoost(float duration)
    {
        float originalJumpForce = movementScript.jumpForce;
        movementScript.jumpForce *= 1.5f; // Increase jump height
        yield return new WaitForSeconds(duration);
        movementScript.jumpForce = originalJumpForce; // Reset jump force
    }

    private IEnumerator Invincibility(float duration)
    {
        isInvincible = true;
        // Add visual feedback, e.g., glowing material
        yield return new WaitForSeconds(duration);
        isInvincible = false;
        // Remove visual feedback
    }
}

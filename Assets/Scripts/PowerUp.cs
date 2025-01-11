using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpType { SpeedBoost, JumpBoost, Invincibility }
    public PowerUpType powerUpType; // Type of the power-up
    public float effectDuration = 5f; // Duration of the power-up effect

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPowerUp player = other.GetComponent<PlayerPowerUp>();
            if (player != null)
            {
                player.ActivatePowerUp(powerUpType, effectDuration);
            }

            // Destroy the power-up after collection
            Destroy(gameObject);
        }
    }
}

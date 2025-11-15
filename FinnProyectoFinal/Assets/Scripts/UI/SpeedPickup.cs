using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public float bonusSpeed = 1f;    // cuánto suma SOLO en esta partida

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.runSpeedBonus += bonusSpeed;
            // opcional: efecto, sonido, etc.
            Destroy(gameObject);
        }
    }
}


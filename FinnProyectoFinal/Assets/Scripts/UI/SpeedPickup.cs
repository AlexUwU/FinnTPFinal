using UnityEngine;

public class SpeedPickup : MonoBehaviour
{
    public float bonusSpeed = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar velocidad SOLO para esta partida
            GameManager.runSpeedBonus += bonusSpeed;

            //  Reproducir audio correcto desde el player
            PlayerBehaviour pb = other.GetComponent<PlayerBehaviour>();
            if (pb != null && pb.upgradeSound != null)
                pb.upgradeSound.Play();

            Destroy(gameObject);
        }
    }
}



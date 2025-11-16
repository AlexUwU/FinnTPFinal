using UnityEngine;

public class DamagePickup : MonoBehaviour
{
    public float bonusDamage = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar daño SOLO para esta partida
            GameManager.runDamageBonus += bonusDamage;

            //  Reproducir audio correcto desde el player
            PlayerBehaviour pb = other.GetComponent<PlayerBehaviour>();
            if (pb != null && pb.upgradeSound != null)
                pb.upgradeSound.Play();

            Destroy(gameObject);
        }
    }
}



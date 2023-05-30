using UnityEngine;

public class RumPickup : MonoBehaviour
{
    public PlayerController player;
    [SerializeField] private bool deleteOnPickup;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent))
        {
            Debug.Log("Rum Healing Player");
            if (player.HealDamage(-0.75f))
            {
                if (deleteOnPickup)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}

using UnityEngine;

public class WeaponPickup : Interactable
{
    [SerializeField] WeaponReference.rangedWeapon pickup;
    public PlayerController player;
    [SerializeField] private bool deleteOnPickup;

    public override void Interact()
    {
        Debug.Log("In Interact Func");
        player.PlayerEquipRangedWeapon(pickup);
        if (deleteOnPickup)
        {
            Destroy(gameObject);
        }
    }
}

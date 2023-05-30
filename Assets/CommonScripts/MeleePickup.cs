using UnityEngine;

public class MeleePickup : Interactable
{
    [SerializeField] WeaponReference.meleeWeapon pickup;
    public PlayerController player;
    [SerializeField] private bool deleteOnPickup;

    public override void Interact()
    {
        Debug.Log("In Interact Func");
        player.PlayerEquipMeleeWeapon(pickup);
        if (deleteOnPickup)
        {
            Destroy(gameObject);
        }
    }
}

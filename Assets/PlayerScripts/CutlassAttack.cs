using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutlassAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private CooldownUI cooldownUI;
    private float meleeSpeed = 0.3f;
    private float damage = 5f;
    private float lastAttack = 0;
    private bool Friendly = true;

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public void Swing(bool friendly)
    {
        Friendly = friendly;
        if (Time.time - lastAttack > 1 / meleeSpeed)
        {
            lastAttack = Time.time;
            anim.SetTrigger("CutlassAttack");
            if (friendly)
            {
                cooldownUI.animateCooldownBar(meleeSpeed);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent) && Friendly)
        {
            Debug.Log("Friendly Cutlass Attack");
            enemyComponent.TakeDamage(damage);
        }
        else if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent) && !Friendly)
        {
            Debug.Log("Enemy Cutlass Attack");
            playerComponent.TakeDamage(damage);
        }
    }
}

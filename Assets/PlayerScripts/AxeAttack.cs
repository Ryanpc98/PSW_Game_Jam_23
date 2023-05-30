using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeAttack : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private CooldownUI cooldownUI;
    [SerializeField] private float meleeSpeed = 0.1f;
    [SerializeField] private float damage = 8f;
    private float lastAttack = 0;
    private bool Friendly = true;

    public void Swing(bool friendly)
    {
        Friendly = friendly;
        if (Time.time - lastAttack > 1 / meleeSpeed)
        {
            lastAttack = Time.time;
            anim.SetTrigger("AxeAttack");
            cooldownUI.animateCooldownBar(meleeSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Enemy>(out Enemy enemyComponent) && Friendly)
        {
            enemyComponent.TakeDamage(damage);
        }
        if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerComponent) && !Friendly)
        {
            playerComponent.TakeDamage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponMelee : MonoBehaviour
{
    public Camera sceneCamera;
    private Vector3 mousePosition;
    private float aimAngle;
    private Quaternion aimRotation;
    public Transform target;

    private void Update()
    {
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        Look();
    }

    void ProcessInputs()
    {
        mousePosition = sceneCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    void Look()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        aimAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        aimRotation = Quaternion.Euler(0f, 0f, aimAngle);

        transform.rotation = aimRotation;
    }
}

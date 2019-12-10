using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaSlowEnemy4 : MonoBehaviour
{
    public float radius;
    public LayerMask lm;
    public void ShootRayCast()
    {
      //  if (!PlayerController.instance.isSlow)
            PlayerController.instance.isSlow = Physics2D.OverlapCircle(transform.position, radius, lm);
    }
    private void Update()
    {
        ShootRayCast();
    }
    private void OnDisable()
    {
        if (PlayerController.instance == null)
            return;
        PlayerController.instance.isSlow = false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

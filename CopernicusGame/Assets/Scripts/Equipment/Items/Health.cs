using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : ItemPickup
{
    public int AddedHealth;

    private new void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9 && collision is CircleCollider2D)
        {
            PlayerStats.instance.AddHealth(AddedHealth);
        }

        base.OnTriggerEnter2D(collision);
    }
}

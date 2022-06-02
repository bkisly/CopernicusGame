using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Obstacle : MonoBehaviour
{
    public int Damage;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            bool ignoreInviolacy = Damage >= 1000;
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(Damage, true, ignoreInviolacy);
        }
    }
}

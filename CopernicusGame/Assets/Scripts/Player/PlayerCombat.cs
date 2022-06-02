using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Transform AttackCenter;
    public Animator PlayerAnimator;
    public float AttackRadius = 1f;
    [Tooltip("Layers on which there are objects hittable by the Player.")] public LayerMask HitLayers;

    [Tooltip("Time after what Player can be hit again.")] public float SecondsHitInterval = 3f;
    [Tooltip("Time when the Player was hit lastly. Equals null if he hasn't been hit yet.")] private float? _lastHitTime = null;

    public float SecondsAttackInterval = 0.5f;
    private float? _lastAttackTime = null;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (PlayerAnimator.GetBool("IsJumping") == false && PlayerAnimator.GetBool("IsCrouching") == false)
            {
                if (Time.time >= _lastAttackTime + SecondsAttackInterval || _lastAttackTime == null)
                {
                    Attack();
                    PlayerAnimator.SetTrigger("Attack");
                    FindObjectOfType<AudioManager>().PlaySound("Attack");
                    _lastAttackTime = Time.time;
                }
            }
        }
    }

    public void TakeDamage(float damage, bool ignoreArmor, bool ignoreInviolacyTime = false)
    {
        if((Time.time >= _lastHitTime + SecondsHitInterval || _lastHitTime == null) || ignoreInviolacyTime)
        {
            _lastHitTime = Time.time;
            PlayerAnimator.SetTrigger("Damaged");
            FindObjectOfType<AudioManager>().PlaySound("PlayerDamage");

            if(ignoreArmor == false) PlayerStats.instance.ReduceHealth(damage);
            else PlayerStats.instance.ReduceHealthHardly(damage);
        }
    }

    private void Attack()
    {
        Collider2D[] hitObjects = Physics2D.OverlapCircleAll(AttackCenter.position, AttackRadius, HitLayers);

        foreach(Collider2D collider in hitObjects)
        {
            if(collider.gameObject.layer == 10)
            {
                print("hittin enemy");
                collider.gameObject.GetComponent<Enemy>().TakeDamage(PlayerStats.instance.AttackStrength);
            }
            if(collider.gameObject.layer == 11)
            {
                print("hittin boss");
                collider.gameObject.GetComponent<Boss>().TakeDamage(PlayerStats.instance.AttackStrength);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if(AttackCenter != null)
        {
            Gizmos.DrawWireSphere(AttackCenter.position, AttackRadius);
        }
    }
}

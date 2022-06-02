using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    public Animator EnemyAnimator;
    public GameObject DeathParticles;
    public int MaxHealth = 50;
    public int Damage = 10;
    public int Points;
    [SerializeField] private float _currentHealth;

    public UnityEvent EnemyDiedEvent;

    private void Start()
    {
        _currentHealth = MaxHealth;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 9)
        {
            collider.gameObject.GetComponent<PlayerCombat>().TakeDamage(Damage, false);
        }
    }

    public void TakeDamage(float damage)
    {
        _currentHealth -= damage;
        EnemyAnimator.SetTrigger("Damaged");
        PlayHitSound();

        if(_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        EnemyDiedEvent.Invoke();
        FindObjectOfType<AudioManager>().PlaySound("EnemyDeath");
        PlayerStats.instance.AddPoints(Points);
        Instantiate(DeathParticles, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void PlayHitSound()
    {
        System.Random random = new System.Random();
        FindObjectOfType<AudioManager>().PlaySound("Hit" + random.Next(1, 4));
    }
}

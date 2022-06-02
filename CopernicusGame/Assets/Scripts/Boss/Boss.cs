using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public class Boss : MonoBehaviour
{
    public GameObject Player;

    [Space]
    [Header("Stats")]

    public float MaxHealth = 500;
    public float CurrentHealth { get; private set; }

    public int Damage = 20;
    public int Speed = 20;
    public int JumpForce = 5000;

    [Space]
    [Header("Config")]
    [SerializeField] private BoxCollider2D _feetCollider;

    public Transform Left;
    public Transform Right;

    public Transform AttackCenter;
    public float AttackRadius;
    public LayerMask HitLayers;

    public UnityEvent BossDeadEvent;

    [Space]
    [Header("Animation effects")]

    public Animator BlackPanelAnimator;
    public GameObject EndingScreen;
    public GameObject ParticlesEffect;

    private bool _isGrounded = false;
    private Rigidbody2D _rigidbody;
    private Collider2D _playerCollider1, _playerCollider2;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        CurrentHealth = MaxHealth;

        _playerCollider1 = Player.GetComponent<BoxCollider2D>();
        _playerCollider2 = Player.GetComponent<CircleCollider2D>();

        Physics2D.IgnoreCollision(_feetCollider, _playerCollider1);
        Physics2D.IgnoreCollision(_feetCollider, _playerCollider2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            _isGrounded = true;
            GetComponent<Animator>().SetBool("IsJumping", false);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 8)
        {
            _isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            collision.gameObject.GetComponent<PlayerCombat>().TakeDamage(Damage, false);
        }
    }

    public void Move()
    {
        transform.position = new Vector3(transform.position.x - Speed / 10 * Time.deltaTime, transform.position.y);

        if (transform.position.x <= Left.position.x || transform.position.x >= Right.position.x)
        {
            transform.Rotate(new Vector3(0, 180));
            Speed *= -1;
        }
    }

    public void Attack()
    {
        Collider2D hitPlayer = Physics2D.OverlapCircle(AttackCenter.position, AttackRadius, HitLayers);

        if(hitPlayer != null)
        {
            print("a maf koper");
            hitPlayer.GetComponent<PlayerCombat>().TakeDamage(Damage, false);
        }
    }

    public void TakeDamage(float damage)
    {
        CurrentHealth -= damage;
        GetComponent<Animator>().SetTrigger("Damaged");
        PlayHitSound();

        if(CurrentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        BossDeadEvent.Invoke();
        Instantiate(ParticlesEffect, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().PlaySound("BossDeath");
        BlackPanelAnimator.SetTrigger("Finish");
        EndingScreen.SetActive(true);
        FindObjectOfType<LayoutsToggler>().DisableLayouts();
        Destroy(gameObject);
    }

    public void Jump()
    {
        _rigidbody.AddForce(new Vector2(0, JumpForce));
        _isGrounded = false;
    }

    private void PlayHitSound()
    {
        System.Random random = new System.Random();
        FindObjectOfType<AudioManager>().PlaySound("Hit" + random.Next(1, 4));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(AttackCenter.position, AttackRadius);
    }
}

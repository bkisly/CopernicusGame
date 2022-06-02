using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [Range(0, 500)]
    public float MaxHealth = 100;

    [Range(0, 100)]
    [Tooltip("The base attack strength without any modifications.")]
    public int BaseAttackStrength = 20;
    public int AttackStrength { get { return BaseAttackStrength + CalculateStrengthPowerups(); } }

    [SerializeField]
    private float _currentHealth;
    public float CurrentHealth { get { return _currentHealth; } }

    public int Armor { get { return (int)((1 - CalculateTakenDamage()) * 100); } }

    public int BaseSpeed = 20;
    public int Speed { get { return BaseSpeed + CalculateSpeedPowerups(); } }

    public int BaseJumpForce = 400;
    public int JumpForce { get { return BaseJumpForce + CalculateJumpPowerups(); } }

    public int Points = 0;

    public GameObject ParticlesEffect;
    public UnityEvent GameOverEvent;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        _currentHealth = MaxHealth;
    }

    public void SetCurrentHealth(float amount)
    {
        _currentHealth = amount;
    }

    public void AddHealth(float value)
    {
        _currentHealth += value;

        if (_currentHealth > MaxHealth) _currentHealth = MaxHealth;
    }

    public void ReduceHealth(float value)
    {
        ReduceHealthHardly(value * CalculateTakenDamage());
    }

    public void ReduceHealthHardly(float value)
    {
        if (_currentHealth <= value)
        {
            _currentHealth = 0;
            GameOver();
        }
        else
        {
            _currentHealth -= value;
        }
    }

    public void AddPoints(int points)
    {
        Points += points;
        print(string.Format("added {0} points, now you have {1} points", points, Points));
    }

    private void GameOver()
    {
        GameOverEvent.Invoke();
        Instantiate(ParticlesEffect, transform.position, transform.rotation);
        FindObjectOfType<AudioManager>().PlaySound("PlayerDeath");
        print("Game over!");
        Destroy(gameObject);
    }


    private int CalculateStrengthPowerups()
    {
        int strength = 0;

        foreach(Item item in Equipment.instance.EquippedItems)
        {
            if(item is ArmilarySphere)
            {
                ArmilarySphere armilarySphere = item as ArmilarySphere;
                strength += armilarySphere.AddedStrength;
            }
        }

        return strength;
    }

    /// <summary>
    /// Returns the damage ratio between 0 and 1 that the Player takes after calculating all equipped powerups.
    /// </summary>
    private float CalculateTakenDamage()
    {
        // List of dmg ratios of every single OerdeRune.
        List<int> dmgPercentageList = new List<int>();

        float damageRatio = 1f;

        /*
        If the Armor in the OerdeRune is e. x. equal to 45, Player takes 55% (100% - 45%) of given damage.
        All powerups are mulitplied by each other.
        */

        foreach(Item item in Equipment.instance.EquippedItems)
        {
            if(item is Astrolabe)
            {
                Astrolabe astrolabe = item as Astrolabe;
                dmgPercentageList.Add(100 - astrolabe.AddedArmor);
            }
        }

        foreach(int i in dmgPercentageList)
        {
            damageRatio *= i / 100f;
        }

        return damageRatio;
    }

    private int CalculateSpeedPowerups()
    {
        int speed = 0;

        foreach(Item item in Equipment.instance.EquippedItems)
        {
            if(item is Quadrant)
            {
                Quadrant quadrant = item as Quadrant;
                speed += quadrant.AddedSpeed;
            }
        }

        return speed;
    }

    private int CalculateJumpPowerups()
    {
        int jumpForce = 0;

        foreach(Item item in Equipment.instance.EquippedItems)
        {
            if(item is ParallacticTriangle)
            {
                ParallacticTriangle parallacticTriangle = item as ParallacticTriangle;
                jumpForce += parallacticTriangle.AddedJumpForce;
            }
        }

        return jumpForce;
    }
}

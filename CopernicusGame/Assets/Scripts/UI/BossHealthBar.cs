using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    public Slider HealthBar;
    public Boss Boss;
    public Vector2 Offset;

    private void Start()
    {
        HealthBar.maxValue = Boss.MaxHealth;
    }

    private void Update()
    {
        transform.position = Boss.transform.position + (Vector3)Offset;
        HealthBar.value = Boss.CurrentHealth;
    }

    public void DestroyHealthBar() { Destroy(gameObject); }
}

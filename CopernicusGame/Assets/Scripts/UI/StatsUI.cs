using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    public Text PointsText;
    public Slider HealthBar;

    public void Update()
    {
        PointsText.text = "Punkty: " + PlayerStats.instance.Points.ToString();
        HealthBar.value = PlayerStats.instance.CurrentHealth;
    }
}

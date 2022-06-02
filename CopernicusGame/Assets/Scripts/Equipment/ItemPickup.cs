using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Item Item;
    public GameObject ParticlesEffect;
    public float AnimationAmplitude;
    public float AnimationSpeed;
    public int Points;

    protected void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider is CircleCollider2D && collider.gameObject.layer == 9)
        {
            if(Item != null) Equipment.instance.Add(Item);
            PlayerStats.instance.AddPoints(Points);
            Instantiate(ParticlesEffect, transform.position, transform.rotation);
            PlaySound();
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + (AnimationAmplitude / 100) * Mathf.Sin(AnimationSpeed * Time.time));
    }

    private void PlaySound()
    {
        if (this is Health) FindObjectOfType<AudioManager>().PlaySound("HealthCollect");
        else if (Item == null) FindObjectOfType<AudioManager>().PlaySound("CoinCollect");
        else FindObjectOfType<AudioManager>().PlaySound("ItemCollect");
    }
}

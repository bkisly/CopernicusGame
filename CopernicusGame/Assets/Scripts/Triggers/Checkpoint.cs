using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Checkpoint : MonoBehaviour
{
    public Transform TargetPosition;
    public Rigidbody2D Player;
    public Animator BlackPanelAnimator;

    private float _timer = 0;
    private bool _hasThePlayerHit = false;
    private bool _hasBeenMoved = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If Player enters...
        if(collision.gameObject.layer == 9)
        {
            BlackPanelAnimator.SetTrigger("Checkpoint");
            _hasThePlayerHit = true;
            FindObjectOfType<ConfigManager>().SavePlayerProgress(TargetPosition.position, SceneManager.GetActiveScene().buildIndex);
        }
    }

    private void Update()
    {
        if(_hasThePlayerHit)
        {
            _timer += Time.deltaTime;
        }

        if(_timer >= 2f)
        {
            if (_hasBeenMoved == false)
            {
                Player.position = TargetPosition.position;
                _hasBeenMoved = true;
            }
        }
    }
}

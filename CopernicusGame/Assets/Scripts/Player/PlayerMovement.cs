using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController2D Controller;
    public Animator PlayerAnimator;

    private float _movementSpeed { get { return PlayerStats.instance.Speed; } }
    private float _direction = 0f;
    private bool _isJumping = false;
    private bool _isCrouching = false;

    private float _timer = 0f;

    private void Update()
    {
        _direction = Input.GetAxisRaw("Horizontal") * _movementSpeed;
        PlayerAnimator.SetFloat("Speed", Mathf.Abs(_direction));

        if (Input.GetButtonDown("Crouch"))
        {
             _isCrouching = true;
        }
        else if (Input.GetButtonUp("Crouch")) _isCrouching = false;

        if (Input.GetButtonDown("Jump") && PlayerAnimator.GetBool("IsCrouching") == false)
        {
            _isJumping = true;
            PlayerAnimator.SetBool("IsJumping", true);
        }

        PlayFootstepSound();
    }

    public void OnLanding() 
    {
        PlayerAnimator.SetBool("IsJumping", false);
        _isJumping = false;
    }

    public void OnCrouching(bool isCrouching)
    {
        PlayerAnimator.SetBool("IsCrouching", isCrouching);
    }

    private void FixedUpdate()
    {
        print("jumping - " + _isJumping);
        Controller.Move(_direction * Time.fixedDeltaTime, _isCrouching, _isJumping);
    }

    private void TimerTick(float maxTime)
    {
        _timer += Time.deltaTime;
        if (_timer >= maxTime) _timer = 0;
    }

    private void PlayFootstepSound()
    {
        System.Random random = new System.Random();

        if (_direction != 0 && !_isJumping)
        {
            if (_timer == 0) FindObjectOfType<AudioManager>().PlaySound("Footstep" + random.Next(1, 4));

            if (!_isCrouching) TimerTick(0.35f);
            else TimerTick(0.55f);
        }
        else _timer = 0f;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    public int NextSceneIndex;
    public Vector2 NextScenePlayerPosition;
    public Animator BlackPanelAnimator;

    private bool _hasThePlayerTouched = false;
    private float _timer = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == 9)
        {
            print("hai");
            BlackPanelAnimator.SetTrigger("Finish");
            _hasThePlayerTouched = true;
            FindObjectOfType<ConfigManager>().SavePlayerProgress(NextScenePlayerPosition, NextSceneIndex + 1);
        }
    }

    private void Update()
    {
        if(_hasThePlayerTouched)
        {
            _timer += Time.deltaTime;
        }

        if(_timer >= 2f)
        {
            // save the game
            SceneManager.LoadScene(NextSceneIndex);
        }
    }
}

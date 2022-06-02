using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public Animator BlackPanelAnimator;

    private float _timer = 0f;
    private bool _isGameOver = false;

    private void Update()
    {
        if (_isGameOver) 
        { 
            _timer += Time.deltaTime;
            if (_timer >= 2f) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void GameOver()
    {
        _isGameOver = true;
        BlackPanelAnimator.SetTrigger("Finish");
        FindObjectOfType<LayoutsToggler>().DisableLayouts();
    }
}

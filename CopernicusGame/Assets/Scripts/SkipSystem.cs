using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipSystem : MonoBehaviour
{
    private bool _isSkippable = false;
    private float _timer = 0f;

    public float SkipUIDuration;
    public int NextSceneIndex;
    public GameObject SkipUI;

    private void Update()
    {
        if (Input.anyKeyDown && !_isSkippable)
        {
            _isSkippable = true;
            SkipUI.SetActive(true);
            return;
        }

        if(_isSkippable)
        {
            print("Skippable. Timer: " + _timer);
            _timer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space)) Skip();

            if (_timer > SkipUIDuration)
            {
                _isSkippable = false;
                SkipUI.SetActive(false);
                _timer = 0f;
            }
        }
    }

    private void Skip()
    {
        SceneManager.LoadScene(NextSceneIndex);
    }
}

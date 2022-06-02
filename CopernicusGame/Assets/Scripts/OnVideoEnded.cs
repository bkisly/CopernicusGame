using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class OnVideoEnded : MonoBehaviour
{
    public VideoPlayer Player;
    public int NextSceneIndex;

    private void Start()
    {
        Player.loopPointReached += Player_loopPointReached;
    }

    private void Player_loopPointReached(VideoPlayer source)
    {
        SceneManager.LoadScene(NextSceneIndex);
    }
}

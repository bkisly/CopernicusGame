using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingNavigator : MonoBehaviour
{
    public void NavigateToMenu()
    {
        SceneManager.LoadScene(0);
    }
}

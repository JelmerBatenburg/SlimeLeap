using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupInfo : MonoBehaviour
{
    public AudioSource source;
    public string mainMenuScene;

    public void Start()
    {
        DontDestroyOnLoad(gameObject);
        SceneManager.LoadScene(mainMenuScene);
    }
}

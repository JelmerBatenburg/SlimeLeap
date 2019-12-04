using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasicUiFunctions : MonoBehaviour
{
    public float goToValue;
    public float currentValue;
    public float valueLerp;
    public Animator animator;

    public void SetHoverValue(float value)
    {
        goToValue = value;
    }

    public void Update()
    {
        if(animator)
        {
            currentValue = Mathf.Lerp(currentValue, goToValue, Time.deltaTime * valueLerp);
            animator.SetFloat("HoverValue", currentValue);
        }
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

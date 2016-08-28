using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashMenu : MonoBehaviour
{
    public Button StartButton;
    public GameObject LoadingText;

    void Start()
    {
        StartButton.onClick.AddListener(() =>
        {
            StartButton.gameObject.SetActive(false);
            LoadingText.SetActive(true);
            SceneManager.LoadScene("Game");
        });
    }
}

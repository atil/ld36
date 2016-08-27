using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Ui : MonoBehaviour
{
    public GameObject Crosshair;
    public GameObject GameOverText;
    public Button RestartButton;
    public Slider FireSlider;
    public Fire Fire;

    void Start()
    {
        RestartButton.onClick.AddListener(() => 
        {
            SceneManager.LoadScene("Game");
        });
    }

    public void GameOver()
    {
        RestartButton.gameObject.SetActive(true);
        GameOverText.SetActive(true);
    }

    void Update()
    {
        if (Fire.HasClay)
        {
            FireSlider.gameObject.SetActive(true);

            var pos = Camera.main.WorldToScreenPoint(Fire.SliderSlot.position);
            if (Vector3.Dot(Camera.main.transform.forward, 
                (Fire.transform.position - Camera.main.transform.position).normalized) > 0)
            {
                FireSlider.transform.position = pos;
                FireSlider.value = Fire.ClayPerc;

            }
        }
        else
        {
            FireSlider.gameObject.SetActive(false);
        }
    }

}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Ui : MonoBehaviour
{
    public GameObject FireSliderPrefab;
    public Fire[] Fires;
    public CrosshairView CrosshairView;

    public Text ScoreText;
    public Text BestText;
    public GameObject GameOverText;
    public Text ReasonText;
    public Button RestartButton;

    private List<Slider> _fireSliders = new List<Slider>();

    void Start()
    {
        RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });

        foreach (var f in Fires)
        {
            var sliderGo = Instantiate(FireSliderPrefab);
            sliderGo.transform.SetParent(transform);

            _fireSliders.Add(sliderGo.GetComponent<Slider>());
        }

    }

    public void GameOver(string message, float time)
    {
        RestartButton.gameObject.SetActive(true);
        GameOverText.SetActive(true);
        ReasonText.gameObject.SetActive(true);
        ReasonText.text = message;

        var scoreStr = (((int)time) / 60).ToString() + ":" + (((int)time) % 60).ToString();
        var bestTime = PlayerPrefs.GetFloat("time");
        var bestStr = (((int)bestTime) / 60).ToString() + ":" + (((int)bestTime) % 60).ToString();

        ScoreText.gameObject.SetActive(true);
        ScoreText.text += scoreStr;
        BestText.gameObject.SetActive(true);
        BestText.text += bestStr;

        CrosshairView.SetCrosshairVisibility(false);
    }

    void Update()
    {
        for (var i = 0; i < Fires.Length; i++)
        {
            var fire = Fires[i];
            var slider = _fireSliders[i];

            if (fire.HasClay)
            {
                _fireSliders[i].gameObject.SetActive(true);

                var pos = Camera.main.WorldToScreenPoint(fire.SliderSlot.position);
                if (Vector3.Dot(Camera.main.transform.forward,
                    (fire.transform.position - Camera.main.transform.position).normalized) > 0)
                {
                    slider.transform.position = pos;
                    slider.value = fire.ClayPerc;
                }
            }
            else
            {
                slider.gameObject.SetActive(false);
            }
        }


       
    }

}

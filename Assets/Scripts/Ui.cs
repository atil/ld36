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

    public GameObject GameOverText;
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

    public void GameOver()
    {
        RestartButton.gameObject.SetActive(true);
        GameOverText.SetActive(true);
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

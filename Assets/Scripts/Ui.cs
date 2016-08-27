using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System.Linq;

public class Ui : MonoBehaviour
{
    public GameObject GameOverText;
    public Button RestartButton;
    public Slider FireSlider;
    public Fire Fire;

    public RectTransform[] pieces = new RectTransform[4];

    private List<Vector2> screenVertices = new List<Vector2>();
    private Vector3[] vertices;
    private Vector3[] defaultPiecePositions = new Vector3[4];
    private Vector3[] targetPiecePositions = new Vector3[4];

    void Start()
    {
        RestartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });

        for (int i = 0; i < 4; i++)
        {
            defaultPiecePositions[i] = pieces[i].position;
        }

        // Init target position
        for (int i = 0; i < 4; i++)
        {
            targetPiecePositions[i] = defaultPiecePositions[i];
        }
    }

    public void GameOver()
    {
        RestartButton.gameObject.SetActive(true);
        GameOverText.SetActive(true);
        SetCrosshairVisibility(false);
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

        for (int i = 0; i < 4; i++)
        {
            pieces[i].position = Vector3.Lerp(pieces[i].position, targetPiecePositions[i], Time.deltaTime * 20);
        }
    }

    public void SetCrosshairTo(GameObject obj)
    {
        if (obj == null)
        {
            for (int i = 0; i < 4; i++)
            {
                targetPiecePositions[i] = defaultPiecePositions[i];
            }
            return;
        }
        var vertices = obj.GetComponent<MeshFilter>().mesh.vertices;

        screenVertices.Clear();

        foreach (var v in vertices)
        {
            var worldspaceV = obj.transform.TransformPoint(v);
            screenVertices.Add(Camera.main.WorldToScreenPoint(worldspaceV));
        }

        float minY = screenVertices.Min(v => v.y);
        float maxY = screenVertices.Max(v => v.y);
        float minX = screenVertices.Min(v => v.x);
        float maxX = screenVertices.Max(v => v.x);

        targetPiecePositions[0] = new Vector2(minX, maxY);
        targetPiecePositions[1] = new Vector2(maxX, maxY);
        targetPiecePositions[2] = new Vector2(minX, minY);
        targetPiecePositions[3] = new Vector2(maxX, minY);
    }

    public void SetCrosshairVisibility(bool isActive)
    {
        foreach (var piece in pieces)
        {
            piece.GetComponent<Image>().CrossFadeAlpha(isActive ? 1f : 0f, 0.1f, true);
        }
    }
}

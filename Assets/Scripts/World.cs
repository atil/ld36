using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class World : MonoBehaviour
{
    public Player Player;
    public Relic Relic;
    public ClaySpawner[] ClaySpawners;
    public RocketSpawner[] RocketSpawners;
    public Ui Ui;

    private float _gameTimer;

    void Start()
    {
        StartCoroutine(TimescaleCoroutine());
    }
    
    void Update()
    {
        _gameTimer += Time.deltaTime;
    }

    public void OnExplosion()
    {
        Time.timeScale = 0.01f;
    }

    IEnumerator TimescaleCoroutine()
    {
        while (true)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, 1f, Time.deltaTime * 15);
            yield return null;
        }
    }

    public void GameOver(MonoBehaviour destroyedAgent)
    {
        if (_gameTimer > PlayerPrefs.GetFloat("time", -1))
        {
            PlayerPrefs.SetFloat("time", _gameTimer);
        }

        var msg = string.Empty;
        if (destroyedAgent is Relic)
        {
            msg = "Relic destroyed";
        }
        else if (destroyedAgent is Player)
        {
            msg = "You are dead";
        }

        Ui.GameOver(msg, _gameTimer);

        foreach (var cs in ClaySpawners)
        {
            cs.enabled = false;
        }

        foreach (var rs in RocketSpawners)
        {
            rs.enabled = false;
        }

        Player.GetComponent<Rigidbody>().drag = 3;
        Player.BoxColl.enabled = true;
        Player.GetComponent<FirstPersonController>().IsCursorLocked = false;
        Player.GetComponent<FirstPersonController>().enabled = false;
        Player.MusicAudioSource.Stop();

        foreach (var rb in FindObjectsOfType<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(50, destroyedAgent.transform.position, 100, 10, ForceMode.VelocityChange);
        }
    }
}

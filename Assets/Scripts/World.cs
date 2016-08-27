using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class World : MonoBehaviour
{
    public Player Player;
    public Relic Relic;
    public ClaySpawner ClaySpawner;
    public RocketSpawner RocketSpawner;
    public Ui Ui;

    void Start()
    {
        StartCoroutine(TimescaleCoroutine());
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

    public void GameOver()
    {
        Ui.GameOver();

        ClaySpawner.enabled = false;
        RocketSpawner.enabled = false;

        Player.GetComponent<Rigidbody>().drag = 3;
        Player.BoxColl.enabled = true;
        Player.GetComponent<FirstPersonController>().IsCursorLocked = false;
        Player.GetComponent<FirstPersonController>().enabled = false;

        foreach (var rb in FindObjectsOfType<Rigidbody>())
        {
            rb.isKinematic = false;
            rb.AddExplosionForce(50, Relic.transform.position, 100, 10, ForceMode.VelocityChange);
        }
    }
}

using UnityEngine;
using System.Collections;

public class RocketSpawner : MonoBehaviour
{
    public GameObject RocketPrefab;

    private float _timer;
    private float _nextSpawnTime;

    void Start()
    {
        _nextSpawnTime = Random.Range(10f, 20f);
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _nextSpawnTime)
        {
            _timer = 0;
            _nextSpawnTime = Random.Range(25f, 35f);

            var x = Random.Range(20f, 30f);
            if (Random.value < 0.5)
            {
                x = -x;
            }

            var z = Random.Range(20f, 30f);
            if (Random.value < 0.5)
            {
                z = -z;
            }

            var rocketGo = Instantiate(RocketPrefab, new Vector3(x, Random.Range(1f, 1.8f), z), Quaternion.identity);
        }
    }

}

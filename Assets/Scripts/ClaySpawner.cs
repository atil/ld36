using UnityEngine;
using System.Collections;

public class ClaySpawner : MonoBehaviour
{
    public GameObject ClayPrefab;

    private float _timer = 10;
    private float _nextSpawnTime;
    private const int ClayLimit = 10;

    void Start()
    {
        _nextSpawnTime = Random.Range(3f, 5f);
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer > _nextSpawnTime)
        {
            _timer = 0;
            _nextSpawnTime = Random.Range(3f, 5f);

            if (FindObjectsOfType<Clay>().Length <= ClayLimit)
            {
                var pos = Random.insideUnitSphere * 20;
                pos.y = 1f;
                var clayGo = Instantiate(ClayPrefab, pos, Quaternion.identity);
            }
        }
    }
	
}

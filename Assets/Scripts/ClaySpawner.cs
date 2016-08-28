using UnityEngine;
using System.Collections;

public class ClaySpawner : MonoBehaviour
{
    public const int ClayLimit = 20;
    public static int ClayCount;

    public GameObject ClayPrefab;
    public Collider[] SpawnVolumes;

    private float _timer = 10;
    private float _nextSpawnTime;

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

            if (ClayCount <= ClayLimit)
            {
                Spawn();
            }
        }
    }

    void Spawn() 
    {
        ClayCount++;

        var bnds = SpawnVolumes[Random.Range(0, SpawnVolumes.Length - 1)].bounds;
        var x = Random.Range(bnds.center.x - bnds.extents.x, bnds.center.x + bnds.extents.x);
        var y = Random.Range(bnds.center.y - bnds.extents.y, bnds.center.y + bnds.extents.y);
        var z = Random.Range(bnds.center.z - bnds.extents.z, bnds.center.z + bnds.extents.z);

        var pos = new Vector3(x, y, z);
        Instantiate(ClayPrefab, pos, Quaternion.identity);

    }
	
}

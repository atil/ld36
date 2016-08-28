using UnityEngine;
using System.Collections;

public class RocketSpawner : MonoBehaviour
{
    public GameObject RocketPrefab;
    public Transform Target;
    public Collider[] SpawnVolumes;

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

            var bnds = SpawnVolumes[Random.Range(0, SpawnVolumes.Length - 1)].bounds;
            var x = Random.Range(bnds.center.x - bnds.extents.x, bnds.center.x + bnds.extents.x);
            var y = Random.Range(bnds.center.y - bnds.extents.y, bnds.center.y + bnds.extents.y);
            var z = Random.Range(bnds.center.z - bnds.extents.z, bnds.center.z + bnds.extents.z);

            var rocketGo = (GameObject) Instantiate(RocketPrefab, new Vector3(x, y, z), Quaternion.identity);
            rocketGo.GetComponent<Rocket>().Target = Target;
        }
    }

}

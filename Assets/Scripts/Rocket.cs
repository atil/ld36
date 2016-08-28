using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Rocket : MonoBehaviour
{
    public const float ExplosionRadius = 10f;
    public GameObject ExplosionPrefab;
    public float SidewaysSpeed;
    public Transform Target;
    public AudioClip ExplodeClip;

    private Transform _relicTransform;
    private float _speed = 1f;
    private Player _player;

    void Start()
    {
        _player = FindObjectOfType<Player>();
    }

    void Update()
    {
        var toRelic = (Target.position - transform.position).normalized;
        toRelic.y = 0;
        transform.up = toRelic;
        transform.Translate(toRelic * _speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.GetComponent<Brick>())
        {
            if (Vector3.Distance(_player.transform.position, transform.position) < ExplosionRadius)
            {
                FindObjectOfType<World>().GameOver(_player);
            }

            ExplodeEffect(1);

            foreach (var c in Physics.OverlapSphere(transform.position, 3))
            {
                if (c.GetComponent<Brick>())
                {
                    c.GetComponent<Rigidbody>().AddExplosionForce(250, transform.position, ExplosionRadius, 20, ForceMode.VelocityChange);

                    Destroy(c.gameObject, Random.Range(1f, 3f));
                }
            }
        }

        if (col.collider.GetComponent<Relic>())
        {
            FindObjectOfType<World>().GameOver(col.collider.GetComponent<Relic>());
            ExplodeEffect(10);
        }
    }

    public void ExplodeEffect(float magnitude)
    {
        var expGo = (GameObject)Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        expGo.transform.localScale = Vector3.one * magnitude;
        FindObjectOfType<World>().OnExplosion();
        AudioSource.PlayClipAtPoint(ExplodeClip, transform.position);
        Destroy(gameObject);
    }
}

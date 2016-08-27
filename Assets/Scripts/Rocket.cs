using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class Rocket : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public float SidewaysSpeed;

    private Transform _relicTransform;
    private float _speed = 1f;

	void Start()
    {
        _relicTransform = FindObjectOfType<Relic>().transform;
    }

    void Update()
    {
        var toRelic = (_relicTransform.position - transform.position).normalized;
        toRelic.y = 0;
        transform.up = toRelic;
        transform.Translate(toRelic * _speed * Time.deltaTime, Space.World);

    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.GetComponent<Brick>())
        {

            Destroy(gameObject);
            ExplodeEffect(1);

            foreach (var c in Physics.OverlapSphere(transform.position, 3))
            {
                if (c.GetComponent<Brick>())
                {
                    c.GetComponent<Rigidbody>().AddExplosionForce(50, transform.position, 10, 10, ForceMode.VelocityChange);

                    Destroy(c.gameObject, Random.Range(1f, 3f));
                }
            }
        }

        if (col.collider.GetComponent<Relic>())
        {
            FindObjectOfType<World>().GameOver();
            ExplodeEffect(10);
            Destroy(gameObject);
        }
    }

    public void ExplodeEffect(float magnitude)
    {
        var expGo = (GameObject)Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        expGo.transform.localScale = Vector3.one * magnitude;
        FindObjectOfType<World>().OnExplosion();

    }
}

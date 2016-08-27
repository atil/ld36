using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour
{
    public GameObject BrickPrefab;

    private bool _hasClay;
    public bool HasClay { get { return _hasClay; } }

    public void TakeClay(Clay clay)
    {
        clay.transform.SetParent(transform, false);
        Destroy(clay.gameObject);
        _hasClay = true;

        StartCoroutine(TransformClayToBrick());
    }

    IEnumerator TransformClayToBrick()
    {
        for (float f = 0; f < 3f; f += Time.deltaTime)
        {
            // Loading bar
            yield return null;
        }

        var brickGo = (GameObject)Instantiate(BrickPrefab, transform.position,
            Quaternion.Euler(new Vector3(Random.value * 100, Random.value * 100, Random.value * 100)));

        brickGo.GetComponent<Rigidbody>().AddForce(Random.value, 10, Random.value, ForceMode.VelocityChange);
        _hasClay = false;

    }


}

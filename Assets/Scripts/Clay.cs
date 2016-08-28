using UnityEngine;
using System.Collections;

public class Clay : MonoBehaviour
{
    public AudioClip BakeClip;

    void OnCollisionEnter(Collision col)
    {
        var fire = col.collider.GetComponent<Fire>();
        if (fire != null && !fire.HasClay)
        {
            AudioSource.PlayClipAtPoint(BakeClip, transform.position);
            ClaySpawner.ClayCount--;
            fire.TakeClay(this);
        }
    }

}

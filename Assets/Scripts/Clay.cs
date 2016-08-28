using UnityEngine;
using System.Collections;

public class Clay : MonoBehaviour
{

    void OnCollisionEnter(Collision col)
    {
        var fire = col.collider.GetComponent<Fire>();
        if (fire != null && !fire.HasClay)
        {
            ClaySpawner.ClayCount--;
            fire.TakeClay(this);
        }
    }

}

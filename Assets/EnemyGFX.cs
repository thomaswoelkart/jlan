using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyGFX : MonoBehaviour
{
    public AIPath aiPath;

    void Update()
    {
        if (aiPath.desiredVelocity.x >= 0.01f)
        {
            transform.localScale = new Vector3(-3f, 3f, 3f);
            //Sprint(transform.parent.GetChild(1).name);
            //transform.parent.GetChild(1).Rotate(0f, 180f, 0);
        }
        else if (aiPath.desiredVelocity.x <= -0.01f)
        {
            transform.localScale = new Vector3(3f, 3f, 3f);
            //print(transform.parent.GetChild(1).name);
        }
    }
}

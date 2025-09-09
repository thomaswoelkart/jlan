using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoDamage : MonoBehaviour
{
    BatScript bat;

    // Start is called before the first frame update
    void Start()
    {
        bat = transform.parent.GetComponent<BatScript>();
    }

    void attackEvent()
    {
        //print(bat.name);
        bat.doDamage();
    }
}

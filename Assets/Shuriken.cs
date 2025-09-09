using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shuriken : MonoBehaviour
{
    bool waitS;

    public Transform firePoint;
    public int damage = 5;
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButton("Fire1") && !waitS)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        StartCoroutine(TimeCounter());
    }

    IEnumerator TimeCounter()
    {
        waitS = true;
        yield return new WaitForSeconds(1);
        waitS = false;
    }
}

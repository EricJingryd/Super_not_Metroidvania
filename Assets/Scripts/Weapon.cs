using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform bulletStart;
    public GameObject bulletPrefab;
    public GameObject tripleBeamPrefab;
    public bool playerHasTripleBeam = false;
    bool switchWeapon = false;
    float f_bulletCooldown = 0.3f;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !switchWeapon && playerHasTripleBeam)
        {
            switchWeapon = true;
        }

        else if(Input.GetKeyDown(KeyCode.Q) && switchWeapon)
        {
            switchWeapon = false;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            if (!switchWeapon)
            {
                Shoot();
            }

            else if(switchWeapon && f_bulletCooldown >= 0.3f)
            {
                Shoot();
                f_bulletCooldown = 0;
            }
        }

        f_bulletCooldown += Time.deltaTime;
    }
    void Shoot()
    {
        if(playerHasTripleBeam && switchWeapon)
        {
            Instantiate(tripleBeamPrefab, bulletStart.position, bulletStart.rotation);
        }

        else if (!switchWeapon)
        {
            Instantiate(bulletPrefab, bulletStart.position, bulletStart.rotation);
        }
    }
}

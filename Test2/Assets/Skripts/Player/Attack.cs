using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 10f;
    public float typeOfAttack = 1;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.timeScale == 1.0f)
        {
            Shoot();
        }
    }
    
    void Shoot()
    {
        //создание экземпляра пули и придание ему ускорения
        Quaternion a = firePoint.rotation;
        if (typeOfAttack == 1)
        {
            a.eulerAngles += new Vector3(0, 0, 90);
        }
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, a);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
    }
}

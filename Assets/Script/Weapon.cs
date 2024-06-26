using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePoint; // Le point d'où les balles sont tirées
    public GameObject bulletPrefab; // Le prefab de la balle
    public float fireRate = 50f; // Nombre de balles tirées par seconde
    private float nextTimeToFire = 0f; // Le temps d'attente avant de pouvoir tirer à nouveau

    // Update is called once per frame
    void Update()
    {
        // Vérifie si le joueur appuie sur le bouton de tir (ex. bouton de la souris) et si le délai est écoulé
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate; // Met à jour le temps du prochain tir
            Shoot();
        }
    }

    void Shoot()
    {
        // Instancie la balle au point de tir
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
    }
}
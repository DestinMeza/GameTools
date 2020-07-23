using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools.Components
{
    public class ProjectileLauncher : MonoBehaviour
    {
        public delegate void OnReload();
        public OnReload onReload = delegate{};
        public string bulletName = "";
        public bool inheritVel = true;
        public int ammoDecay = 1;
        public int maxAmmo = 10;
        public float fireRate = 1;
        public float reloadTime = 5;
        public float accuracy = 1;
        public bool useReload = false; 
        public ForceMode forceType;
        public ForceMode2D forceType2D;
        int ammo = 10;
        float lastShotTime = -1;
        float lastReloadTime = -1;
        bool reloading = false;
        Rigidbody rb;
        Rigidbody2D rb2D;
        void Awake(){
            rb = GetComponentInParent<Rigidbody>();
            if(rb == null) rb2D = GetComponentInParent<Rigidbody2D>();

            if(rb == null && rb2D == null) useReload = false;
        }

        public void Fire(Vector3 targetDir){
            
            if(Time.time - lastReloadTime > reloadTime){
                if(reloading){
                    onReload();
                    ammo = maxAmmo;
                    reloading = false;
                }

                if(Time.time - lastShotTime > fireRate){
                    GameObject bullet = SpawnManager.Spawn(bulletName, transform.position);
                    Projectile bulletBody = bullet.GetComponent<Projectile>();
                    if(bulletBody != null){
                        bullet.SetActive(true);
                        lastShotTime = Time.time;
                        Vector3 actualDir = Quaternion.AngleAxis(180 * Random.Range(accuracy-1, 1-accuracy), Vector3.up) * targetDir;
                        actualDir += (Quaternion.AngleAxis(180 * Random.Range(accuracy-1, 1-accuracy), Vector3.right) * targetDir);
                        bulletBody.Shoot(actualDir);
                        if(useReload) ammo -= ammoDecay;
                    }
                }

                if(ammo <= 0){
                    lastReloadTime = Time.time;
                    reloading = true;
                }
            }
        }
    }
}


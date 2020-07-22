using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools.Components
{
    public class Projectile : MonoBehaviour
    {
        public float lifeTime = 1;
        public float maxSpeedChange = 1;
        public bool dieOffScreen = false;
        public bool dieOnCollision = true; 
        Rigidbody2D rb2D;
        Rigidbody rb;
        Vector3 targetVel;
        Vector2 targetVel2D;
        public float speed;
        bool launched = false;
        float lastLaunchTime = 0;
        public string[] ignoreTags = new string[1] {"Ignore"};
        Camera cam;
        void Awake(){
            cam = FindObjectOfType<Camera>();
            rb = GetComponentInParent<Rigidbody>();
            rb2D = GetComponentInParent<Rigidbody2D>();
        }
        void Update(){
            if(dieOffScreen){
                Vector3 pos = cam.WorldToViewportPoint(transform.position);
                if(pos.x > 1 || pos.x < 0 || pos.y < 0 || pos.y > 1) gameObject.SetActive(false);
            }
            if(Time.time - lastLaunchTime > lifeTime){
                gameObject.SetActive(false);
            }
        }

        void OnDisable(){
            launched = false;
        }

        void FixedUpdate()
        {
            if(launched){
                if(rb != null) rb.velocity = targetVel.normalized * speed;
                else{
                    rb2D.velocity = targetVel2D.normalized * speed;
                }
            }

        }

        public void Shoot(Vector3 dir){
            if(rb != null){
                targetVel = dir;
            }
            else{
                targetVel2D = dir;
            }
            lastLaunchTime = Time.time;
            launched = true;
        }

        void OnCollisionEnter(Collision col){
            foreach(string tag in ignoreTags){
                if(gameObject.CompareTag(tag)){
                    return;
                }
            }
            if(dieOnCollision)gameObject.SetActive(false);
        }

        void OnCollisionEnter2D(Collision2D col){
            foreach(string tag in ignoreTags){
                if(gameObject.CompareTag(tag)){
                    return;
                }
            }
            if(dieOnCollision)gameObject.SetActive(false);
        }
    }
}

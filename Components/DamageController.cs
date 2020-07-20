using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools.Components
{
    public class DamageController : MonoBehaviour
    {
        public delegate void OnHit(Transform t);
        public OnHit onHit = delegate {};

        public string hitSound = "";
        public string hitParticles = "";
        int defaultDmg;
        public int damage = 1;

        void Awake(){
            defaultDmg = damage;
        }

        void OnEnable(){
            damage = defaultDmg;
        }

        void OnCollisionEnter(Collision col){
            Hit(col.gameObject);
        }

        void OnTriggerEnter(Collider col){
            Hit(col.gameObject);
        }

        void OnCollisionEnter2D(Collision2D col){
            Hit(col.gameObject);
        }

        void OnTriggerEnter2D(Collider2D col){
            Hit(col.gameObject);
        }

        void Hit(GameObject g){
            HealthController h = g.GetComponentInParent<HealthController>();
            if(h == null) return;
            onHit(g.transform);
            h.TakeDamage(damage);
        }
    }
}
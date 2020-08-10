using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameTools.Components;
public class KnockbackController : MonoBehaviour
{
    HealthController health;
    public float knockbackStrength;
    public bool usePercentScale = false;
    float healthScale = 1;
    Rigidbody rb;
    void Awake(){
        health = GetComponent<HealthController>();
        rb = GetComponent<Rigidbody>();
    }

    // void OnEnable(){
    //     health.onKnockBack += Knockback;
    // }

    // void OnDisable(){
    //     health.onKnockBack -= Knockback;
    // }

    public void Knockback(Transform t){
        Vector3 knockBackDir = t.position - transform.position;
        if(usePercentScale) healthScale = health.maxHealth/health.health;
        rb.AddForce(knockBackDir.normalized * knockbackStrength * healthScale * Physics.gravity.magnitude * rb.mass * -1, ForceMode.Force);
    }
    public void Knockback(Vector3 contactPoint){
        Vector3 knockBackDir = contactPoint - transform.position;
        if(usePercentScale) healthScale = health.maxHealth/health.health;
        rb.AddForce((knockBackDir.normalized * knockbackStrength * healthScale * Physics.gravity.magnitude * rb.mass) * -1, ForceMode.Force);
    }

    void OnCollisionEnter(Collision col){
        Vector3 closestPoint = new Vector3(Mathf.Infinity, Mathf.Infinity, Mathf.Infinity);
        for(int i = 0; i < col.contacts.Length; i++){
            if(col.contacts[i].point.magnitude < closestPoint.magnitude && col.gameObject.GetComponent<DamageController>()){
                closestPoint = col.contacts[i].point;
            }
        }
        if(closestPoint.magnitude > 5) return;
        Knockback(closestPoint);
    }
}

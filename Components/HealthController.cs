﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameTools.Components
{
    public class HealthController : MonoBehaviour
    {

        public delegate void OnIncreaseScore(int score);
        public static event OnIncreaseScore onIncreaseScore = delegate{};
        public delegate void OnAnyDeath(HealthController health);
        public static OnAnyDeath onAnyDeath =delegate {};
        public delegate void OnDeath(HealthController health);
        public OnDeath onDeath = delegate {};
        public delegate void OnHealthIncrease();
        public OnHealthIncrease onHealthIncrease = delegate {};
        public delegate void OnHealthDecrease();
        public OnHealthDecrease onHealthDecrease = delegate {};
        public float regenTick = 1;
        public float regenDelay = 2;
        public int regenAmount = 1; 
        public int maxHealth = 3;
        public int health;
        public int scoreValue = 5;
        public bool projectScore = false;
        public bool onDeathSetInactive = true;
        public bool healOnEnable = true;
        public bool invulnerble = false;
        public bool useRegen = true;
        bool regenerating = false;
        bool addLife = false;
        float lastHitTime = -1;
        float lastTickTime = -1;
        void OnEnable(){
            if(healOnEnable){
                health = maxHealth;
                onHealthIncrease();
            }
        }

        public bool isAlive(){
            return health > 0;
        }
        public int GetHealth(){
            return health;
        }

        void Update(){
            if(regenerating && useRegen){
                
                if(Time.time - lastTickTime <= regenTick){
                    addLife = true;
                }
                
                if(addLife){
                    if(Time.time - lastHitTime <= regenDelay){
                        float timer = Time.time - lastHitTime;
                        Debug.Log(gameObject.name + " : " + (timer).ToString());
                    }
                    if(health >= maxHealth) regenerating = false;
                    else{
                        health += regenAmount;
                    }
                    addLife = false;
                }
                
            }
        }

        public void TakeDamage(int damage){
            if(invulnerble) return;
            health -= damage;
            lastHitTime = Time.time;
            onHealthDecrease();
            Debug.Log(gameObject.name + "Took Damage");
            if(health <= 0){
                health = 0;
                if(onDeathSetInactive) gameObject.SetActive(false);
                if(projectScore) onIncreaseScore(scoreValue);
                onDeath(this);
                onAnyDeath(this);
            }
            if(!regenerating && useRegen) regenerating = true;
        }

        public void IncreaseHeath(int health){
            if(this.health + health >= maxHealth){
                this.health = maxHealth;
            }
            else{
                this.health += health;
            }
            onHealthIncrease();
        }
    }
}
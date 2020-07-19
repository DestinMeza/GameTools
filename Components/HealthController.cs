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

        public int maxHealth = 3;
        public int health;
        public int scoreValue = 5;
        public bool projectScore = false;
        public bool onDeathSetInactive = true;
        public bool healOnEnable = true;
        public bool invulnerble = false;

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
        public void TakeDamage(int damage){
            if(invulnerble) return;
            health -= damage;
            onHealthDecrease();
            Debug.Log(gameObject.name + "Took Damage");
            if(health <= 0){
                health = 0;
                if(onDeathSetInactive) gameObject.SetActive(false);
                if(projectScore) onIncreaseScore(scoreValue);
                onDeath(this);
                onAnyDeath(this);
            }
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
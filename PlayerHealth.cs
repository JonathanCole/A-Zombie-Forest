using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] TextMeshProUGUI healthText;
    public void TakeDamage(float damage){
        health -= damage;
        if(health <= 0){
            GetComponent<DeathHandler>().HandleDeath();
        }
    }

    void Update(){
        DisplayHealth();
    }

    void DisplayHealth(){
        healthText.text = "Health: " + health.ToString();
    }
}

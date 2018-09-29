using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    PlayerHealth health;
    Text uiText;

    void Awake()
    {
        health = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        uiText = GetComponent<Text>();
    }
	
	void Update () {
        uiText.text = "Health: " + health.GetHealth() + " / " + health.maxHealth;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    Health health;
    Text uiText;

    void Awake()
    {
        health = GameObject.FindWithTag("Player").GetComponent<Health>();
        uiText = GetComponent<Text>();
    }
	
	void Update () {
        uiText.text = "Health: " + health.GetHealth() + " / " + health.maxHealth;
	}
}

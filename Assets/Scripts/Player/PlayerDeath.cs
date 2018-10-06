using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerDeath : MonoBehaviour {

    public Camera MainCamera;
    public Camera LoadingScreenCamera;
    private Health health;
    private PlayerMovement movementEngine;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnDeath += StartPlayerRespawnRoutine;
        movementEngine = GetComponent<PlayerMovement>();
    }

    private void StartPlayerRespawnRoutine()
    {
        StartCoroutine(OnPlayerDeath());
    }

    private IEnumerator OnPlayerDeath()
    {
        DisableMovement();
        PlayDeathAnimation();
        BlackOut();
        RespawnPlayer();
        RecoverHealth();
        RespawnEnemys();
        yield return new WaitForSeconds(0.1f);
        yield return new WaitUntil(() => movementEngine.IsGrounded);
        BlackOutOver();
        EnableMovement();
    }

    private void PlayDeathAnimation()
    {
        Debug.Log("PlayDeathAnimation ist noch nicht implementiert!");
    }

    private void EnableMovement()
    {
        movementEngine.enabled = true;
    }

    private void DisableMovement()
    {
        movementEngine.Stop();
        movementEngine.enabled = false;
    }

    private void RespawnEnemys()
    {
        Debug.Log("RespwamEnemy ist noch nicht implementiert!");
    }

    private void RecoverHealth()
    {
        health.TakeHeal(health.maxHealth);
    }

    private void BlackOutOver()
    {
        MainCamera.enabled = true;
        LoadingScreenCamera.enabled = false;
    }

    private void BlackOut()
    {
        LoadingScreenCamera.enabled = true;
        MainCamera.enabled = false;
    }

    private void RespawnPlayer()
    {
        GameObject respawnPoint = SceneState.Instance.lastRespawnPoint.gameObject;
        transform.position = respawnPoint.transform.position;
        transform.position += new Vector3(0, 2);
    }
}

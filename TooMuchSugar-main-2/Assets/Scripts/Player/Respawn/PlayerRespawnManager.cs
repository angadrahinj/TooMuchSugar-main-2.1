using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class PlayerRespawnManager : MonoBehaviour
{
    public static PlayerRespawnManager instance;
    public GameObject player;
    public PlayerData playerData;
    public float deathTimer = 2f;
    public Vector3 currentRespawnPoint;
    public Vector3 temporaryRespawnPoint;
    public Transform halfwayRespawnPoint;
    public Transform startingRespawnPoint;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        halfwayRespawnPoint = transform.Find("HalfwayRespawn");
        startingRespawnPoint = transform.Find("StartingRespawn");
        temporaryRespawnPoint = startingRespawnPoint.position;
    }

    void Start()
    {
    }
    
    void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
    }

    void OnDisable()
    {
        player = null;
    }

    #region Death Sequence
    public void StartDeathSequence()
    {
        StartCoroutine(DeathSequence());
    }

    IEnumerator DeathSequence()
    {

        player.SetActive(false);
        Debug.Log("Current respawn point: " + currentRespawnPoint);
        yield return new WaitForSeconds(deathTimer);
        player.transform.position = currentRespawnPoint;
        player.SetActive(true);
        HealthBar.instance.SetMaxHealth(playerData.maxHealth);
        player.GetComponent<PlayerHealthManager>().SetHealth(playerData.maxHealth);
    }
    #endregion

    #region Temporary Respawn Sequence
    public void StartTemporaryRespawnSequence()
    {
        StartCoroutine(TemporaryRespawnSequence());
    }

    IEnumerator TemporaryRespawnSequence()
    {

        player.SetActive(false);
        Debug.Log("Current respawn point: " + temporaryRespawnPoint);
        yield return new WaitForSeconds(deathTimer);
        player.transform.position = temporaryRespawnPoint;
        player.SetActive(true);
    }

    #endregion

    #region Set Respawn Points
    public void SetRespawnPoint(Vector3 position)
    {
        currentRespawnPoint = position;
        Debug.Log("Respawn Complete");
    }

    public void SetTemporaryRespawnPoint(Vector3 position)
    {
        temporaryRespawnPoint = position;
    }

    public void SetHalfwayRespawnPoint()
    {
        SetRespawnPoint(halfwayRespawnPoint.transform.position);
    }
    #endregion
}

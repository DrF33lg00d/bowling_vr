using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using Object = System.Object;

public class BowlingSystem : MonoBehaviour
{
    public List<GameObject> balls;
    public Transform ballSpawnLocation;
    public GameObject pinsPosition;
    public GameObject pinsPrefab;
    public PlayerScore player;
    public GameManager manager;
    
    private GameObject _deletedBall;
    private float _timer;
    private bool _needCount = false;
    private bool _isFirstRoll = true;
    private PinSetter _setter;
    
    void Start()
    {
        _setter = gameObject.AddComponent<PinSetter>();
        Instantiate(pinsPrefab, pinsPosition.transform.position, Quaternion.identity, transform);
        _setter.FindPins();
        player = player.GetComponent<PlayerScore>();
        manager = GameObject.Find("GameManager").GetComponent<GameManager>();
        manager.SetPinSetter(_setter);
    }

    void Update()
    {
        _timer += Time.deltaTime;

        // spawn ball, if ball was deleted on end of road
        if (_timer > 5f && _deletedBall)
        {
            //position to spawn ball
            Vector3 spawnBallLocation = ballSpawnLocation.transform.position + new Vector3(0, 0.2f, 0);
            Instantiate(_deletedBall, spawnBallLocation, Quaternion.identity);
            _deletedBall = null;
        }

        // some
        if (_timer > 3f && _needCount)
        {
            _needCount = false;
            RespawnPins();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            foreach (var ball in balls)
            {
                if (ball.name == other.gameObject.name.Replace("(Clone)", ""))
                {
                    _deletedBall = ball;
                    break;
                }
            }
            Destroy(other.gameObject);
            _timer = 0;
            _needCount = true;

        }
    }

    // don't use it
    // public List<GameObject> CreateAndGetPins(Vector3 startPosition)
    // {
    //     List<GameObject> resultPins = new List<GameObject>();
    //
    //     // first row
    //     GameObject pin = Instantiate(pinPrefab, startPosition, Quaternion.identity);
    //     resultPins.Add(pin);
    //     //second row
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(-0.15f, 0, 0.26f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(0.15f, 0, 0.26f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     //third row
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(-0.3f, 0, 0.52f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(0.0f, 0, 0.52f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(0.3f, 0, 0.52f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     //forth row
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(-0.45f, 0, 0.78f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(-0.15f, 0, 0.78f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(0.0f, 0, 0.78f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(0.15f, 0, 0.78f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     pin = Instantiate(pinPrefab, startPosition + new Vector3(0.45f, 0, 0.78f ), Quaternion.identity);
    //     resultPins.Add(pin);
    //     
    //     return resultPins;
    // }

    private void RespawnPins()
    {
        int standingPins = _setter.CountStanding();
        bool isStrike = _isFirstRoll && standingPins == 0;
        
        
        if (isStrike)
        {
            player.AddStrike();
            _setter.ResetAllPins();
        }
        else if (_isFirstRoll)
        {
            player.AddResultRoll(standingPins, _isFirstRoll);
            _setter.ResetStandingPins();
        }
        else
        {
            player.AddResultRoll(standingPins, _isFirstRoll);
            _setter.ResetAllPins();
        }
        
        _isFirstRoll = isStrike || !_isFirstRoll;

        _setter.SetPinsToStart();
        manager.UpdateScoreUI();
    }
    
}

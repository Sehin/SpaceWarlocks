using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Team : MonoBehaviour
{
    public int id;
    private List<Character> players = new List<Character>();
    private SpawnPoint[] spawnPoints;

    void Start()
    {
        spawnPoints = GetComponentsInChildren<SpawnPoint>();
    }

    public SpawnPoint[] SpawnPoints
    {
        get { return spawnPoints; }
    }

    public List<Character> Players
    {
        get { return players; }
        set { players = value; }
    }

    public void addPlayer(Character character)
    {
        players.Add(character);
        character.Team = this;
    }

    public SpawnPoint getRandomSpawnPoint()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Length)];
    }
}

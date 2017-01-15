using System;
using UnityEngine;
using UnityEngine.Networking;
using Random = UnityEngine.Random;

public class GameManager : NetworkManager
{
    public int AICount = 3;
    public bool spawnBots;
    public bool autoTeamBalance;

    public GameObject map;
    public GameObject AIPrefab;

    private Boolean isInit = false;
    private Team[] teams;

    public override void OnStartServer(){
        Debug.Log("OnStartServer isNetworkServerActive:" + NetworkServer.active);
        teams = map.GetComponentsInChildren<Team>();
    }


    //Вызывается при подключении первого игрока
    //Тут вся инициализация, которая треьбует работы с сервером.
    public void init()
    {
        isInit = true;

        if (spawnBots)
        {
            SpawnPoint[] spawnPoints = teams[1].SpawnPoints;

            for (int i = 0; i < AICount; i++)
            {
                SpawnPoint sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
                Debug.Log("Spawn bot");
                var bot = Instantiate(AIPrefab, sp.transform.position, Quaternion.identity);
                Character botChar = bot.GetComponent<Character>();
                NetworkServer.Spawn(bot);

                botChar.GameManager = this;
                teams[1].addPlayer(botChar);
            }
        }
    }

    public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
    {
        if (!isInit) init();

        Team team = teams[0];

        if (autoTeamBalance)
        {
            int min = team.Players.Count;
            foreach (var t in teams)
            {
                if (t.Players.Count < min)
                {
                    min = t.Players.Count;
                    team = t;
                }
            }
        }
        else
        {
            team = teams[Random.Range(0, teams.Length)];
        }

        //Выбираем значит спавн для команды
        SpawnPoint sp = team.getRandomSpawnPoint();
        var player = Instantiate(playerPrefab, sp.transform.position, Quaternion.identity);
        Character playerChar = player.GetComponent<Character>();
        playerChar.GameManager = this;
        team.addPlayer(playerChar);
        NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
    }

    #region events
    public void onCharacterDeath(Character character, Character killer)
    {
        Debug.Log("Start timer for respawn");
        character.enabled = false;
        TimersManager.Instance.addTimer(new TimersManager.CallbackTimer(3, true, () =>
        {
            character.GetComponent<Health>().healFullHealth();
            character.respawnAt(character.Team.getRandomSpawnPoint());
            character.enabled = true;
        }));
    }

    #endregion
}

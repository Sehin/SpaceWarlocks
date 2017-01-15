using UnityEngine.Networking;
using UnityEngine;

public class SceneManagerScript : NetworkBehaviour {
    GameObject[] spawnPoints;
    // Use this for initialization
    public override void OnStartServer () {
        spawnPoints = GameObject.FindGameObjectsWithTag("Spawn point"); // Получаем все спаун точки

        foreach (var spawnPoint in spawnPoints)  // Перебираем спаун точки, на каждую создаем экземпляр префаба Character.
        {
            var character = (GameObject)Instantiate(Resources.Load("Character"));
            character.transform.position = spawnPoint.transform.position;

            {
                character.AddComponent<AIController>();
            }
            
        }

    }

    // Update is called once per frame
    void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MyNetworkManager : NetworkManager {
    public override void OnClientConnect(NetworkConnection conn)
    {
        base.OnClientConnect(conn);
        Debug.Log("Client connected");
    }

}

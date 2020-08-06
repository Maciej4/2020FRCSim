using Mirror;
using Mirror.Cloud.Example;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class NetworkManagerFRC : NetworkManagerListServer
{
    [SerializeField] Transform[] strtPos;

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Debug.Assert(strtPos.Length == 2, "Pong Scene should have 2 start Poitions");
        // add player at correct spawn position
        Transform startPos = numPlayers == 0 ? strtPos[0] : strtPos[1];

        GameObject player = startPos != null
            ? Instantiate(playerPrefab, startPos.position, startPos.rotation)
            : Instantiate(playerPrefab);

        NetworkServer.AddPlayerForConnection(conn, player);
    }
}

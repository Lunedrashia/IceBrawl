using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Prototype.NetworkLobby;
using UnityEngine.Networking;

public class PlayerInfo_Hook : LobbyHook {

    public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
    {
        LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();
        GamePlayerInfo player = gamePlayer.GetComponent<GamePlayerInfo>();
        PlayerColor pc = gamePlayer.GetComponent<PlayerColor>();

        player.playerName = lobby.playerName;
        pc.color = lobby.playerColor;
    }
}

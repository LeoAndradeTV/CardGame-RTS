using JetBrains.Annotations;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;


public static class Support
{
    private const string ROOM_ID = "RoomID";

    public static List<Player> GetPlayersInRoom(Dictionary<int, Player> playersPair)
    {
        List<Player> list = new List<Player>();
        foreach (KeyValuePair<int, Player> pair in playersPair)
        {
            list.Add(pair.Value);
        }
        return list;
    }

    public static void SetPlayersNumbers(List<Player> players)
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].CustomProperties[ROOM_ID] = i;
        }
    }

    public static int GetPlayerRoomId(Player player)
    {
        if (!player.CustomProperties.ContainsKey(ROOM_ID)) { return -1; }

        return (int)player.CustomProperties[ROOM_ID];
    }

    public static List<Player> SortListOfPlayers(List<Player> players)
    {
        List<Player> sortedList = new List<Player>();
        while (players.Count > 0)
        {
            Player lowestPlayer = players[0];
            for (int i = 0; i < players.Count; i++)
            {
                if (players[i].ActorNumber < lowestPlayer.ActorNumber)
                {
                    lowestPlayer = players[i];
                }
            }
            sortedList.Add(lowestPlayer);
            players.Remove(lowestPlayer);
        }
        return sortedList;
    }
}

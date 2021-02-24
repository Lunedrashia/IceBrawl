using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Leaderboard : MonoBehaviour
{
    public static Leaderboard instance;
    [SerializeField] Dictionary<string, int> leaderboard;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        leaderboard = new Dictionary<string, int>();
        var allPlayer = FindObjectsOfType<GamePlayerInfo>();
        foreach (GamePlayerInfo p in allPlayer)
        {
            leaderboard.Add(p.name, 0);
        }
    }

    public void AddScore(string playerName, int scoreChanged)
    {
        if (leaderboard.ContainsKey(playerName))
        {
            leaderboard[playerName] += scoreChanged; 
        }
        Sort();
    }

    public void Sort()
    {
        var sortedLeaderboard = from entry in leaderboard orderby entry.Value descending select entry;
        leaderboard = sortedLeaderboard.ToDictionary(pair => pair.Key, pair => pair.Value);
    }

}

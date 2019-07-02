using System;
using UnityEngine;
using Discord;

public class DiscordManager : MonoBehaviour
{
    private const string CLIENT_ID = "585450365571432448";
    private Discord.Discord _discord;

    // CORE

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        InitializeDiscord();
        InitializeActivity();
    }

    private void Update()
    {
        _discord.RunCallbacks();
    }

    private void OnDestroy()
    {
        ClearActivity();
    }

    // PRIVATE

    private void InitializeDiscord()
    {
        _discord = new Discord.Discord(Int64.Parse(CLIENT_ID), (UInt64)CreateFlags.Default);
        Debug.Log("[DISCORD] Initialization done.");
    }

    private void InitializeActivity()
    {
        var activityManager = _discord.GetActivityManager();
        var activity = new Activity
        {
            State = "In a game.",
            Details = "An online Battle Kart Arena game !",
            Instance = true,
        };
        activityManager.UpdateActivity(activity, result =>
        {
            Debug.LogFormat("[DISCORD] Update Activity {0}", result);
        });
    }

    private void ClearActivity()
    {
        var activityManager = _discord.GetActivityManager();

        activityManager.ClearActivity(result =>
        {
            Debug.Log("[DISCORD] Cleared activity.");
        });

        _discord.Dispose();
    }
}

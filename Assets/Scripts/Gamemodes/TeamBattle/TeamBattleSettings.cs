using UnityEngine;

[CreateAssetMenu(menuName = "Multiplayer Settings/TeamBattle Settings")]
public class TeamBattleSettings : ScriptableObject
{
    public int LifeCountPerPlayers;
    public int JailMaxDuration;
}

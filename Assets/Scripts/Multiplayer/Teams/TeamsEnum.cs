using UnityEngine.Events;

public enum Team { None, Blue, Red }

[System.Serializable]
public class TeamEvent : UnityEvent<Team> { };

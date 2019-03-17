using UnityEngine.Events;

public enum Team { None, Blue, Red, Purple, Yellow, Green, Pink, Brown, Teal, Black, White }

[System.Serializable]
public class TeamEvent : UnityEvent<Team> { };

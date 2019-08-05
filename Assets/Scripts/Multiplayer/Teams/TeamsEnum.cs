using UnityEngine.Events;

public enum Team { None, Any, Blue, Red, Purple, Yellow, Green, Pink, Brown, Teal, Black, White }

[System.Serializable]
public class TeamEvent : UnityEvent<Team> { };

[System.Serializable]
public class TeamIntEvent : UnityEvent<Team, int> { };

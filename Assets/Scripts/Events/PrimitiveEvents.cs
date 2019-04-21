using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FloatEvent : UnityEvent<float> { }

[System.Serializable]
public class IntEvent : UnityEvent<int> { }

[System.Serializable]
public class StringEvent : UnityEvent<string> { }

[System.Serializable]
public class BoolEvent : UnityEvent<bool> { }

[System.Serializable]
public class DoubleIntEvent : UnityEvent<int, int> { }

[System.Serializable]
public class IntFloatEvent : UnityEvent<int> { }

[System.Serializable]
public class ColorEvent : UnityEvent<Color> { }

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

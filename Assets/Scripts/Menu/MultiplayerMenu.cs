using UnityEngine;
using UnityEngine.UI;

public class MultiplayerMenu : MonoBehaviour {

    [SerializeField] private Button createRoomButton;
    [SerializeField] private Button refreshRoomButton;

    // CORE

    private void Awake()
    {
        createRoomButton.onClick.AddListener(() => FindObjectOfType<StringInput>().GetStringInput("Create a new room", CreateRoom));
        refreshRoomButton.onClick.AddListener(RefreshRoom);
    }

    // PUBLIC

    // PRIVATE
    private void CreateRoom(string roomName)
    {
    }

    private void RefreshRoom()
    {
    }
}

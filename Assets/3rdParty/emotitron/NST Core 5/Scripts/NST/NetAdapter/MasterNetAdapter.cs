//Copyright 2018, Davin Carten, All rights reserved

using UnityEngine;
using emotitron.Compression;
using emotitron.Utilities.GUIUtilities;
using System.Collections.Generic;
using System;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using Photon.Realtime;

#if UNITY_EDITOR
using UnityEditor;
#endif


namespace emotitron.NST
{
	// ver 1
	/// <summary>
	/// The UNET version of this interface for the NSTMaster - unifying code to work with both UNET and Photon.
	/// </summary>
	[DisallowMultipleComponent]
	[AddComponentMenu("")]
	public class MasterNetAdapter : MonoBehaviourPunCallbacks //, INSTMasterAdapter
	{
		[HideInInspector]
		public static bool networkStarted;

		public static MasterNetAdapter single;
		public const string ADAPTER_NAME = "PUN";

		public const NetworkLibrary NET_LIB = NetworkLibrary.PUN;

		/// <summary>
		/// Attribute for getting the NET_LIB value, without throwing warnings about unreachable code.
		/// </summary>
		public static NetworkLibrary NetLib { get { return NET_LIB; } }

		public const NetworkModel NET_MODEL = NetworkModel.MasterClient;

		//private NSTMasterSettings nstMasterSettings;

		// TODO this likely needs an actual test
		public static int MasterClientId { get { return PhotonNetwork.MasterClient.ActorNumber; } }

		// Interfaced fields
		public NetworkLibrary NetLibrary { get { return NetworkLibrary.PUN; } }
		public static NetworkLibrary NetworkLibrary { get { return NetworkLibrary.PUN; } }

		public static bool Connected { get { return PhotonNetwork.IsConnected; } }
		public static bool ReadyToSend { get { return true; } } // PhotonNetwork.IsMasterClient || PhotonNetwork.isNonMasterClientInRoom; } }
		public static bool ServerIsActive { get { return true; } } //PhotonNetwork.IsMasterClient; } }
        public static bool ClientIsActive { get { return true; } } //PhotonNetwork.isNonMasterClientInRoom; } }
        public static bool NetworkIsActive { get { return PhotonNetwork.IsMasterClient || PhotonNetwork.NetworkClientState == Photon.Realtime.ClientState.Joined; } }
		/// <summary> Cached value for defaultAuthority since this is hotpath </summary>

		public const byte LowestMsgTypeId = 0;
		public const byte HighestMsgTypeId = 199;
		public const byte DefaultMsgTypeId = 190;

		private static bool isServerClient;

		public bool IsRegistered { get { return isRegistered; } set { isRegistered = value; } }

		#region Callback Interfaces

		[HideInInspector] public static List<Component> iNetEvents = new List<Component>();
		[HideInInspector] public static List<Component> iOnConnect = new List<Component>();
		[HideInInspector] public static List<Component> iOnStartServer = new List<Component>();
		[HideInInspector] public static List<Component> iOnStartClient = new List<Component>();
		[HideInInspector] public static List<Component> iOnStartLocalPlayer = new List<Component>();
		[HideInInspector] public static List<Component> iOnNetworkDestroy = new List<Component>();
		[HideInInspector] public static List<Component> iOnJoinRoom = new List<Component>();
		[HideInInspector] public static List<Component> iOnJoinRoomFailed = new List<Component>();

		public static void RegisterCallbackInterfaces(Component obj)
		{
			MasterNetCommon.RegisterCallbackInterfaces(obj);
		}

		public static void UnregisterCallbackInterfaces(Component obj)
		{
			MasterNetCommon.UnregisterCallbackInterfaces(obj);
		}

		#endregion

		// Statics
		private static short masterMsgTypeId;
		private static bool isRegistered;

#if UNITY_EDITOR

		/// <summary>
		/// Add a NetworkIdentity to the supplied NSTMaster gameobject. Sets localPlayerAuth to false (master isn't a player)
		/// </summary>
		/// <param name="go"></param>
		public static bool AddRequiredEntityComponentToMaster(GameObject go)
		{
			// PUN doesn't need a PhotonView on the master
			return false;
		}

		public static void PurgeLibSpecificComponents()
		{
			NetAdapterTools.PurgeTypeFromEverywhere<PhotonView>();
		}

		public static void AddNstEntityComponentsEverywhere()
		{
			NetAdapterTools.AddComponentsWhereverOtherComponentIsFound<NetworkSyncTransform, NSTNetAdapter, PhotonView>();
		}

		public static void AddLibrarySpecificEntityComponent(GameObject go)
		{
			if (!go.GetComponent<PhotonView>())
				go.AddComponent<PhotonView>();
		}


#endif

		static RaiseEventOptions optsOthers;
		static RaiseEventOptions optsSvr;
		private void Awake()
		{
			isServerClient = NetLibrarySettings.Single.defaultAuthority == DefaultAuthority.ServerAuthority;

			optsOthers = new RaiseEventOptions();
			optsOthers.Receivers = ReceiverGroup.Others;

			optsSvr = new RaiseEventOptions();
			optsSvr.Receivers = ReceiverGroup.MasterClient;
		}

		void OnEnable()
		{
			if (isRegistered)
				return;

			isRegistered = true;

			//PhotonNetwork.RemoveCallbackTarget(OnEventHandler);
			//PhotonNetwork.AddCallbackTarget(OnEventHandler);
		}

		private void OnDisable()
		{
			//PhotonNetwork.OnEventCall -= this.OnEventHandler;
			isRegistered = false;
		}


		public override void OnConnected()
		{
			//Debug.Log("OnConnectedToPhoton");
		}

		public override void OnConnectedToMaster()
		{
			foreach (INetEvents cb in iNetEvents)
				cb.OnConnect(ServerClient.Master);

			foreach (IOnConnect cb in iOnConnect)
				cb.OnConnect(ServerClient.Master);
		}

		public override void OnDisconnected(DisconnectCause cause)
		{
			networkStarted = false;

			if (iNetEvents != null)
				foreach (INetEvents cb in iNetEvents)
					cb.OnNetworkDestroy();

			if (iOnNetworkDestroy != null)
				foreach (IOnNetworkDestroy cb in iOnNetworkDestroy)
					cb.OnNetworkDestroy();
		}
        /*
		public override void OnConnectionFail(DisconnectCause cause)
		{
			base.OnConnectionFail(cause);
			DebugX.LogWarning("Failed to connect. " + cause);
		}

		public override void OnFailedToConnectToPhoton(DisconnectCause cause)
		{
			base.OnFailedToConnectToPhoton(cause);
			DebugX.LogWarning("Failed to connect to Photon. " + cause);
		}
        */
		public override void OnJoinedRoom()
		{
			foreach (IOnJoinRoom cb in iOnJoinRoom)
				cb.OnJoinRoom();
		}

		public override void OnJoinRoomFailed(short returnCode, string message)
        {
			foreach (IOnJoinRoomFailed cb in iOnJoinRoomFailed)
				cb.OnJoinRoomFailed();
		}

		/// <summary>
		/// Capture incoming Photon messages here. If it is the one we are interested in - pass it to NSTMaster
		/// </summary>
		private void OnEventHandler(byte eventCode, object content, int senderId)
		{
			if (eventCode != DefaultMsgTypeId)
				return;

			// ignore messages from self.
			if (ServerIsActive && PhotonNetwork.MasterClient.ActorNumber == senderId)
			{
				DebugX.Log("Master Client talking to self? Normal occurance for a few seconds after Master leaves the game and a new master is selected.");
				return;
			}

			UdpBitStream bitstream = new UdpBitStream(content as byte[]);
			UdpBitStream outstream = new UdpBitStream(NSTMaster.outstreamByteArray);

			bool mirror = PhotonNetwork.IsMasterClient && NetLibrarySettings.single.defaultAuthority == DefaultAuthority.ServerAuthority;

			NSTMaster.ReceiveUpdate(ref bitstream, ref outstream, mirror, senderId);

			if (mirror)// authorityModel == DefaultAuthority.ServerClient)
			{
				byte[] outbytes = new byte[outstream.BytesUsed];
				Array.Copy(outstream.Data, outbytes, outbytes.Length);
				//PhotonNetwork.NetworkingClient.OpRaiseEvent(DefaultMsgTypeId, outbytes, false, optsOthers);
				PhotonNetwork.NetworkingClient.Service();
			}
		}

		public static void SendUpdate(ref UdpBitStream bitstream, ref UdpBitStream outstream)
		{
			//TODO replace this GC generating mess with something prealloc
			byte[] streambytes = new byte[bitstream.BytesUsed];
			Array.Copy(bitstream.Data, streambytes, streambytes.Length);
			//PhotonNetwork.NetworkingClient.OpRaiseEvent(DefaultMsgTypeId, streambytes, false, (isServerClient && !PhotonNetwork.IsMasterClient) ? optsSvr : optsOthers);
			PhotonNetwork.NetworkingClient.Service();

			// MasterClient send to self - may are may not need this in the future.
			if (PhotonNetwork.IsMasterClient)
				NSTMaster.ReceiveUpdate(ref bitstream, ref outstream, false, PhotonNetwork.MasterClient.ActorNumber);
		}

		#region UNET Specific methods

		public static Transform UNET_GetPlayerSpawnPoint(){	return null;}
		public static void UNET_RegisterStartPosition(Transform tr){}
		public static void UNET_UnRegisterStartPosition(Transform tr){}
		public static GameObject UNET_GetRegisteredPlayerPrefab(){ return null; }

		#endregion

		#region PUN Specific methods


		public static bool PUN_AutomaticallySyncScene
		{
			get { return PhotonNetwork.AutomaticallySyncScene; }
			set { PhotonNetwork.AutomaticallySyncScene = value; }

		}
		public static bool PUN_Connected
		{
			get { return PhotonNetwork.IsConnected; }
		}

        public static bool PUN_AutoJoinLobby { get; internal set; }

        public static void PUN_ConnectUsingSettings(string gameversion)
		{
			PhotonNetwork.ConnectUsingSettings();
		}

		public static void PUN_JoinRandomRoom()
		{
			PhotonNetwork.JoinRandomRoom();
		}

		public static void PUN_LoadLevel(string scenename)
		{
			PhotonNetwork.LoadLevel(scenename);
		}

		public static void PUN_CreateRoom(string roomname, byte maxPlayers)
		{
			PhotonNetwork.CreateRoom(roomname, new RoomOptions() { MaxPlayers = maxPlayers }, null);
		}

		#endregion

		public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
		{
			GameObject go = PhotonNetwork.Instantiate(prefab.name, position, rotation, 0);
			go.transform.parent = parent;
			return go;
		}
	}

#if UNITY_EDITOR

	[CustomEditor(typeof(MasterNetAdapter))]
	[CanEditMultipleObjects]
	public class MasterNetAdapterPEditor : NSTHeaderEditorBase
	{
		public override void OnEnable()
		{
			headerColor = HeaderSettingsColor;
			headerName = HeaderMasterName;
			base.OnEnable();
		}

		public override void OnInspectorGUI()
		{
			serializedObject.Update();

			base.OnInspectorGUI();
			EditorGUILayout.HelpBox("This is the Adapter for Photon. To work with UNET, switch the Network Library.", MessageType.None);
		}
	}

#endif
}

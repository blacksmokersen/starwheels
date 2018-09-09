using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace MyExtensions
{
    #region Math
    public class Math
    {
        public static float RemapValue(float actualMin, float actualMax, float targetMin, float targetMax, float val)
        {
            return targetMin + (targetMax - targetMin) * ((val - actualMin) / (actualMax - actualMin));
        }
    }
    #endregion

    #region TeamExtensions

    public class TeamUtilities
    {
        public static List<Player> GetTeammates()
        {
            var teammates = new List<Player>();
            foreach (Player player in PhotonNetwork.PlayerListOthers)
            {
                if (player.GetTeam() == PhotonNetwork.LocalPlayer.GetTeam())
                {
                    teammates.Add(player);
                }
            }
            return teammates;
        }

        public static Player GetNextTeammate(Player currentTeammate)
        {
            var teammates = GetTeammates();
            for (int i = 0; i < teammates.Count; i++)
            {
                if (teammates[i] == currentTeammate)
                {
                    return teammates[(i + 1) % teammates.Count];
                }
            }
            return null;
        }

        public static Player PickRandomTeammate()
        {
            var teammates = GetTeammates();
            var rand = Random.Range(0, teammates.Count - 1);
            return teammates[rand];
        }
    }

        #endregion

    #region KartExtensions
    public class Kart
    {
        public static List<GameObject> GetTeamKarts()
        {
            var teamKarts = new List<GameObject>();
            var allKarts = GameObject.FindGameObjectsWithTag(Constants.Tag.Kart);
            foreach (GameObject kart in allKarts)
            {
                var kartPlayer = kart.GetComponent<PhotonView>().Owner;
                if (kartPlayer.GetTeam() == PhotonNetwork.LocalPlayer.GetTeam() && kartPlayer != PhotonNetwork.LocalPlayer)
                {
                    teamKarts.Add(kart);
                }
            }
            return teamKarts;
        }

        public static GameObject GetNextTeamKart(GameObject currentTeamKart)
        {
            var teamKarts = GetTeamKarts();
            for (int i = 0; i < teamKarts.Count; i++)
            {
                if (teamKarts[i] == currentTeamKart)
                {
                    return teamKarts[(i + 1) % teamKarts.Count];
                }
            }
            return null;
        }

        public static GameObject PickRandomTeamKart()
        {
            var teamKart = GetTeamKarts();
            var rand = Random.Range(0, teamKart.Count - 1);
            return teamKart[rand];
        }
    }
    #endregion

    #region AudioExtensions
    public class Audio
    {
        public static void PlayClipObjectAndDestroy(AudioSource audioSource)
        {
            GameObject oneShotObject = new GameObject("One shot sound from " + audioSource.name);
            oneShotObject.transform.position = audioSource.transform.position;
            UnityEditorInternal.ComponentUtility.CopyComponent(audioSource);
            UnityEditorInternal.ComponentUtility.PasteComponentAsNew(oneShotObject);
            var oneShotSource = oneShotObject.GetComponent<AudioSource>();
            oneShotSource.Play();
            MonoBehaviour.Destroy(oneShotObject, oneShotSource.clip.length);
        }
    }
    #endregion
}

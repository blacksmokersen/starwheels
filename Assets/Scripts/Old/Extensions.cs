using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System;
using System.Reflection;

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
            var rand = UnityEngine.Random.Range(0, teammates.Count - 1);
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
            if (teamKart.Count > 0)
            {
                var rand = UnityEngine.Random.Range(0, teamKart.Count - 1);
                return teamKart[rand];
            }
            return null;
        }
    }
    #endregion

    #region Components
    public static class Components
    {
        public static T CopyComponent<T>(T original, GameObject destination) where T : Component
        {
            System.Type type = original.GetType();
            var dst = destination.GetComponent(type) as T;
            if (!dst) dst = destination.AddComponent(type) as T;
            var fields = type.GetFields();
            foreach (var field in fields)
            {
                if (field.IsStatic) continue;
                field.SetValue(dst, field.GetValue(original));
            }
            var props = type.GetProperties();
            foreach (var prop in props)
            {
                if (!prop.CanWrite || !prop.CanWrite || prop.Name == "name") continue;
                prop.SetValue(dst, prop.GetValue(original, null), null);
            }
            return dst as T;
        }

        public static void CopyAndPasteAudioSource(AudioSource original, GameObject destination)
        {
            destination.AddComponent<AudioSource>();
            AudioSource audioSource = destination.GetComponent<AudioSource>();
            audioSource.bypassEffects = original.bypassEffects;
            audioSource.bypassListenerEffects = original.bypassListenerEffects;
            audioSource.bypassReverbZones = original.bypassReverbZones;
            audioSource.clip = original.clip;
            audioSource.dopplerLevel = original.dopplerLevel;
            audioSource.ignoreListenerPause = original.ignoreListenerPause;
            audioSource.ignoreListenerVolume = original.ignoreListenerVolume;
            audioSource.loop = original.loop;
            audioSource.maxDistance = original.maxDistance;
            audioSource.minDistance = original.minDistance;
            audioSource.mute = original.mute;
            audioSource.outputAudioMixerGroup = original.outputAudioMixerGroup;
            audioSource.playOnAwake = original.playOnAwake;
            audioSource.panStereo = original.panStereo;
            audioSource.priority = original.priority;
            audioSource.pitch = original.pitch;
            audioSource.rolloffMode = original.rolloffMode;
            audioSource.reverbZoneMix = original.reverbZoneMix;
            audioSource.spatialBlend = original.spatialBlend;
            audioSource.spatialize = original.spatialize;
            audioSource.spatializePostEffects = original.spatializePostEffects;
            audioSource.spread = original.spread;
            audioSource.time = original.time;
            audioSource.timeSamples = original.timeSamples;
            audioSource.velocityUpdateMode = original.velocityUpdateMode;
            audioSource.volume = original.volume;
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
            Components.CopyAndPasteAudioSource(audioSource, oneShotObject);
            var oneShotSource = oneShotObject.GetComponent<AudioSource>();
            oneShotSource.Play();
            MonoBehaviour.Destroy(oneShotObject, oneShotSource.clip.length);
        }
    }
    #endregion
}

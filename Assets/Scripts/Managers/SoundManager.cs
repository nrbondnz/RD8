using System;
using System.Collections.Generic;
using Collision;
using UnityEngine;

namespace Managers
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        [Header("Sounds")] [SerializeField] private AudioSource audioSource;

        private static Dictionary<CollisionActionEnum,
            Dictionary<CollisionEffectStrengthEnum, AudioClip>> _collisionSounds =
            new Dictionary<CollisionActionEnum,
                Dictionary<CollisionEffectStrengthEnum, AudioClip>>();

        private void Awake()
        {
            if (_instance != null)
            {
                Debug.Log("SoundManager Trying second Awake");
                Destroy(gameObject);
                return;
            }

            Debug.Log("SoundManager Awake");
            _instance = this as SoundManager;
            DontDestroyOnLoad(gameObject);
            SetupResources();
        }

        private static void SetupResources()
        {
            AudioClip[] collAudioArrayList = Resources.LoadAll<AudioClip>("Sounds/Collision");
            Debug.Log(collAudioArrayList);
            Dictionary<CollisionActionEnum, AudioClip> defaultClips =
                new Dictionary<CollisionActionEnum, AudioClip>();
            // find defaults
            foreach (AudioClip audioClip in collAudioArrayList)
            {
                var audioClipName = audioClip.name;
                String actionString = audioClipName.Substring(0, audioClipName.IndexOf("-"));
                String audioClipAction = audioClip.name.Substring(0, audioClip.name.IndexOf("-"));
                var effectStrength = audioClipName.Substring(audioClipName.IndexOf("-") + 1);
                CollisionActionEnum actionEnum;
                Enum.TryParse<CollisionActionEnum>(actionString, out actionEnum);
                if (effectStrength.Equals("Default"))
                {
                    defaultClips[actionEnum] = audioClip;
                }
            }

            // setup collisionSounds
            foreach (CollisionActionEnum actionVals in Enum.GetValues(
                         (typeof(CollisionActionEnum))))
            {
                _collisionSounds[actionVals] = new Dictionary<CollisionEffectStrengthEnum, AudioClip>();
            }

            // loop over sound list from directory and put them in the dictionary called collisionSounds
            foreach (AudioClip audioClip in collAudioArrayList)
            {
                var audioClipName = audioClip.name;
                Debug.Log(audioClipName);
                String actionString = audioClipName.Substring(0, audioClipName.IndexOf("-"));
                Debug.Log(actionString);
                var effectStrength = audioClipName.Substring(audioClipName.IndexOf("-") + 1);
                Debug.Log(effectStrength);
                CollisionActionEnum actionEnum;
                Enum.TryParse<CollisionActionEnum>(actionString, out actionEnum);
                CollisionEffectStrengthEnum effectStrengthEnum;
                Enum.TryParse<CollisionEffectStrengthEnum>(effectStrength, out effectStrengthEnum);
                if ((actionEnum.GetHashCode() > -1))
                {
                    Debug.Log(actionEnum.ToString() + "." + effectStrengthEnum.ToString());
                    var collisionSoundsActionGroup = _collisionSounds[actionEnum];
                    if (effectStrengthEnum.GetHashCode() > 0)
                    {
                        collisionSoundsActionGroup[effectStrengthEnum] = audioClip;
                    }
                }
            }

            foreach (CollisionActionEnum actionEnumVal in Enum.GetValues(
                         typeof(CollisionActionEnum)))
            {
                AudioClip defaultActionAudioClip = defaultClips[actionEnumVal];
                var collisionSoundsActionGroup = _collisionSounds[actionEnumVal];
                foreach (CollisionEffectStrengthEnum effectVal in Enum.GetValues(
                             typeof(CollisionEffectStrengthEnum)))
                {
                    if (!collisionSoundsActionGroup.ContainsKey(effectVal))
                    {
                        // needs default
                        collisionSoundsActionGroup[effectVal] = defaultActionAudioClip;
                    }
                }
            }
        }

        public static SoundManager GetInstance()
        {
            return _instance;
        }

        public void PlayCollisionSound(CollisionActionController collisionActionController)
        {
            Dictionary<CollisionEffectStrengthEnum, AudioClip> actionGroup =
                _collisionSounds[collisionActionController.GetCollisionActionEnum()];
            AudioClip collisionSound = actionGroup[collisionActionController.GetCollisionEffectStrengthEnum()];
            audioSource.PlayOneShot(collisionSound);
        }

    }
}

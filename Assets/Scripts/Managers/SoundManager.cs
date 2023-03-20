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
            Dictionary<CollisionEffectStrengthEnum, AudioClip>> _collisionSounds;

        /// <summary>
        /// Initializes the singleton SoundManager
        /// This includes a call to SetupResources to load all the sounds
        /// </summary>
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

        /// <summary>
        /// Uses the Resources class to load all the audio clips from their location, Sounds/Collision
        /// </summary>
        private static void SetupResources()
        {
            /// Loads all of the audio clips for processing
            AudioClip[] collAudioArrayList = Resources.LoadAll<AudioClip>("Sounds/Collision");
            Debug.Log(collAudioArrayList);
            /// Creates a distionary to hold a set of default audio clips, one for each CollisionActionEnum entry
            Dictionary<CollisionActionEnum, AudioClip> defaultClips =
                new Dictionary<CollisionActionEnum, AudioClip>();
            /// find defaults amongst all of the loaded audio clips
            foreach (AudioClip audioClip in collAudioArrayList)
            {
                var audioClipName = audioClip.name;
                /// Parse the audio clips to find the required regex of action-effectstrength
                String actionString = audioClipName.Substring(0, audioClipName.IndexOf("-"));
                var effectStrength = audioClipName.Substring(audioClipName.IndexOf("-") + 1);
                CollisionActionEnum actionEnum;
                /// try to parse the action enum from the action part of the regex
                Enum.TryParse<CollisionActionEnum>(actionString, out actionEnum);
                /// If the effect strength is the 'Default'
                if (effectStrength.Equals("Default"))
                {
                    /// then set the dictionary element for the actionEnum to the audio clip for that entry
                    defaultClips[actionEnum] = audioClip;
                }
            }

            /// setup _collisionSounds child dictionary for each action value by adding
            /// a child dictionary for each with a key of effect strength
            foreach (CollisionActionEnum actionVals in Enum.GetValues(
                         (typeof(CollisionActionEnum))))
            {
                _collisionSounds[actionVals] = new Dictionary<CollisionEffectStrengthEnum, AudioClip>();
            }

            /// loop over loaded sound list and use REgex to work out action and effect strength
            /// Then load into the 2 dimentional dictionary in the correct position
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

            /// loop over the loaded sounds and find gaps, where there are gaps use the action types default sound
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

        /// <summary>
        /// Gets the singleton instance of the SoundManager
        /// </summary>
        /// <returns>SoundManager</returns>
        public static SoundManager GetInstance()
        {
            return _instance;
        }

        /// <summary>
        /// When calls from the collision action controller it plays the sound for the specified
        /// 2 dimensional sound dictionary setting
        /// </summary>
        /// <param name="collisionActionController"></param>
        public void PlayCollisionSound(CollisionActionController collisionActionController)
        {
            Dictionary<CollisionEffectStrengthEnum, AudioClip> actionGroup =
                _collisionSounds[collisionActionController.GetCollisionActionEnum()];
            AudioClip collisionSound = actionGroup[collisionActionController.GetCollisionEffectStrengthEnum()];
            audioSource.PlayOneShot(collisionSound);
        }

    }
}

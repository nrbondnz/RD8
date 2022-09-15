using System;
using System.Collections.Generic;
using Collision;
using UnityEngine;

namespace DefaultNamespace
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;
        [Header("Sounds")]
        [SerializeField] private AudioSource audioSource;
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
        }

        public static SoundManager GetInstance()
        {
            return _instance;
        }

        private Dictionary<ICollisionAction.CollisionActionEnum,
            Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip>> collisionSounds =
            new Dictionary<ICollisionAction.CollisionActionEnum,
                Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip>>();
        

        private AudioClip[] _collisionAudioClip;
        private Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip> bounceClips;

        private void Start()
        {

            AudioClip[] collAudioArrayList = Resources.LoadAll<AudioClip>("Sounds/Collision");
            Debug.Log(collAudioArrayList);
            Dictionary<ICollisionAction.CollisionActionEnum, AudioClip> defaultClips =
                new Dictionary<ICollisionAction.CollisionActionEnum, AudioClip>();
            // find defaults
            foreach (AudioClip audioClip in collAudioArrayList)
            {
                var audioClipName = audioClip.name;
                String actionString = audioClipName.Substring(0, audioClipName.IndexOf("-"));
                String audioClipAction = audioClip.name.Substring(0, audioClip.name.IndexOf("-"));
                var effectStrength = audioClipName.Substring(audioClipName.IndexOf("-") + 1);
                ICollisionAction.CollisionActionEnum actionEnum;
                Enum.TryParse<ICollisionAction.CollisionActionEnum>(actionString, out actionEnum);
                if (effectStrength.Equals("Default"))
                {
                    defaultClips[actionEnum] = audioClip;
                }
            }
            
            // setup collisionSounds
            foreach (ICollisionAction.CollisionActionEnum actionVals in Enum.GetValues((typeof(ICollisionAction.CollisionActionEnum))))
            {
                collisionSounds[actionVals] = new Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip>();
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
                ICollisionAction.CollisionActionEnum actionEnum;
                Enum.TryParse<ICollisionAction.CollisionActionEnum>(actionString, out actionEnum);
                ICollisionAction.CollisionEffectStrengthEnum effectStrengthEnum;
                Enum.TryParse<ICollisionAction.CollisionEffectStrengthEnum>(effectStrength, out effectStrengthEnum);
                if ((actionEnum.GetHashCode() > -1))
                {
                    Debug.Log(actionEnum.ToString() + "." + effectStrengthEnum.ToString());
                    var collisionSoundsActionGroup = collisionSounds[actionEnum];
                    if (effectStrengthEnum.GetHashCode() > 0)
                    {
                        collisionSoundsActionGroup[effectStrengthEnum] = audioClip;
                    }

                }
            }

            foreach (ICollisionAction.CollisionActionEnum actionEnumVal in Enum.GetValues(
                         typeof(ICollisionAction.CollisionActionEnum)))
            {
                AudioClip defaultActionAudioClip = defaultClips[actionEnumVal];
                var collisionSoundsActionGroup = collisionSounds[actionEnumVal];
                foreach (ICollisionAction.CollisionEffectStrengthEnum effectVal in Enum.GetValues(
                             typeof(ICollisionAction.CollisionEffectStrengthEnum)))
                {
                    if (!collisionSoundsActionGroup.ContainsKey(effectVal))
                    {
                        // needs default
                        collisionSoundsActionGroup[effectVal] = defaultActionAudioClip;
                    }
                }
            }

        }

        public void PlayCollisionSound(CollisionActionController collisionActionController)
        {
            Dictionary<ICollisionAction.CollisionEffectStrengthEnum,AudioClip> actionGroup = this.collisionSounds[collisionActionController.GetCollisionActionEnum()];
            AudioClip collisionSound = actionGroup[collisionActionController.GetCollisionEffectStrengthEnum()];
            audioSource.PlayOneShot(collisionSound);
        }

    }
}
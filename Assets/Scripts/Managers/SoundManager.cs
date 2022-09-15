using System;
using System.Collections.Generic;
using Collision;
using UnityEngine;

namespace DefaultNamespace
{
    public class SoundManager : MonoBehaviour
    {
        private static SoundManager _instance;

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

        [Header("Sounds")]
        [SerializeField] private AudioSource audioSource;
        [Header("Collision Sounds")] 
        [SerializeField] public AudioClip bounce_Full_AudioClip;
        [SerializeField] public AudioClip bounce_Strong_AudioClip;
        [SerializeField] public AudioClip bounce_MedStrong_AudioClip;
        [SerializeField] public AudioClip bounce_Normal_AudioClip;
        [SerializeField] public AudioClip bounce_Half_AudioClip;
        [SerializeField] public AudioClip bounce_Low_AudioClip;
        [SerializeField] public AudioClip bounce_Lowest_AudioClip;

        [SerializeField] public AudioClip deathAudioClip;

        [SerializeField] public AudioClip attractAudioClip;

        [SerializeField] public AudioClip speedChangeAudioClip;

        private Dictionary<ICollisionAction.CollisionActionEnum,
            Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip>> collisionSounds 
            = new Dictionary<ICollisionAction.CollisionActionEnum, 
            Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip>>();
        

        private AudioClip[] _collisionAudioClip;
        private Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip> bounceClips;

        private void Start()
        {
            /*_collisionAudioClip = new AudioClip[4];
            //_collisionAudioClip[0] = bounceAudioClip;
            _collisionAudioClip[1] = speedChangeAudioClip;
            _collisionAudioClip[2] = deathAudioClip;
            _collisionAudioClip[3] = attractAudioClip;
            bounceClips =
                new Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip>();
            bounceClips[ICollisionAction.CollisionEffectStrengthEnum.Full] = bounce_Full_AudioClip;
            bounceClips[ICollisionAction.CollisionEffectStrengthEnum.Strong] = bounce_Strong_AudioClip;
            bounceClips[ICollisionAction.CollisionEffectStrengthEnum.MedStrong] = bounce_MedStrong_AudioClip;
            bounceClips[ICollisionAction.CollisionEffectStrengthEnum.Normal] = bounce_Normal_AudioClip;
            bounceClips[ICollisionAction.CollisionEffectStrengthEnum.Half] = bounce_Half_AudioClip;
            bounceClips[ICollisionAction.CollisionEffectStrengthEnum.Low] = bounce_Low_AudioClip;
            bounceClips[ICollisionAction.CollisionEffectStrengthEnum.Lowest] = bounce_Lowest_AudioClip;
            collisionSounds[ICollisionAction.CollisionActionEnum.Bounce] = bounceClips;*/

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

            // loop over sound list and put them in the dictionary called collisionSounds
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


            /*foreach (var actionString in Enum.GetNames(typeof(ICollisionAction.CollisionActionEnum)))
            {
                ICollisionAction.CollisionActionEnum actionEnum;
                Enum.TryParse<ICollisionAction.CollisionActionEnum>(actionString, out actionEnum);
                var audioClipActions = collisionSounds[actionEnum];
                foreach (var effectStrength in Enum.GetNames(typeof(ICollisionAction.CollisionEffectStrengthEnum)))
                {
                    ICollisionAction.CollisionEffectStrengthEnum effectStrengthEnum;
                    Enum.TryParse<ICollisionAction.CollisionEffectStrengthEnum>(effectStrength, out effectStrengthEnum);
                    if (!audioClipActions.ContainsKey(effectStrengthEnum) )
                    {
                        audioClipActions[effectStrengthEnum] = defaultClips[actionEnum];
                    }

                    var collisionSoundArray = collisionSounds[actionEnum];
                    Debug.Log(
                        actionString + " " + effectStrength + " = " + collisionSoundArray[effectStrengthEnum].name);
                }
            }*/
            
        }

        public void PlayCollisionSound(CollisionActionController collisionActionController)
        {
            AudioClip collisionSound;
            if ( ICollisionAction.CollisionActionEnum.Bounce.Equals(collisionActionController.GetCollisionActionEnum() ) )
            {
                collisionSound = bounceClips[collisionActionController.GetCollisionEffectStrengthEnum()];
            } else {
                collisionSound = _collisionAudioClip[collisionActionController.GetCollisionActionID()];
            }
            //AudioClip collisionSound = collisionAudioClip[0];
            audioSource.PlayOneShot(collisionSound);
        }

    }
}
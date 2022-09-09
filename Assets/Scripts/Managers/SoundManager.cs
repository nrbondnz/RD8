using System;
using System.Collections.Generic;
using Collision;
using UnityEngine;

namespace DefaultNamespace
{
    public class SoundManager : MBSingleton<SoundManager>
    {
        private static SoundManager _instance;
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
            = new Dictionary<ICollisionAction.CollisionActionEnum, Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip>>();
        

        private AudioClip[] _collisionAudioClip;
        private Dictionary<ICollisionAction.CollisionEffectStrengthEnum, AudioClip> bounceClips;

        private void Start()
        {
            _collisionAudioClip = new AudioClip[4];
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
            collisionSounds[ICollisionAction.CollisionActionEnum.Bounce] = bounceClips;
                ;
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
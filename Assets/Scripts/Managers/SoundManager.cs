using System;
using Collision;
using UnityEngine;

namespace DefaultNamespace
{
    public class SoundManager : MBSingleton<SoundManager>
    {
        private static SoundManager _instance;
        [Header("Sounds")]
        [SerializeField] private AudioSource _audioSource;
        [Header("Collision Sounds")] 
        [SerializeField] public AudioClip bounceAudioClip;

        [SerializeField] public AudioClip deathAudioClip;

        [SerializeField] public AudioClip attractAudioClip;

        [SerializeField] public AudioClip speedChangeAudioClip;

        
        private AudioClip[] collisionAudioClip;

        private void Start()
        {
            collisionAudioClip = new AudioClip[4];
            collisionAudioClip[0] = bounceAudioClip;
            collisionAudioClip[1] = speedChangeAudioClip;
            collisionAudioClip[2] = deathAudioClip;
            collisionAudioClip[3] = attractAudioClip;
        }

        public void PlayCollisionSound(CollisionActionController collisionActionController)
        {
            AudioClip collisionSound = collisionAudioClip[collisionActionController.GetCollisionActionID()];
            //AudioClip collisionSound = collisionAudioClip[0];
            _audioSource.PlayOneShot(collisionSound);
        }

    }
}
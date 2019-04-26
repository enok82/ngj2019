using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

    public class SoundAnimationHandler : MonoBehaviour
    {
        
        
        public void AudioEvent(string eventRef)
        {
            string audioEvent = eventRef as string; 
            
            if (audioEvent != null)
            {
                RuntimeManager.PlayOneShot(audioEvent, gameObject.transform.position);

            }
            else
            {
                Debug.LogError("No Event Sound at " + gameObject);
            }

        }
    
    }




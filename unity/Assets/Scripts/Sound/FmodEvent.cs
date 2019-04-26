
using FMODUnity;
using FMOD.Studio;
using UnityEngine;

public static class FmodEvent
{
     public static EventInstance PlayOneShotAtPosition (string eventName, Transform position) 
            {
                EventInstance instance = RuntimeManager.CreateInstance(eventName);
             
                if (string.IsNullOrEmpty(eventName))
                    return instance;
        

               FMOD.ATTRIBUTES_3D positionAttribute = RuntimeUtils.To3DAttributes(position);
               instance.set3DAttributes(positionAttribute);
        
               instance.start();
               instance.release();
               return instance;
               
               
            }	
    
    
    public static EventInstance PlayOneShotAtPosition (string eventName, Transform position,PARAMETER_ID parameterId, float value)
    {
       
        EventInstance instance = RuntimeManager.CreateInstance(eventName);
        
        if (string.IsNullOrEmpty(eventName))
            return instance;

       instance.setParameterByID(parameterId, value);
       
       FMOD.ATTRIBUTES_3D positionAttribute = RuntimeUtils.To3DAttributes(position);
       instance.set3DAttributes(positionAttribute);

       instance.start();
       instance.release();
       return instance;
    }	
    
    
    public static EventInstance PlayOneShot (string eventName, Transform position, Rigidbody rb) 
    {
        EventInstance instance = RuntimeManager.CreateInstance(eventName);
             
        if (string.IsNullOrEmpty(eventName))
            return instance;
        
        RuntimeManager.AttachInstanceToGameObject(instance,position,rb);
        
        instance.start();
        instance.release();
        return instance;
    }	
    
    public static EventInstance PlayOneShot (string eventName, Transform position, Rigidbody rb, PARAMETER_ID parameterId, float value)
    {
      
        EventInstance instance = RuntimeManager.CreateInstance(eventName);
        
        if (string.IsNullOrEmpty(eventName))
            return instance;

        instance.setParameterByID(parameterId, value);
        RuntimeManager.AttachInstanceToGameObject(instance, position,rb);
       
        instance.start();
        instance.release();
        return instance;
    }	
    
    
    public static EventInstance Play (string eventName, Transform position, Rigidbody rb) 
    {
        EventInstance instance = RuntimeManager.CreateInstance(eventName);
             
        if (string.IsNullOrEmpty(eventName))
            return instance;
        

        RuntimeManager.AttachInstanceToGameObject(instance,position,rb);

        instance.start();
        return instance;
    }	
   
    public static EventInstance Stop (EventInstance instance, FMOD.Studio.STOP_MODE stopMode) 
    {

        if (instance.isValid())
        {
            instance.stop(stopMode);
            instance.release();          
        }
        else
        {
            Debug.LogError("No Instance was found!");
        }

        return instance;
    }	
    
    
    public static EventInstance Pause (EventInstance instance) 
    {

        if (instance.isValid())
        {
            bool isPaused;
            instance.getPaused(out isPaused);
            if (isPaused)
                return instance;
         
                
           instance.setPaused(true);
        }
        else
        {
            Debug.LogError("No Instance was found!");
        }

        return instance;
    }
    
    public static EventInstance UnPause (EventInstance instance) 
    {

        if (instance.isValid())
        {
            bool isPaused;
            instance.getPaused(out isPaused);
            if (!isPaused)
                return instance;
         
            instance.setPaused(false);
        }
        else
        {
            Debug.LogError("No Instance was found!");
        }

        return instance;
        
    }

    public static PARAMETER_ID GetParameterId(string eventName, string parameterName)
    {

        EventDescription eventDescription = RuntimeManager.GetEventDescription(eventName);

        PARAMETER_DESCRIPTION parameterDescription;
        

        eventDescription.getParameterDescriptionByName(parameterName, out parameterDescription);

        PARAMETER_ID parameterId = parameterDescription.id;

        return parameterId;

    }
    
    
}

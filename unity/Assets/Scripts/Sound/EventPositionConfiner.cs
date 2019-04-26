
 using UnityEngine;
using System.Collections;
using FMODUnity;
using FMOD.Studio;
 using UnityEngine.Serialization;


 [RequireComponent(typeof(Collider))]
public class EventPositionConfiner : MonoBehaviour
{

    [EventRef]
    public string eventToPlay;
    
    [Header("Settings")]
    public float updateInterval = 0.05f;

    #region private variables
    private IEnumerator positionClamperRoutine;

    private Collider m_trigger;
    private Transform m_targetTransform;

    private GameObject m_eventEmitter;

    private EventInstance _instance;

    private StudioListener listenerGameObject;
    #endregion

    private void Awake()
    {
        m_trigger = GetComponent<Collider>();
        m_trigger.isTrigger = true;

        m_eventEmitter = new GameObject("Clamped Emitter");
        m_eventEmitter.transform.parent = transform;
        Rigidbody rb = m_eventEmitter.AddComponent<Rigidbody>();
        rb.isKinematic = true;
        SphereCollider SPC = m_eventEmitter.AddComponent<SphereCollider>();
        SPC.isTrigger = true;
        //_eventEmitter.AddComponent<StudioEventEmitter>();
    }

    private void OnEnable()
    {
        listenerGameObject = FindObjectOfType<StudioListener>();
        

        if (listenerGameObject != null)
        {
            m_targetTransform = listenerGameObject.transform;
        }
        else
        {
            Debug.LogError(this + ": No GameObject with 'Studio Listener' Component found! Aborting.");
            enabled = false;
        }

        _instance = FmodEvent.Play(eventToPlay, m_eventEmitter.transform,null);
        
        positionClamperRoutine = ClampEmitterPosition();
        StartCoroutine(positionClamperRoutine);
    }

    private void OnDisable()
    {

     
        FmodEvent.Stop(_instance,FMOD.Studio.STOP_MODE.ALLOWFADEOUT);

        if(positionClamperRoutine != null)
        {
            StopCoroutine(positionClamperRoutine);
        }
    }

    IEnumerator ClampEmitterPosition()
    {
        while (true)
        {        
           Vector3 closestPoint = m_trigger.ClosestPoint(m_targetTransform.position);

           m_eventEmitter.transform.position = closestPoint;
          
            yield return new WaitForSecondsRealtime(updateInterval);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        if (m_eventEmitter != null)
        {
            Gizmos.DrawSphere(m_eventEmitter.transform.position, 0.2f);
        }
    }

  
}

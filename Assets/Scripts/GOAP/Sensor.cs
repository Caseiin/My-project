using System;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Sensor : MonoBehaviour{
    [SerializeField] Color defaultColour = Color.green;
    [SerializeField] Color changedColour= Color.red;

    [SerializeField] float sensorRadius = 5f;
    [SerializeField] float timerInterval = 1f;

    public float Radius => sensorRadius;
    Timer timer;
    SphereCollider dectectionRange;
    public event Action OnTargetChanged = delegate {};
    GameObject target;
    Vector3 lastKnownPosition;

    public Vector3 TargetPosition => target? target.transform.position: Vector3.zero;
    public bool IsTargetInRange => target != null;
    
    void Awake()
    {
        dectectionRange = GetComponent<SphereCollider>();
        dectectionRange.isTrigger = true;
        dectectionRange.radius = sensorRadius;
    }

    void Start()
    {
        timer = new CountdownTimer(timerInterval);
        timer.OnTimerStop += ()=> {
            UpdateTargetPosition(target);
            timer.Start();
        };

        timer.Start();
    }

    void Update()
    {
        timer.Tick(Time.deltaTime);
    }

    void UpdateTargetPosition(GameObject target = null){
        this.target = target;
        if (IsTargetInRange && (lastKnownPosition != TargetPosition || lastKnownPosition != Vector3.zero)){
            lastKnownPosition = TargetPosition;
            OnTargetChanged.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        UpdateTargetPosition();
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        UpdateTargetPosition(other.gameObject);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = IsTargetInRange? changedColour : defaultColour;
        Gizmos.DrawWireSphere(transform.position, sensorRadius);
    }
}

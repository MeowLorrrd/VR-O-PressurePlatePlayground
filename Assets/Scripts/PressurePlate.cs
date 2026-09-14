using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Fires an event while something heavy enough rests on the plate.
/// </summary>
public class PressurePlate : MonoBehaviour
{
    public float requiredMass = 1f;
    [SerializeField] private float pressDepth = 0.02f;

    public UnityEvent onPressed;
    public UnityEvent onReleased;

    private Vector3 restPosition;
   
    private int load;

    private void Awake()
    {
        restPosition = transform.localPosition;
    }

    private void Update()
    {
        Vector3 target = load > 0
            ? restPosition + new Vector3(0f, -pressDepth, 0f)
            : restPosition;
        transform.localPosition = target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Counts(other))
        {
            load++;
            if (load == 1) onPressed.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (Counts(other))
        {
            load--;
            if (load == 0) onReleased.Invoke();
        }
    }

    private bool Counts(Collider other)
    {
        Rigidbody otherRigidbody = other.GetComponent<Rigidbody>();
        return otherRigidbody.mass >= requiredMass;    
    }
}

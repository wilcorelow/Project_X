using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] float radius = 3f;
    [SerializeField] Transform interactionTransform;

    [SerializeField] bool autoInteract;

    bool isFocus = false;
    /// <summary>
    /// Interactable object is learning the player who focused to the interactable object to check distance.
    /// </summary>
    Transform player;

    bool hasInteract = false;

    /// <summary>
    /// This method meant to be overwritten.
    /// </summary>
    public virtual void Interact()
    {
        Debug.Log("Interracting with " + transform.name);
        hasInteract = true;
    }

    void OnDrawGizmosSelected()
    {
        if (interactionTransform == null)
        {
            interactionTransform = transform;
        }
        
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interactionTransform.position, radius);
    }

    public void OnFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
    }

    public void onDefocused()
    {
        isFocus = false;
        player = null;
    }

    public void CheckDistanceAndInteract()
    {
        if (!autoInteract && isFocus && !hasInteract)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if (distance <= radius)
            {
                Interact();
            }
        }
    }
    
    void OnCollisionEnter(Collision col)
    {
        if (autoInteract && !hasInteract && col.transform.CompareTag("Player"))
        { 
            Interact();
        }
    }
}

using UnityEngine;

public class ConsumerScanner : MonoBehaviour
{
    public LayerMask LayerMask;
    public Consumable CurrentConsumableInSight;

    public void OnTriggerEnter(Collider collider)
    {
        // TODO: also leave it...
        if ((LayerMask.value & (1 << collider.gameObject.layer)) == 0) return;

        var consumable = collider.gameObject.GetComponentInParent<Consumable>();
        
        if (consumable == null) return;

        CurrentConsumableInSight = consumable;
    }
}
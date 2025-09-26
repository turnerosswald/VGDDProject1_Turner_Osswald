using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class KnockbackCube : MonoBehaviour
{
    [SerializeField] private float knockbackForce = 20f;

    private BoxCollider box;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        box.isTrigger = true;
    }

    private void OnEnable()
    {
        ApplyKnockback();
    }

    private void ApplyKnockback()
    {
        Collider[] hits = Physics.OverlapBox(box.bounds.center, box.bounds.extents, transform.rotation);

        foreach (Collider hit in hits)
        {
            if (!hit.CompareTag("Enemy") && !hit.CompareTag("Enemy2"))
                continue;

            Rigidbody rb = hit.attachedRigidbody;
            if (rb == null || rb.isKinematic)
                continue;

            Vector3 pushDir = (hit.transform.position - transform.position).normalized;
            rb.AddForce(pushDir * knockbackForce, ForceMode.VelocityChange);
        }
    }
}

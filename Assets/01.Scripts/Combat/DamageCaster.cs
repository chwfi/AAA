using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    private float _detectRange = 1f;
    [SerializeField]
    private Transform _castTrm;

    public void CastDamage(LayerMask layer)
    {
        bool raycastSuccess = Physics.SphereCast(_castTrm.position, _detectRange, _castTrm.forward, out RaycastHit raycastHit, 0, layer);

        if (raycastSuccess
            && raycastHit.collider.TryGetComponent(out Health health))
        {
            int damage = 10;

            health.ApplyDamage(damage, raycastHit.point, raycastHit.normal);
        }
    }
}

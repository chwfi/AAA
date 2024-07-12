using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    private float _castDistance = 5f;
    [SerializeField]
    private Transform _castTrm;

    public void CastDamage(LayerMask layer)
    {
        bool raycastSuccess = Physics.Raycast(_castTrm.position, _castTrm.forward, out RaycastHit raycastHit, _castDistance, layer);

        if (raycastSuccess)
        {
            Health health = raycastHit.collider.transform.GetComponentInParent<Health>();
            int damage = 10;

            health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, HitTypeEnum.Spark);
        }
    }
}
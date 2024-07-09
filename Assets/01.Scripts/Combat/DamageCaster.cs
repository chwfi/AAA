using UnityEngine;

public class DamageCaster : MonoBehaviour
{
    [SerializeField]
    private float _detectRange = 1f;
    [SerializeField]
    private float _castDistance = 5f; // 캐스트 거리 추가
    [SerializeField]
    private Transform _castTrm;

    public void CastDamage(LayerMask layer)
    {
        bool raycastSuccess = Physics.Raycast(_castTrm.position, _castTrm.forward, out RaycastHit raycastHit, _castDistance, layer);

        if (raycastSuccess
            && raycastHit.collider.TryGetComponent(out Health health))
        {
            int damage = 10;

            health.ApplyDamage(damage, raycastHit.point, raycastHit.normal, HitTypeEnum.Normal);
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(_castTrm.position, _castTrm.position + _castTrm.forward * _castDistance);
    }
    #endif
}
using System;
using System.Linq;
using UniRx;
using UnityEngine;

/// <summary>
/// プレイヤーにパリィさせるコンポーネント
/// </summary>
public class PlayerParry : MonoBehaviour
{
    /// <summary> 設定データ </summary>
    [SerializeField]
    private ParryData _parryData;
    /// <summary> パリィ範囲のコライダー </summary>
    [SerializeField]
    private CircleCollider2D _hitCollider;

    /// <summary> パリィ待機中かどうか </summary>
    private bool _stand;
    /// <summary> パリィする対象のRigidbody2D </summary>
    private Collider2D[] _hits;

    /// <summary>
    /// パリィ判定の開始
    /// </summary>
    public void StandParry()
    {
        if(!_stand)
        {
            _stand = true;
            var timer = Observable.Timer(TimeSpan.FromSeconds(_parryData.ParryTime)).First();

            Observable.EveryUpdate().TakeUntil(timer).Where(_ => (_hits = Physics2D.OverlapCircleAll((Vector2)transform.position, _hitCollider.radius, _parryData.AttackLayer)).Length > 0).Subscribe(_ =>
                {
                    Debug.Log("パリィ");
                    _hits.ToList().ForEach(hit =>
                        {
                            var rb2d = hit.GetComponent<Rigidbody2D>();
                            rb2d.linearVelocity = (hit.transform.position - transform.position).normalized * _parryData.ParrySpeed;
                            hit.GetComponent<BulletHitAndDestroySys>()._isParry = true;
                        }
                    );
                }
            );

            timer.Subscribe(_ =>
                {
                    _stand = false;
                }
            );
        }
    }
}

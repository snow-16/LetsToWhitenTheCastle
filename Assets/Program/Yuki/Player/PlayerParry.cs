using System;
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

    /// <summary> パリィ待機中かどうか </summary>
    private bool _stand;
    /// <summary> パリィする対象のRigidbody2D </summary>
    private Rigidbody2D _hit;

    /// <summary>
    /// パリィ判定の開始
    /// </summary>
    public void StandParry()
    {
        if(!_stand)
        {
            _stand = true;

            var timer = Observable.Timer(TimeSpan.FromSeconds(_parryData.ParryTime)).First();

            Observable.EveryUpdate().TakeUntil(timer).Where(_ => _hit).First().Subscribe(_ =>
                {
                    _hit.linearVelocity = -_hit.linearVelocity;
                    _hit = null;
                }
            );

            timer.Subscribe(_ =>
                {
                    _stand = false;
                }
            );
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(_stand && (collision.gameObject.layer & _parryData.AttackLayer) > 0)
        {
            collision.gameObject.TryGetComponent(out _hit);
        }
    }
}

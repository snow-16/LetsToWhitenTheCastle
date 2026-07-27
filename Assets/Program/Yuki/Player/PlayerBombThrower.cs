using UnityEngine;

/// <summary>
/// プレイヤーの爆弾攻撃用コンポーネント
/// </summary>
public class PlayerBombThrower : MonoBehaviour
{
    /// <summary> 爆弾のプレハブ </summary>
    [SerializeField]
    private GameObject _bombPrefab;
    /// <summary> 爆弾の高度別着弾地点 </summary>
    [SerializeField]
    private Transform[] _bombTargetPoints = new Transform[3];
    /// <summary> 爆弾を投げる中心 </summary>
    [SerializeField]
    private Transform _throwPoint;
    /// <summary> 爆弾を投げる高さ </summary>
    [SerializeField]
    private float _throwHeight;
    /// <summary> 爆弾の投擲速度 </summary>
    [SerializeField]
    private float _throwSpeed;

    /// <summary>
    /// 爆弾を投擲する
    /// </summary>
    public void ThrowBomb()
    {
        var bomb = Instantiate(_bombPrefab, _throwPoint.position, _bombPrefab.transform.rotation).GetComponent<BombMover>();
        bomb.Throw(_throwSpeed, _bombTargetPoints[0].position, _throwHeight);
    }
}

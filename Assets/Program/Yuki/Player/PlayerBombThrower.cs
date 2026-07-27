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
    /// <summary> 爆弾の投擲速度 </summary>
    [SerializeField]
    private float _throwSpeed;

    /// <summary>
    /// 爆弾を投擲する
    /// </summary>
    public void ThrowBomb()
    {
        Instantiate(_bombPrefab, _throwPoint.position, _bombPrefab.transform.rotation);
    }
}

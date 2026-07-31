using UnityEngine;

/// <summary>
/// プレイヤーの体力管理を調整するコンポーネント
/// </summary>
public class PlayerLifeManager : MonoBehaviour
{
    /// <summary> 設定データ </summary>
    [SerializeField]
    private PlayerLifeData _playerLifeData;
    /// <summary> プレイヤーのLifeSystemのインスタンス </summary>
    [SerializeField]
    private LifeSystem _playerLifeSystem;

    void Start()
    {
        _playerLifeSystem.HP = _playerLifeSystem.MaxHP = _playerLifeData.MaxHP;
        _playerLifeSystem.InvincibleTime = _playerLifeData.InvincibleTime;
    }
}

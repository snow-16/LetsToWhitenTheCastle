using UnityEngine;

/// <summary>
/// プレイヤーの各種状態を保存するコンポーネント
/// </summary>
public class PlayerStateHolder : MonoBehaviour
{
    /// <summary> 爆弾の入手に必要な手裏剣ヒット数 </summary>
    [SerializeField]
    [Tooltip("爆弾を入手できる手裏剣のヒット回数です。")]
    private int _bombGetBorder;
    /// <summary> 爆弾の入手に必要な手裏剣ヒット数 </summary>
    public int BombGetBorder { get => _bombGetBorder; set => _bombGetBorder = value; }

    /// <summary> 爆弾の最大数 </summary>
    [SerializeField]
    private int _maxBombs;
    /// <summary> 爆弾の最大数 </summary>
    public int MaxBombs { get => _maxBombs; set => _maxBombs = value; }

    /// <summary> プレイヤーのジャンプ状態 </summary>
    public PlayerJumpState PlayerJumpState { get; set; } = PlayerJumpState.Fall;
    /// <summary> 手裏剣を当てた回数 </summary>
    public int HitCount { get; set; }
    /// <summary> 爆弾の所持数 </summary>
    public int BombCount { get; set; }
}

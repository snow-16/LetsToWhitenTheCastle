using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 爆弾の所持数に応じて爆弾アイコンを表示するUI
/// </summary>
public class BombIconUI : MonoBehaviour
{
    /// <summary> プレイヤーの状態 </summary>
    [SerializeField]
    private PlayerStateHolder _stateHolder;

    /// <summary> 爆弾アイコンを並べる親 </summary>
    [SerializeField]
    private Transform _iconParent;

    /// <summary> 爆弾アイコンのPrefab </summary>
    [SerializeField]
    private GameObject _bombIconPrefab;

    private int _oldBombCount = -1;

    private void Update()
    {
        // 爆弾数が変わっていなければ何もしない
        if (_oldBombCount == _stateHolder.BombCount)
        {
            return;
        }

        _oldBombCount = _stateHolder.BombCount;

        UpdateIcons();
    }

    /// <summary>
    /// 爆弾アイコンを現在の所持数に合わせる
    /// </summary>
    private void UpdateIcons()
    {
        // 今あるアイコンを全部削除
        foreach (Transform child in _iconParent)
        {
            Destroy(child.gameObject);
        }

        // 所持している爆弾の数だけアイコンを作る
        for (int i = 0; i < _stateHolder.BombCount; i++)
        {
            Instantiate(_bombIconPrefab, _iconParent);
        }
    }
}

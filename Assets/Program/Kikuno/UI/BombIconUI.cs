using System.Collections.Generic;
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

    /// <summary> 爆弾アイコンのスプライト </summary>
    [SerializeField]
    private Sprite[] _bombSprites = new Sprite[2];

    private int _oldBombCount = -1;
    /// <summary> 全爆弾のアイコン </summary>
    private List<Image> _bombIcons = new();

    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            _bombIcons.Add(Instantiate(_bombIconPrefab, _iconParent).GetComponent<Image>());
        }
    }

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
        for(int i = 0; i < _bombIcons.Count; i++)
        {
            if(i < _stateHolder.BombCount)
            {
                _bombIcons[i].sprite = _bombSprites[1];
            }
            else
            {
                _bombIcons[i].sprite = _bombSprites[0];
            }
        }
    }
}

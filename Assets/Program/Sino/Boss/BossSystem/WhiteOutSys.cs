using UnityEngine;

public class WhiteOutSys : MonoBehaviour
{
    GameObject _mine;
    LifeSystem _lifeSystem;
    SpriteRenderer _spriteRenderer;
    Color _color;
    [Tooltip("どれだけ白くなるか")]
    [SerializeField] float _whitePercent = 1;
    float _lifePercent;

    void Start()
    {
        _mine = GameObject.FindWithTag("Boss");
        _lifeSystem = _mine.GetComponent<LifeSystem>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _color = _spriteRenderer.color;
    }

    void Update()
    {
        _lifePercent = 1f - (float)_lifeSystem.HP / _lifeSystem.MaxHP;
        _color.a = _lifePercent * _whitePercent;
        _spriteRenderer.color = _color;
    }
}

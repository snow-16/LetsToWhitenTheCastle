using System;
using UnityEngine;

/// <summary>
/// プレイヤーの横軸移動用コンポーネント
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerWalker : MonoBehaviour
{
    [SerializeField]
    private WalkOrSprintProperty _speed;
    [SerializeField]
    private WalkOrSprintProperty _maxSpeed;
    [SerializeField]
    private WalkOrSprintProperty _initialSpeed;
    [SerializeField]
    private float _damping;

    private float _nowSpeed = 0;
    private bool _isSprint = false;

    private Rigidbody2D _rigidbody2D;

    void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        _nowSpeed = Mathf.Max(_damping * Mathf.Abs(_nowSpeed), 0) * Mathf.Sign(_nowSpeed);
        transform.Translate(_nowSpeed, 0, 0);
    }

    public void Walk(float direction)
    {
        _nowSpeed += _speed.walk * (_isSprint ? _speed.sprintMultipiler : 1) * direction;
        var moveForword = _nowSpeed == 0 || Mathf.Sign(_nowSpeed) == direction;

        if(moveForword && Mathf.Abs(_nowSpeed) < _initialSpeed.walk * (_isSprint ? _initialSpeed.sprintMultipiler : 1))
        {
            _nowSpeed = _initialSpeed.walk * (_isSprint ? _initialSpeed.sprintMultipiler : 1) * direction;
        }
        else if(moveForword && Mathf.Abs(_rigidbody2D.linearVelocityX) > _maxSpeed.walk * (_isSprint ? _maxSpeed.sprintMultipiler : 1))
        {
            _nowSpeed = _maxSpeed.walk * (_isSprint ? _maxSpeed.sprintMultipiler : 1) * Mathf.Sign(_rigidbody2D.linearVelocityX);
        }
    }

    public void SwitchSprint()
    {
        _isSprint = !_isSprint;
    }

    [Serializable]
    private struct WalkOrSprintProperty
    {
        public float walk;
        public float sprintMultipiler;
    }
}

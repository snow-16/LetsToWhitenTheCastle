using UnityEngine;

public class BulletTimeDestroy : MonoBehaviour
{
    [SerializeField]float _destroyTime = 10;
    float _nowTime;
    void Update()
    {
        if (_destroyTime < _nowTime) Destroy(gameObject);
        else _nowTime += Time.deltaTime;
    }
}

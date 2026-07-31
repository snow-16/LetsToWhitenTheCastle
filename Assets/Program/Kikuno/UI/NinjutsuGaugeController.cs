using UnityEngine;

public class NinjutsuGaugeController : MonoBehaviour
{
    [SerializeField]
    private PlayerStateHolder _stateHolder;

    [SerializeField]
    private BombData _bombData;

    [SerializeField]
    private GaugeUI _gaugeUI;

    private void Update()
    {
        _gaugeUI.UpdateGauge( _stateHolder.HitCount, _bombData.CollectLate);
    }
}

using UnityEngine;

public class HPGaugeController : MonoBehaviour
{
    [SerializeField]
    private LifeSystem _lifeSystem;

    [SerializeField]
    private GaugeUI _gaugeUI;

    private void Update()
    {
        _gaugeUI.UpdateGauge(_lifeSystem.HP, _lifeSystem.MaxHP);
    }
}

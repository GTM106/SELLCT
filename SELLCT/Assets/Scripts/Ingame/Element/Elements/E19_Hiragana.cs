using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E19_Hiragana : Card
{
    [SerializeField] HiraganaHandView _hiraganaHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;

    readonly int elementIndex = (int)StringManager.Element.E19;

    public override int Id => 19;

    private void Awake()
    {
        _phaseController.OnTradingPhaseStart.Add(OnPhaseStart);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseStart.Remove(OnPhaseStart);
    }

    private void OnPhaseStart()
    {
        StringManager.hasElements[elementIndex] = _handMediator.ContainsCard(this);
        _hiraganaHandView.Set();
    }

    public override void Buy()
    {
        base.Buy();

        StringManager.hasElements[elementIndex] = true;
        _hiraganaHandView.Set();
    }

    public override void Sell()
    {
        base.Sell();

        if (_handMediator.ContainsCard(this)) return;
        StringManager.hasElements[elementIndex] = false;
        _hiraganaHandView.Set();
    }
}

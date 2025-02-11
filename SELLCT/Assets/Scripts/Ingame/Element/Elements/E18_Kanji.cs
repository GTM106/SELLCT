using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E18_Kanji : Card
{
    [SerializeField] KanjiHandView _kanjiHandView = default!;
    [SerializeField] PhaseController _phaseController = default!;
    [SerializeField] DeckUIController _deckUIController = default!;

    readonly int elementIndex = (int)StringManager.Element.E18;

    public override int Id => 18;

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
        _kanjiHandView.Set();

        if (StringManager.hasElements[elementIndex])
        {
            _deckUIController.EnableKanji();
        }
        else
        {
            _deckUIController.DisableKanji();
        }
    }

    public override void Buy()
    {
        base.Buy();

        StringManager.hasElements[elementIndex] = true;
        _kanjiHandView.Set();
        _deckUIController.EnableKanji();
    }

    public override void Sell()
    {
        base.Sell();

        if (_handMediator.ContainsCard(this)) return;
        StringManager.hasElements[elementIndex] = false;
        _kanjiHandView.Set();
        _deckUIController.DisableKanji();
    }
}

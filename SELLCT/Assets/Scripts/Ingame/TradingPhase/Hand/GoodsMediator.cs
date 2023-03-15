using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsMediator : DeckMediator
{
    [SerializeField] Hand _hand = default!;
    [SerializeField] List<CardUIHandler> _clickHandlers = new();
    [SerializeField] TraderController _traderController = default!;
    [SerializeField] PhaseController _phaseController = default!;
    
    //プレイヤーが売却したカードが一時的に入るデッキ
    BuyingCardDeck _buyingCardDeck = new();

    private void Awake()
    {
        _phaseController.OnTradingPhaseComplete.Add(OnComplete);
        _phaseController.OnTradingPhaseStart.Add(InitTakeCard);
    }

    private void OnDestroy()
    {
        _phaseController.OnTradingPhaseComplete.Remove(OnComplete);
        _phaseController.OnTradingPhaseStart.Remove(InitTakeCard);
    }

    //売買フェーズ終了時に行う処理。
    private async UniTask OnComplete()
    {
        var token = this.GetCancellationTokenOnDestroy();

        while (true)
        {
            Card card = _buyingCardDeck.Draw();

            if (card is EEX_null) break;

            _traderController.CurrentTrader.TraderDeck.Add(card);
        }

        await UniTask.Yield(token);
    }

    private void InitTakeCard()
    {
        int drawableCount = _hand.CalcDrawableCount();

        for (int i = 0; i < drawableCount; i++)
        {
            Card top = _traderController.CurrentTrader.TraderDeck.Draw();

            //手札に追加
            _hand.Add(top);

            //UI要素に追加
            _clickHandlers[i].InsertCard(top);
        }
    }

    public override Card TakeDeckCard()
    {
        if (_hand.CalcDrawableCount() == 0) return EEX_null.Instance;

        Card top = _traderController.CurrentTrader.TraderDeck.Draw();

        //手札に追加
        _hand.Add(top);

        return top;
    }

    public override void RemoveHandCard(Card card)
    {
        _hand.Remove(card);
    }

    public override void AddDeck(Card card)
    {
        _traderController.CurrentTrader.TraderDeck.Add(card);
    }

    public override void RearrangeCardSlots()
    {
        int handCapacity = _hand.HandCapacity();

        //順番入れ替え
        for (int i = 0; i < handCapacity - 1; i++)
        {
            if (true == _clickHandlers[i].NullCheck())
            {
                //カード名取得
                var cardName = _clickHandlers[i + 1].GetCardName();
                _clickHandlers[i + 1].InsertCard(EEX_null.Instance);
                _clickHandlers[i].InsertCard(cardName);
            }
        }
    }

    public override void AddBuyingDeck(Card card)
    {
        _buyingCardDeck.Add(card);
    }

    public override bool ContainsCard(Card card)
    {
        return _traderController.CurrentTrader.TraderDeck.ContainsCard(card) || _hand.ContainsCard(card) || _buyingCardDeck.ContainsCard(card);
    }
}

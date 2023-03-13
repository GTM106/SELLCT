using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class E0_Life : Card
{
    [SerializeField] CardParameter _parameter = default!;
    [SerializeField] MoneyPossessedController _controller = default!;
    [SerializeField] Sprite _cardSprite = default!;
    [SerializeField] HandMediator _handMediator = default!;

    public override bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public override int Rarity => _parameter.Rarity();
    public override Sprite CardSprite => _cardSprite;

    public override void Buy()
    {
        //TODO:SE301の再生
        //TODO:画面全体を脈動させるアニメーション
        //TODO:テキストボックスを更新する

        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        GameOverChecker();
    }

    private void GameOverChecker()
    {
        //売った際に命が1枚もないならゲームオーバー
        if (_handMediator.ContainsCard(this)) return;

        Debug.LogWarning("命がなくなりました。シーン3に遷移する処理は未実装なため続行されます。");
        //TODO:シーン3に遷移
    }
}
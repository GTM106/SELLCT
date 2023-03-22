using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class E2_Sell : Card
{
    [SerializeField] CardParameter _parameter = default!;
    [SerializeField] MoneyPossessedController _controller = default!;
    [SerializeField] Sprite _baseSprite = default!;
    [SerializeField] Sprite _number = default!;
    [SerializeField] Sprite _chineseCharacters = default!;
    [SerializeField] Sprite _hiragana = default!;
    [SerializeField] Sprite _katakana = default!;
    [SerializeField] Sprite _alphabet = default!;
    [SerializeField] HandMediator _handMediator = default!;
    [SerializeField] Color defaultColor = default!;
    [SerializeField] Color changeColor = default!;
    [SerializeField] CardUIInstance cardUIInstance = default!;

    bool currentColor = true;

    readonly List<Sprite> result = new();
    public override bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public override int Rarity => _parameter.Rarity();
    public override IReadOnlyList<Sprite> CardSprite
    {
        get
        {
            //初期化
            if (result.Count == 0)
            {
                result.Add(_baseSprite);
                result.Add(_number);
                result.Add(_chineseCharacters);
                result.Add(_hiragana);
                result.Add(_katakana);
                result.Add(_alphabet);
            }
            return result;
        }
    }
    public override bool ContainsPlayerDeck => _handMediator.ContainsCard(this);

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());
        Debug.Log("買う");
        SellChecker(defaultColor);
    }

    public override void Passive()
    {
        //TODO:SE2の再生
    }

    public override void Sell()
    {
        if (_handMediator.ContainsCard(this)) return;
        _controller.IncreaseMoney(_parameter.GetMoney());
        Debug.Log("売る");
        SellChecker(changeColor);
    }

    public void SellChecker(Color color)
    {
        foreach (var cardUIHandler in cardUIInstance.Handlers)
        {
            if (color == defaultColor)
            {
                cardUIHandler.EnabledSelectebility();
            }
            else
            {
                cardUIHandler.DisableSelectability();
            }

            foreach (var cardImage in cardUIHandler.CardImages)
            {
                cardImage.color = color;
            }
        }
    }
}

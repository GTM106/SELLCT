using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E8_BGM : Card
{
    [SerializeField] CardParameter _parameter = default!;
    [SerializeField] MoneyPossessedController _controller = default!;
    [SerializeField] Sprite _baseSprite = default!;
    [SerializeField] Sprite _number = default!;
    [SerializeField] Sprite _chineseCharacters = default!;
    [SerializeField] Sprite _hiragana = default!;
    [SerializeField] Sprite _katakana = default!;
    [SerializeField] HandMediator _handMediator = default!;

    readonly List<Sprite> result = new();

    public override bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public override int Rarity => _parameter.Rarity();
    public override IReadOnlyList<Sprite> CardSprite
    {
        get
        {
            //������
            if (result.Count == 0)
            {
                result.Add(_baseSprite);
                result.Add(_number);
                result.Add(_chineseCharacters);
                result.Add(_hiragana);
                result.Add(_katakana);
            }

            return result;
        }
    }

    public override void Buy()
    {
        //TODO:BGM�̍Đ�

        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        StopBGM();

        _controller.IncreaseMoney(_parameter.GetMoney());
    }

    private void StopBGM()
    {
        if (_handMediator.ContainsCard(this)) return;

        //TODO:BGM�̒�~
    }
}
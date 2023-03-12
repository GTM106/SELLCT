using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class E0_Life : Card
{
    [SerializeField] CardParameter _parameter = default!;
    [SerializeField] MoneyPossessedController _controller = default!;
    [SerializeField] Sprite _cardSprite = default!;

    public override bool IsDisposedOfAfterSell => _parameter.IsDisposedOfAfterSell();
    public override int Rarity => _parameter.Rarity();
    public override Sprite CardSprite => _cardSprite;

    public override void Buy()
    {
        //TODO:SE301�̍Đ�
        //TODO:��ʑS�̂𖬓�������A�j���[�V����
        //TODO:�e�L�X�g�{�b�N�X���X�V����

        _controller.DecreaseMoney(_parameter.GetMoney());
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());
    }
}
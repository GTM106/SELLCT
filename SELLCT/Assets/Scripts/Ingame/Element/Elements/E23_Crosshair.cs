using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E23_Crosshair : Card
{
    [SerializeField] CursorController _cursorController = default!;

    public override int Id => 23;

    public override void Buy()
    {
        _moneyPossessedCcontroller.DecreaseMoney(_parameter.GetMoney());

        _cursorController.Enable();
    }

    public override void OnPressedU6Button()
    {
        throw new System.NotImplementedException();
    }

    public override void Sell()
    {
        _moneyPossessedCcontroller.IncreaseMoney(_parameter.GetMoney());

        if (_handMediator.ContainsCard(this)) return;

        _cursorController.Disable();
    }
}

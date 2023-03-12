using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderDeckGenerator : MonoBehaviour
{
    [SerializeField] CardPool _cardPool = default!;

    [SerializeField] TradersInstance _tradersInstance = default!;

    //Edit > Project Settings > Script Execution Orderで実行順を調整しています。
    private void Awake()
    {
        AddTrader2_7Deck();
        AddTrader1Deck();
    }

    //TR2〜7の処理
    private void AddTrader2_7Deck()
    {
        for (int i = 1; i < _tradersInstance.Traders.Count; i++)
        {
            _tradersInstance.Traders[i].CreateDeck(_cardPool);
        }
    }

    //TR1の処理
    private void AddTrader1Deck()
    {
        _tradersInstance.Traders[0].CreateDeck(_cardPool);
    }
}

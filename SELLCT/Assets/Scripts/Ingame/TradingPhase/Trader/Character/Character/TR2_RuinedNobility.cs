using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static EndingController;

public class TR2_RuinedNobility : Trader
{
    protected override void Awake()
    {
        base.Awake();
    }

    public override void CreateDeck(CardPool pool)
    {
        List<Card> list = new();
        list.AddRange(pool.Pool());

        foreach (var item in traderParameter.PriorityCards)
        {
            for (int i = 0; i < item.Priority; i++)
            {
                list.Add(item.Card);
            }
        }

        for (int i = 0; i < traderParameter.InitalDeckCount; i++)
        {
            int index = Random.Range(0, list.Count);
            if (list.Count <= index) break;

            Card card = pool.Draw(list[index]);
            list.RemoveAt(index);

            //今引いたカードがpoolの最後のカードならもう抽選されないようにする
            if (!pool.Pool().Contains(card))
            {
                list.RemoveAll(x => x == card);
            }

            if (card.Id < 0) break;
            CardCount[card.Id]++;
            deck.Add(card);
        }
    }

    public override ConversationMessage BuyMessage(Card card)
    {
        List<ConversationData> data = _buy.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int cardId = card.Id;

        string[] texts = data[cardId].Text;
        int[] face = data[cardId].Face;
        string[] name = data[cardId].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage CardMessage(Card card)
    {
        List<ConversationData> data = _select.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int cardId = card.Id;

        string[] texts = data[cardId].Text;
        int[] face = data[cardId].Face;
        string[] name = data[cardId].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage EndMessage()
    {
        List<ConversationData> end = _end.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = Random.Range(0, end.Count());

        string[] texts = end[index].Text;
        int[] face = end[index].Face;
        string[] name = end[index].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage SellMessage(Card card)
    {
        List<ConversationData> data = _sell.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int cardId = card.Id;

        string[] texts = data[cardId].Text;
        int[] face = data[cardId].Face;
        string[] name = data[cardId].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage StartMessage()
    {
        List<ConversationData> start = _start.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = Random.Range(0, start.Count());

        string[] texts = start[index].Text;
        int[] face = start[index].Face;
        string[] name = start[index].Name;

        return new(texts, face, name);
    }

    public override ConversationMessage SceneEndingMessage(EndingScene endingScene)
    {
        List<ConversationData> data = _sceneEnding.datas.Where(x => x.Likability.Contains((int)favorability.FavorabilityClassification())).ToList();

        int index = (int)endingScene + 1;

        string[] texts = data[index].Text;
        int[] face = data[index].Face;
        string[] name = data[index].Name;

        return new(texts, face, name);
    }

    public override void OnPlayerSell(Card selledCard)
    {
        //TODO：もし与えられたカードが仲間カードなら好感度減少処理を行う。

        //好感度上昇処理
        Favorability totalAddValue = traderParameter.AddFavorabilityValue;

        if (traderParameter.FavoriteCards.Contains(selledCard))
        {
            totalAddValue.Add(traderParameter.FavoriteCardBonus);
        }

        favorability = favorability.Add(totalAddValue);
    }

    public override void OnPlayerBuy(Card card)
    {
        //買い処理はお気に入りエレメントは関係ない
        favorability = favorability.Add(traderParameter.AddFavorabilityValue);
    }
}

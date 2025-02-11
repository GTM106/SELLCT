using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PriorityCard
{
    [SerializeField] Card _card;
    [SerializeField, Min(0)] int _priority;

    public Card Card => _card;

    public int Priority => _priority;

}

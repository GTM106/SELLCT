using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoChoice : ITalkChoice
{
    public int Id => 1;

    public string Text => "いいえ";

    public bool Enabled => _enabled;

    bool _enabled;

    public NoChoice(bool enabled)
    {
        _enabled = enabled;
    }

    public void Disable()
    {
        _enabled = false;
    }

    public void Enable()
    {
        _enabled = true;
    }

    public void Select()
    {
        if (!Enabled)
        {
            //TODO:SE2の再生
            Debug.Log("Noは無効です");
            return;
        }

        Debug.Log("No...");
    }
}

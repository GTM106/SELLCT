using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextBoxView : MonoBehaviour
{
    [SerializeField, Min(0)] int _delayFrame = 3;
    [SerializeField] TextMeshProUGUI _text;

    public async void DisplayTextOneByOne()
    {
        var cancellationToken = this.GetCancellationTokenOnDestroy();

        string s = _text.text;

        for (int i = 0; i <= s.Length; i++)
        {
            //範囲演算子を用いています。Substringと同じ効果です
            string newText = s[..i];
            _text.text = newText;

            await UniTask.DelayFrame(_delayFrame,PlayerLoopTiming.FixedUpdate, cancellationToken);
        }
    }

    public void UpdeteText(string text)
    {
        _text.text = text;
    }
}
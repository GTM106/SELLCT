using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TextBoxView : MonoBehaviour
{
    [SerializeField, Min(0)] int _delayFrame = 3;

    //U5
    [SerializeField] Image _messageImage = default!;
    [SerializeField] TextMeshProUGUI _messageText = default!;

    //U13
    [SerializeField] TextMeshProUGUI _speakerText = default!;

    CancellationTokenSource _cancellationTokenSource = default!;

    bool _imageEnabled;

    public async UniTask DisplayTextOneByOne()
    {
        var cancellationToken = this.GetCancellationTokenOnDestroy();

        //前回の非同期処理が実行中であればキャンセルする
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        string s = _messageText.text;

        for (int i = 0; i <= s.Length; i++)
        {
            SoundManager.Instance.PlaySE(SoundSource.SE13_TEXT, 0.5f);

            // 範囲演算子を用いています。Substringと同じ効果です
            string newText = s[..i];
            _messageText.text = newText;

            try
            {
                await UniTask.DelayFrame(_delayFrame, PlayerLoopTiming.FixedUpdate, _cancellationTokenSource.Token);
            }
            catch (OperationCanceledException)
            {
                //キャンセルされた場合は処理を終了する
                throw new OperationCanceledException("新たなテキストが代入されました。以前のテキスト処理を終了してください。");
            }
        }

        // CancellationTokenSourceをnullにする
        _cancellationTokenSource = null;
    }

    /// <summary>
    /// テキストを変更します。
    /// </summary>
    /// <param name="speaker"></param>
    /// <param name="speakerSprite"></param>
    /// <param name="message"></param>
    public void UpdateText(string speaker, string message)
    {
        _speakerText.text = speaker.ToDisplayString();
        _messageText.text = message.ToDisplayString();
    }

    public void UpdateDisplay()
    {
        bool spekerImageEnabled = !string.IsNullOrEmpty(_speakerText.text) && _imageEnabled;

        //ImageだけでTextはE14によって左右されない
        _messageImage.enabled = _imageEnabled;
    }

    public void Enable()
    {
        _messageImage.enabled = true;
        _imageEnabled = true;
    }

    public void Disable()
    {
        _messageImage.enabled = false;
        _imageEnabled = false;
    }
}
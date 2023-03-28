using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class E12_Eye : Card
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

    [SerializeField] Material _eye = default!;
    [Header("Default�̉𑜓x��1�{�Ƃ��āA�ȉ��̒l�ŏ㏸���܂��B\n��F�l��2�̏ꍇ 0.5��1")]
    [SerializeField, Range(1f, 2160f)] float _eyeIncreaseValue;
    [Header("Default�̉𑜓x��1�{�Ƃ��āA�ȉ��̒l�Ō������܂��B\n��F�l��0.7�̏ꍇ 1��0.7")]
    [SerializeField, Range(0.0001f, 1f)] float _eyeDecreaseValue;

    [Header("�����̉𑜓x��ݒ肵�܂��B\n�܂��A�����̃G�������g�������Ɋ֌W�Ȃ����̒l�ɂȂ�܂��B")]
    [SerializeField, Range(0, 2160f)] float _firstEye = 1080f;

    float _currentEyeValue = 1.0f;
    const float MAX_VALUE = 2160f;
    const float MIN_VALUE = 1;

    readonly List<Sprite> result = new();

    public override string CardName => _parameter.GetName();
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
                result.Add(_alphabet);
            }

            return result;
        }
    }
    public override bool ContainsPlayerDeck => _handMediator.ContainsCard(this);

    private void Awake()
    {
        //�ŏ��̏��������Ɗ֌W�Ȃ��ݒ肳���B�t�F�[�Y�Ɗ֌W�Ȃ��ݒ肷�邽�߁A��肪��������ύX����
        _currentEyeValue = _firstEye;
        SetEye();
    }

    public override void Buy()
    {
        _controller.DecreaseMoney(_parameter.GetMoney());

        IncreaseEyeValue();
    }

    public override void Passive()
    {
        // DoNothing
    }

    public override void Sell()
    {
        _controller.IncreaseMoney(_parameter.GetMoney());

        DesreaseEyeValue();
    }

    private void IncreaseEyeValue()
    {
        _currentEyeValue = Mathf.Min(MAX_VALUE, _currentEyeValue * _eyeIncreaseValue);

        SetEye();
    }

    private void DesreaseEyeValue()
    {
        _currentEyeValue = Mathf.Max(MIN_VALUE, _currentEyeValue * _eyeDecreaseValue);

        SetEye();
    }

    private void SetEye()
    {
        _eye.SetFloat("_Resolution", _currentEyeValue);
    }


#if UNITY_EDITOR
    class EyeResetWindow : EditorWindow
    {
        private E12_Eye _eye;
        private float _eyeValue = 1080f;
        public void SetEye(E12_Eye eye)
        {
            _eye = eye;
        }

        [MenuItem("Window/Util/Eye Window")]
        public static void ShowWindow()
        {
            EyeResetWindow window = GetWindow<EyeResetWindow>("Eye Changer");
            E12_Eye eye = FindObjectOfType<E12_Eye>();
            window.SetEye(eye);
        }

        private void OnGUI()
        {
            if (_eye != null)
            {
                if (GUILayout.Button("Reset"))
                {
                    _eye._eye.SetFloat("_Resolution", 1080f);
                    _eyeValue = 1080f;
                }
                GUILayout.Space(10);
                if (GUILayout.Button("Set EyeValue"))
                {
                    _eye._eye.SetFloat("_Resolution", _eyeValue);
                }

                _eyeValue = EditorGUILayout.Slider("Eye Value", _eyeValue, 1f, 2160f);
            }
            else
            {
                EditorGUILayout.HelpBox("E12_Eye object not found in scene.", MessageType.Warning);
                if (GUILayout.Button("Reload"))
                {
                    ShowWindow();
                }
            }
        }
    }
#endif

}
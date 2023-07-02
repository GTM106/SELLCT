using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class E0_Life : Card
{
    [Header("End5�u�S���Ƃ܂�v���A�^�b�`���Ă�������")]
    [SerializeField] End_5 end_5;

    public override int Id => 0;

    public override void Buy()
    {
        base.Buy();
    }

    public override void Sell()
    {
        //�Q�[���I�[�o�[�Ȃ�ʏ퉉�o�ɂ͂����Ȃ�
        if (GameOverChecker()) return;

        base.Sell();
    }

    private bool GameOverChecker()
    {
        //�������ۂɖ���1�����Ȃ��Ȃ�Q�[���I�[�o�[
        if (_handMediator.ContainsCard(this)) return false;

        end_5.End_5Transition();
        return true;
    }
}
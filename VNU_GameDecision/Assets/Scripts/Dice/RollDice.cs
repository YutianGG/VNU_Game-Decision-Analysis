using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    [Header("��l����")]
    [SerializeField] GameObject[] dice;
    [SerializeField] Sprite[] diceSprite;
    [Header("��l�I��")]
    [SerializeField] int[] dicePoint;
    [Header("��r����")]
    [SerializeField] GameObject[] text;
    [SerializeField] Text point;

    SpriteRenderer sr0;
    SpriteRenderer sr1;
    SpriteRenderer sr2;
    private void Start()
    {
        sr0 = dice[0].GetComponent<SpriteRenderer>();
        sr1 = dice[1].GetComponent<SpriteRenderer>();
        sr2 = dice[2].GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// �Y��l
    /// </summary>
    public void Roll()
    {
        //������r����
        for (int i = 0; i < text.Length; i++) 
        {
            text[i].SetActive(false);
        }

        //�ͦ��I��
        for (int i = 0; i < dice.Length; i++)
        {
            dicePoint[i] = Random.Range(1, 7);
        }
        //�Ƨ��I��
        for (int i = 0; i < dicePoint.Length; i++)
        {
            for (int j = 0; j < dicePoint.Length; ++j)
            {
                if (dicePoint[i] < dicePoint[j])
                {
                    int tmp = dicePoint[i];
                    dicePoint[i] = dicePoint[j];
                    dicePoint[j] = tmp;
                }
            }
        }

        //��s��l�Ϥ�
        DiceChanged();

        //�˴���l
        Judge();

    }

    /// <summary>
    /// �˴���l
    /// </summary>
    private void Judge()
    {
        int repeat = 0; // ���Ʀ���
        int straight = 0; // ���W����
        for (int i = 0; i < dice.Length; i++)
        {
            for (int j = i + 1; j < dice.Length; j++)
            {
                if (dicePoint[i] == dicePoint[j])
                    repeat++;
                if (dicePoint[i] + 1 == dicePoint[j])
                    straight++;
            }
        }

        if (repeat == 1)//�@��
            text[0].SetActive(true);
        else if (repeat == dice.Length)//�\�l
            text[1].SetActive(true);
        else if (straight == dice.Length - 1)//���l
            text[2].SetActive(true);

        int total = 0;
        for (int i = 0; i < dice.Length; i++)
        {
            total += dicePoint[i];
        }
        point.text = total.ToString() + " �I";
        
    }

    /// <summary>
    /// �󴫻�l
    /// </summary>
    private void DiceChanged()
    {
        sr0.sprite = diceSprite[dicePoint[0] - 1];
        sr1.sprite = diceSprite[dicePoint[1] - 1];
        sr2.sprite = diceSprite[dicePoint[2] - 1];
    }
}

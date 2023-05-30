using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    [Header("骰子物件")]
    [SerializeField] GameObject[] dice;
    [SerializeField] Sprite[] diceSprite;
    [Header("骰子點數")]
    [SerializeField] int[] dicePoint;
    [Header("文字物件")]
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
    /// 擲骰子
    /// </summary>
    public void Roll()
    {
        //關閉文字物件
        for (int i = 0; i < text.Length; i++) 
        {
            text[i].SetActive(false);
        }

        //生成點數
        for (int i = 0; i < dice.Length; i++)
        {
            dicePoint[i] = Random.Range(1, 7);
        }
        //排序點數
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

        //更新骰子圖片
        DiceChanged();

        //檢測骰子
        Judge();

    }

    /// <summary>
    /// 檢測骰子
    /// </summary>
    private void Judge()
    {
        int repeat = 0; // 重複次數
        int straight = 0; // 遞增次數
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

        if (repeat == 1)//一對
            text[0].SetActive(true);
        else if (repeat == dice.Length)//豹子
            text[1].SetActive(true);
        else if (straight == dice.Length - 1)//順子
            text[2].SetActive(true);

        int total = 0;
        for (int i = 0; i < dice.Length; i++)
        {
            total += dicePoint[i];
        }
        point.text = total.ToString() + " 點";
        
    }

    /// <summary>
    /// 更換骰子
    /// </summary>
    private void DiceChanged()
    {
        sr0.sprite = diceSprite[dicePoint[0] - 1];
        sr1.sprite = diceSprite[dicePoint[1] - 1];
        sr2.sprite = diceSprite[dicePoint[2] - 1];
    }
}

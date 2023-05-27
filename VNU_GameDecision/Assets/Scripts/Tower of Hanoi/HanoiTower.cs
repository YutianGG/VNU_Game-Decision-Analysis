using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiTower : MonoBehaviour
{
    //設定單例模式
    public static HanoiTower instance = null;

    [Header("遊戲難度")]
    [SerializeField] int level = 3;
    [Header("盤子物件")]
    [SerializeField] GameObject[] disk;
    [Header("盤子位置")]
    [SerializeField] int[] diskNum;
      
    //移動步驟，false選擇、true移動
    public bool canSelect = true;

    private void Awake()
    {
        //初始化單例模式
        instance = this;
    }
    private void Start()
    {
        //放置盤子
        PutDisk(level);
    }

    //遊戲通關文字
    [SerializeField] GameObject gameClear;
    private void Update()
    {
        if(GameClear())
        {
            //顯示通關文字
            gameClear.SetActive(true);
        }
    }

    //選中竿子編號
    int numSelect;
    //選中盤子號碼
    int numDiskSelect;
    /// <summary>
    /// 選擇竿子
    /// </summary>
    /// <param name="num">被選擇的竿子編號</param>
    public void PostSelect(int num)
    {
        //Debug.Log("Select");

        //更新被選中竿子編號
        numSelect = num;
        for (int i = 0; i < level; i++)
        {
            if (diskNum[i] == num)
            {
                //選擇的竿子的第一個盤子向上移動
                disk[i].transform.Translate(new Vector2(0, 1));
                //更新選中盤子號碼
                numDiskSelect = i;
                //轉換為移動模式
                canSelect = false;
                break;
            }            
        }    
    }

    /// <summary>
    /// 移動盤子
    /// </summary>
    /// <param name="num">目標竿子序號</param>
    public void MoveDisk(int num)
    {
        //Debug.Log("Move");

        if(CheckDisk(num))
        {
            //目標X軸位置
            float targetX = 0;
            switch (num)
            {
                case 0:
                    targetX = -4f;
                    break;

                case 1:
                    targetX = 0f;
                    break;

                case 2:
                    targetX = 4f;
                    break;
            }

            //目標Y軸位置
            float targetY = -2.3f;

            //選擇同一竿子時候不能算自己
            if (numSelect == num)
                targetY -= 0.5f;

            //計算目標竿子已放置幾個盤子
            for (int i = 0; i < level; i++)
            {   
                if (diskNum[i] == num)
                {
                    targetY += 0.5f;
                }
            } 

            //移動至目標位置
            disk[numDiskSelect].transform.position =
                new Vector2(targetX, targetY);

            //更新盤子位置
            diskNum[numDiskSelect] = num;

            //轉換為選擇模式
            canSelect = true;
        }
    }

    /// <summary>
    /// 檢測下方盤子大小
    /// </summary>
    private bool CheckDisk(int num)
    {
        for (int i = 0; i < numDiskSelect; i++)
        {
            if (diskNum[i] == num)
            {
                Debug.Log("不可放置");
                return false;
            }               
        }
        return true;
    }

    /// <summary>
    /// 開局放置盤子並設定
    /// </summary>
    /// <param name="num">盤子數量</param>
    private void PutDisk(int num)
    {
        //Debug.Log("Put");

        //隱藏所有盤子
        for (int i = 0; i < disk.Length; i++)
            disk[i].SetActive(false);

        //由大至小每個盤子依序排列
        for (int i = 1; i <= num; i++)
        {
            //顯示盤子
            disk[num - i].SetActive(true);

            //放到第一個竿子
            disk[num - i].transform.position =
                new Vector2(-4, -2.8f + (i * 0.5f));
            
            //紀錄目前盤子位置
            diskNum[num - i] = 0;
        }
        
    }

    /// <summary>
    /// 遊戲完成檢查
    /// </summary>
    private bool GameClear()
    {
        //紀算有幾個盤子在終點竿子
        int num = 0;
        for (int i = 0; i < level; i++)
        {
            if (diskNum[i] == 2)
            {
                num++;
            }
        }
        if (num == level)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 重新開始
    /// </summary>
    public void Restart()
    {
        //隱藏通關文字
        gameClear.SetActive(false);
        PutDisk(level);
    }
    /// <summary>
    /// 選擇難度
    /// </summary>
    /// <param name="num"></param>
    public void LevelChange(int num)
    {

        //隱藏通關文字
        gameClear.SetActive(false);
        //更新遊戲難度
        level = num;
        PutDisk(level);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HanoiTower : MonoBehaviour
{
    //�]�w��ҼҦ�
    public static HanoiTower instance = null;

    [Header("�C������")]
    [SerializeField] int level = 3;
    [Header("�L�l����")]
    [SerializeField] GameObject[] disk;
    [Header("�L�l��m")]
    [SerializeField] int[] diskNum;
      
    //���ʨB�J�Afalse��ܡBtrue����
    public bool canSelect = true;

    private void Awake()
    {
        //��l�Ƴ�ҼҦ�
        instance = this;
    }
    private void Start()
    {
        //��m�L�l
        PutDisk(level);
    }

    //�C���q����r
    [SerializeField] GameObject gameClear;
    private void Update()
    {
        if(GameClear())
        {
            //��ܳq����r
            gameClear.SetActive(true);
        }
    }

    //�襤��l�s��
    int numSelect;
    //�襤�L�l���X
    int numDiskSelect;
    /// <summary>
    /// ��ܬ�l
    /// </summary>
    /// <param name="num">�Q��ܪ���l�s��</param>
    public void PostSelect(int num)
    {
        //Debug.Log("Select");

        //��s�Q�襤��l�s��
        numSelect = num;
        for (int i = 0; i < level; i++)
        {
            if (diskNum[i] == num)
            {
                //��ܪ���l���Ĥ@�ӽL�l�V�W����
                disk[i].transform.Translate(new Vector2(0, 1));
                //��s�襤�L�l���X
                numDiskSelect = i;
                //�ഫ�����ʼҦ�
                canSelect = false;
                break;
            }            
        }    
    }

    /// <summary>
    /// ���ʽL�l
    /// </summary>
    /// <param name="num">�ؼЬ�l�Ǹ�</param>
    public void MoveDisk(int num)
    {
        //Debug.Log("Move");

        if(CheckDisk(num))
        {
            //�ؼ�X�b��m
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

            //�ؼ�Y�b��m
            float targetY = -2.3f;

            //��ܦP�@��l�ɭԤ����ۤv
            if (numSelect == num)
                targetY -= 0.5f;

            //�p��ؼЬ�l�w��m�X�ӽL�l
            for (int i = 0; i < level; i++)
            {   
                if (diskNum[i] == num)
                {
                    targetY += 0.5f;
                }
            } 

            //���ʦܥؼЦ�m
            disk[numDiskSelect].transform.position =
                new Vector2(targetX, targetY);

            //��s�L�l��m
            diskNum[numDiskSelect] = num;

            //�ഫ����ܼҦ�
            canSelect = true;
        }
    }

    /// <summary>
    /// �˴��U��L�l�j�p
    /// </summary>
    private bool CheckDisk(int num)
    {
        for (int i = 0; i < numDiskSelect; i++)
        {
            if (diskNum[i] == num)
            {
                Debug.Log("���i��m");
                return false;
            }               
        }
        return true;
    }

    /// <summary>
    /// �}����m�L�l�ó]�w
    /// </summary>
    /// <param name="num">�L�l�ƶq</param>
    private void PutDisk(int num)
    {
        //Debug.Log("Put");

        //���éҦ��L�l
        for (int i = 0; i < disk.Length; i++)
            disk[i].SetActive(false);

        //�Ѥj�ܤp�C�ӽL�l�̧ǱƦC
        for (int i = 1; i <= num; i++)
        {
            //��ܽL�l
            disk[num - i].SetActive(true);

            //���Ĥ@�Ӭ�l
            disk[num - i].transform.position =
                new Vector2(-4, -2.8f + (i * 0.5f));
            
            //�����ثe�L�l��m
            diskNum[num - i] = 0;
        }
        
    }

    /// <summary>
    /// �C�������ˬd
    /// </summary>
    private bool GameClear()
    {
        //���⦳�X�ӽL�l�b���I��l
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
    /// ���s�}�l
    /// </summary>
    public void Restart()
    {
        //���óq����r
        gameClear.SetActive(false);
        PutDisk(level);
    }
    /// <summary>
    /// �������
    /// </summary>
    /// <param name="num"></param>
    public void LevelChange(int num)
    {

        //���óq����r
        gameClear.SetActive(false);
        //��s�C������
        level = num;
        PutDisk(level);
    }



}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    //��l�Ǹ�
    [SerializeField] int num;

    private void OnMouseDown()
    {
        //��ܬ�l
        if (HanoiTower.instance.canSelect)
        {
            HanoiTower.instance.PostSelect(num);
        }
        //���ʽL�l
        else
        {
            HanoiTower.instance.MoveDisk(num);
        }
    }
}

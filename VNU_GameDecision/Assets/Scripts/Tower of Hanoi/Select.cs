using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Select : MonoBehaviour
{
    //竿子序號
    [SerializeField] int num;

    private void OnMouseDown()
    {
        //選擇竿子
        if (HanoiTower.instance.canSelect)
        {
            HanoiTower.instance.PostSelect(num);
        }
        //移動盤子
        else
        {
            HanoiTower.instance.MoveDisk(num);
        }
    }
}

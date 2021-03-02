using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
        ボタンオブジェクト（演出用）スクリプト
*/
public class ButtonScript : MonoBehaviour
{
    //コンポーネント//
    [SerializeField]private Animator anim = null;//アニメーション
    //ボタンを押す機能
    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            anim.SetBool("isPush",true);
        }else if(Input.GetKeyUp(KeyCode.Return)){
            anim.SetBool("isPush",false);
        }
    }
}

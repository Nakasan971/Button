using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
        電球オブジェクト（演出用）スクリプト
*/
public class LightingScript : MonoBehaviour
{
    private bool isLighting;
    [SerializeField]private Animator anim = null;//アニメーション
    //ボタンを押す機能
    void Update(){
        if(Input.GetKeyDown(KeyCode.Return)){
            isLighting = !isLighting;
            SetLight();
        }
    }
    void SetLight(){
        if(isLighting){
            anim.SetBool("isLighting",true);
        }else{
           anim.SetBool("isLighting",false); 
        }
    }
}

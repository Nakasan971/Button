using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
        クロックオブジェクト（演出用）スクリプト
*/
public class NeedleScript : MonoBehaviour{   
    private float rotateSpeed = 30.0f;//針の回るスピード
    void Update(){
        transform.Rotate(0,0,-rotateSpeed * Time.deltaTime);//回転
    }
}

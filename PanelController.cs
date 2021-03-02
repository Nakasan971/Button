using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
        パネルオブジェクト（演出用）スクリプト
*/
public class PanelController : MonoBehaviour{
    //プライベート//
    [SerializeField]private Image img = default;      //イメージUI
    [SerializeField]private Text chapText = default;  //チャプター文字
    [SerializeField]private Animator panelAnim = null;//アニメーション
    //他スクリプト//
    [SerializeField] private EventManager event_Man = default;//イベント管理同期
    void Start(){
        img.enabled = true;//イメージ表示
    }
    //シーン初期のチャプター表示を消す（アニメーションイベント）
    void inDispChapter(){
        Destroy(chapText);
        event_Man.start = true;
    }
    //フェードアウト
    public void EndChapter() {
        event_Man.start = false;
        panelAnim.SetBool("isFadeOut",true);
    }
    //EventManagerスクリプト経由でシーン移動（アニメーションイベント）
    void SceneMove(){
        event_Man.SceneMove();
    }
}

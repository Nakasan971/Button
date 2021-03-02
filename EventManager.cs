using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/*
        全体管理スクリプト
*/
public class EventManager : MonoBehaviour
{
    //パブリック//
    public int currentPage = 0;//進行中のシーンのページ（進行状況確認）
    public int impatient = 0;  //せっかち数
    public int select = 0;     //ポーズ画面の選択肢
    public bool start;         //始めていいか？
    //プライベート//
    private bool activePause;//ポーズ画面中か？
    private bool wantExit;   //ゲームをやめたいか？
    private Vector3[] ArrowPos = {
        new Vector3(-2,1f,-1f),
        new Vector3(-2f,-1f,-1f)
    };
    //シリアライズ//
    [SerializeField] private bool titleScene        = default;//今はタイトルシーンか？
    [SerializeField] private GameObject pausePanel  = default;//ポーズ画面
    [SerializeField] private GameObject selectArrow = default;//ポーズ画面の矢印
    //他スクリプト//
    [SerializeField] private TextManager message    = default;//メッセージ文同期
    [SerializeField] private GimmickScript gimmick  = default;//ギミック同期
    [SerializeField] private ScenePicker pickScene  = default;//シーン移動同期

    void Start(){
        if(titleScene){ //タイトル画面なら
            start = true;
        }else{
            pausePanel.SetActive(false);
        }
    }

    void Update(){
        if(start){
            if(Input.GetKeyDown(KeyCode.Return)){     //Enterを押した時
                PushEnter();
            }
            if(Input.GetKeyDown(KeyCode.Backspace)){  //Backspaceを押した時
                pausePanel.SetActive(true);
                activePause = true;
            }
            if(activePause){    //ポーズ中なら
                Pause();
            }
        }
    }
    //押された時の挙動
    void PushEnter(){
        if(titleScene){                  //タイトルシーンの時
            SceneMove();
        }else if(activePause && wantExit){
            Exit();
        }else if(activePause && !wantExit){
            pausePanel.SetActive(false);
            activePause = false;
        }else{
            if(message.isCompleteMessage && !gimmick.isMove){
                message.NextPage();      //テキスト更新
                gimmick.CheckGimmick(currentPage);//イベント機能
            }else if(!gimmick.isMove){
                message.DispAllMessage();//テキスト全表示
            }
        }
    }
    //シーン移動
    public void SceneMove(){
        pickScene.ToScene();
    }
    //ポーズ機能
    void Pause(){
        //カーソル制御
        if(Input.GetKeyDown(KeyCode.UpArrow) && 0 < select){
            select--;
            wantExit = false;
        }else if(Input.GetKeyDown(KeyCode.DownArrow) && select < ArrowPos.Length - 1){
            select++;
            wantExit = true;
        }
        selectArrow.transform.position = ArrowPos[select];
    }
    //ゲーム終了
    public void Exit(){
        #if UNITY_EDITOR    //エディタに依存
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
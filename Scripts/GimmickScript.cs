using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
        ギミックスクリプト
*/
public class GimmickScript : MonoBehaviour
{
    //パブリック//
    public bool isMove;       //moveObject()が機能中か？
    //プライベート//
    private int gimmickPage;  //機能すべきギミックの行
    private int objectId;     //格納したオブジェクトのID
    private float [] arr;     //抽出したコマンドを扱いやすくする
    private float moveSpeed;  //moveObject()の移動スピード
    private string textData;  //テキストファイルのテキストそのもの
    private Vector3 targetPos;//moveObject()の移動先
    private List<string> events = new List<string>();//イベント格納
    //シリアライズ//
    [SerializeField] private string fileName           = default;//テキストファイル参照
    [SerializeField] private Text message              = default;//メッセージText参照
    [SerializeField] private List<GameObject> objects  = default;//オブジェクト格納
    //他スクリプト//
    [SerializeField] private EventManager event_Man = default;　　//イベント管理同期
    [SerializeField] private PanelController panel_Con = default;//オブジェクト格納

    void Start(){
        ReadFile();
    }

    void Update(){
        if(isMove){
            moveObject();
        }
    }
    //ギミック制御
    public void CheckGimmick(int checkPage){
        try{
            string [] eventId = events[gimmickPage].Split(' ');//どの行のコマンドか
            arr = Array.ConvertAll(eventId,float.Parse);     　//扱いやすく変換
            if(arr[0] == checkPage-1){
                WhichGimmick();
            }
        }catch{
            Debug.Log("Out of Index");
        }
    }
    //テキストファイル読み込み
    void ReadFile(){
        //ファイル読み込み
        using (StreamReader reader = new StreamReader(Application.streamingAssetsPath +"/"+fileName)) {
            //テキスト内容の最後まで
            while((textData = reader.ReadLine()) != null) {
                events.Add(textData);    //リストに追加
            }
        }
    }
    //指定オブジェクトを指定先まで移動させる
    void moveObject(){
        objects[objectId].transform.position =
        Vector3.MoveTowards (objects[objectId].transform.position, targetPos, arr[6] * Time.deltaTime);
        if(objects[objectId].transform.position == targetPos){//到着したら
            isMove = false;
        }
    }
    //ギミック一覧
    void WhichGimmick(){
        switch(arr[1]){
            case 1:                                             //オブジェクト移動
                objectId = (int)arr[2];
                isMove = true;
                targetPos = new Vector3(arr[3],arr[4],arr[5]);
                break;
            case 2:                                             //シーン移動
                panel_Con.EndChapter();
                break;
            case 3:                                             //フォントサイズ変更
                message.fontSize = (int)arr[2];
                break;
            case 4:                                             //フォントサイズをデフォに
                message.fontSize = 35;
                break;
            case 5:
                message.color = new Color(arr[2],arr[3],arr[4],arr[5]); //テキストカラーの変更（RGBA）
                break;
            case 6:
                message.color = new Color(1.0f,1.0f,1.0f,1.0f);//テキストカラーを白へ
                break;
            case 7:
                event_Man.Exit();                              //ゲームの強制終了（LastChapter用）
                break;
            default :
                break;
        }
        gimmickPage++;                                          //参照する行を更新
    }
}

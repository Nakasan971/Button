using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*
        テキスト・メッセージ用スクリプト
*/
public class TextManager : MonoBehaviour
{
    //プライベート//
    private int pageNum = 0;             //参照するメッセージの行           
    private int checkNum = 0;            //表示すべきメッセージの行
    private int updateChara = -1;        //表示中の文字数
    private float timeLapsed = 1;        //テキスト表示の開始時間
    private float timeUntil = 0;         //表示にかかる時間
    private float intervalChara = 0.05f; //1文字の表示にかかる時間
    private string textData = " ";       //テキストファイルのテキストそのもの
    private string currentMessage = string.Empty;       //現在のテキスト文 
    private List<string> scenarios = new List<string>();//テキスト格納
    //シリアリズ//
    [SerializeField] private string fileName        = default;//テキストファイル参照
    [SerializeField] private Text messageText       = default;//文章Textオブジェクト
    [SerializeField] private Text countText         = default;//カウントTextオブジェクト
    [SerializeField] private Text impatientText     = default;//せっかちTextオブジェクト
    [SerializeField] private GameObject arrowIcon   = default;//送りアイコンオブジェクト
    //他スクリプト//
    [SerializeField] private EventManager event_Man = default;//イベント管理同期
    [SerializeField] private AudioManager audio_Man = default;//SE管理同期
    //全ての文字を表示し終えたか？
    public bool isCompleteMessage{
        get{return Time.time > timeLapsed + timeUntil;}
    }
    void Awake(){
        ReadFile();
        NextPage();
    }
    //テキストを１文字ずつ表示
    void Update(){
        // クリックから経過した時間が想定表示時間の何%か確認し、表示文字数を出す
		int charaCount = (int)(Mathf.Clamp01((Time.time - timeLapsed) / timeUntil) * currentMessage.Length);
		if(charaCount != updateChara ){ //表示文字数の比較
		    messageText.text = currentMessage.Substring(0,charaCount);//テキスト更新
		    updateChara = charaCount;   //文字数をキャッシュ
            audio_Man.OneShot();        //セリフ音
		}else if(isCompleteMessage && currentMessage.Length != 0){
            arrowIcon.SetActive(true);  //送りアイコン表示
        }
    }
    //テキスト処理
    public void NextPage(){
        arrowIcon.SetActive(false);//送りアイコン非表示
        try{
            countText.text = event_Man.currentPage.ToString();　//カウント数更新
            string[] scenarioId = scenarios[pageNum].Split(' ');//空白で文字列を分割
            checkNum = int.Parse(scenarioId[0]);                //表示行の取得
            if(checkNum == event_Man.currentPage){
                currentMessage = scenarioId[1];                 //テキスト取得
                timeUntil = currentMessage.Length * intervalChara;//想定表示時間と現在時刻をキャッシュ
		        timeLapsed = Time.time;
		        updateChara = -1;                               //文字カウントを初期化
                pageNum++;                                      //次の行を予約
            }
            event_Man.currentPage++;                          　//次ページの予約
        }catch{
            event_Man.currentPage++;                            //次ページの予約(例外でも)
            Debug.Log("Page Size OverFlow");    　　　           //ページサイズを超える例外
        }
    }
    //表示途中のテキスト全文を表示
    public void DispAllMessage(){
        timeUntil = 0;          //表示にかかる時間を０に
        event_Man.impatient++;  //せっかち度上昇
        impatientText.text = event_Man.impatient.ToString();//せっかち数更新
    }
    //テキストファイル読み込み
    void ReadFile(){
        //ファイル読み込み
        using (StreamReader reader = new StreamReader(Application.streamingAssetsPath +"/"+ fileName)) {
            //テキスト内容の最後まで（i行ずつ）
            while((textData = reader.ReadLine()) != null) {
                scenarios.Add(textData);    //リストに追加
            }
        }
    }
}
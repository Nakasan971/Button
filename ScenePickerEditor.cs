using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(ScenePicker), true)] //エディタ制御表示
public class ScenePickerEditor : Editor{
    //オーバーライド
    public override void OnInspectorGUI(){
        //プロパティ定義 as クラス
        var picker = target as ScenePicker;
        //シリアライズオブジェクト代入
        var oldScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(picker.scenePath);
        //値を最新なものにする
        serializedObject.Update();
        //インスペクター上で変更があったか？
        EditorGUI.BeginChangeCheck();
        //指定した型の表示フィールド作成
        var newScene = EditorGUILayout.ObjectField("scene", oldScene, typeof(SceneAsset), false) as SceneAsset;
        //変更があった場合
        if (EditorGUI.EndChangeCheck()){
            //フィールド内のシーンパスを取得（オブジェクト）
            var newPath = AssetDatabase.GetAssetPath(newScene);
            //シリアライズオブジェクトのプロパティ取得(シーンプロパティ)
            var scenePathProperty = serializedObject.FindProperty("scenePath");
            //値にシーンパスを代入
            scenePathProperty.stringValue = newPath;
        }
        //プロパティの変更を適応
        serializedObject.ApplyModifiedProperties();
    }
}
#endif

/*----------Library-----------
7 :[CustomEditor(typeof(型),bool)]...
    [参照する型、子クラスも表示するか？] クラス名から型宣言を取得
 L_GetType()...型情報取得
10:OnInspectorGUI()...
    カスタムインスペクター作成用関数
    オーバーライド前提の機能？
 L_ovarride...親クラス内関数の動作を上書きする
12:target...
    指定オブジェクトのプロパティ
14:AssetDatabase.LoadAssetAtPath<型>(assetPath)...
    指定のパスと型である最初のアセットオブジェクトを返す
 L_SceneAsset...ObjectField UI 要素の型として使用
 L_.scenePath...シリアライズオブジェクトの変数
16:serializedObject.Update()...
    シリアライズされたオブジェクトの形式を更新
 L_serializedObject...シリアライズ化されたフィールドを扱う機能
18:EditorGUI.BeginChangeCheck()...
    EditorGUI.EndChangeCheck()とペアで使う
    囲われたブロック内の変更を感知する
 L_GUI.changed...入力データの変更を感知して返す(bool)
 L_EditorGUI...エディタ向けに機能が追加されたGUIクラス
20:EditorGUILayout.ObjectField(string,Object,型,bool)...
    [ラベル,指定オブジェクト,指定オブジェクトの型,割り当てを許可するか？]
    任意のオブジェクトのTypeを表示するフィールドを作成
22:EditorGUI.EndChangeCheck()...
    17:参照
24:AssetDatabase.GetAssetPath(assetPath)...
    アセットが保存されているプロジェクトフォルダーのパス名を返す。
26:serializedObject.FindProperty(string)...
    [プロパティのフルパス]ターゲットのオブジェクトで特定のプロパティーを検索するために使用
28:scenePathProperty.stringValue...
    文字列プロパティの値
30:serializedObject.ApplyModifiedProperties()...
    プロパティーの変更を適用
----------------------------*/
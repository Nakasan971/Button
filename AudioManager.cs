using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
        オーディオ関係スクリプト
*/
public class AudioManager : MonoBehaviour{
    //コンポーネント//
    [SerializeField] private AudioClip sound = default;
    [SerializeField] private AudioSource audio_Src = default;
    public void OneShot(){
        audio_Src.PlayOneShot(sound);
    }
}

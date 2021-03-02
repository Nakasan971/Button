using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdScript : MonoBehaviour
{
    int action;
    int walkSpeed;
    float total;
    Vector3 targetPos;
    [SerializeField] private List<float> actionList = default; 
    [SerializeField]private SpriteRenderer sprite = default;
    [SerializeField]private Animator anim = null;
    void Start(){
        transform.position = new Vector3(Random.Range(-5.0f,5.0f),Random.Range(-4.0f,0.0f),-1.0f);
        InitializeList();
        StartCoroutine(Movement()); 
    }
    void InitializeList(){
        foreach(float elem in actionList){
            total += elem;
        }
    }
    int SelectAction(){
        float random = Random.value * total;
        for(int i = 0;i < actionList.Count;i++){
            if(random < actionList[i]){
                return i;
            }else{
                random -= actionList[i];
            }
        }
        return 0;
    }
    IEnumerator Movement(){
        while(true){
            action = SelectAction();
            switch(action){
                case 1:
                    walkSpeed = Random.Range(1,5);
                    targetPos = new Vector3(Random.Range(-5.0f,5.0f),Random.Range(-4.0f,0.0f),-1.0f);
                    float distance = targetPos.x - transform.position.x;
                    if(distance < 0)sprite.flipX = true;
                    else sprite.flipX = false;
                    StartCoroutine(Walking(targetPos));
                    break;
                case 2:
                    StartCoroutine(Eating());
                    break;
                case 3:
                    StartCoroutine(Sleeping());
                    break;
                default:
                    break;
            }
            yield return new WaitForSeconds(Random.Range(10,15));
        }
    }
    IEnumerator Sleeping(){
        anim.SetBool("isSleep",true);
        yield return new WaitForSeconds(10);
        anim.SetBool("isSleep",false);
        StopCoroutine(Sleeping()); 
    }
    IEnumerator Eating(){
        anim.SetBool("isEat",true);
        yield return new WaitForSeconds(1);
        anim.SetBool("isEat",false);
        StopCoroutine(Eating());
    }
    IEnumerator Walking(Vector3 targetPos){
        anim.SetBool("isWalk",true);
        while(transform.position != targetPos){
            transform.position = Vector3.MoveTowards(transform.position, targetPos, walkSpeed * Time.deltaTime);
            yield return null;
        }
        anim.SetBool("isWalk",false);
        StopCoroutine(Walking(targetPos));
    }
}

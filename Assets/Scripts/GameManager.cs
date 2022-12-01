using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public StageManager stageManager;
    int currentStage;
    public float cleartime;
    public Text textStageNumber;

    public GameObject clearPanel;
    public GameObject fnishPanel;


    void Start()
    {
        //ステージ数ロード
        currentStage = ES3.Load("Stagekey", currentStage);
        //ステージ数表示
        textStageNumber.text = "" + currentStage;

        stageManager.LoadStageFromText(currentStage);
        stageManager.CreateStage();
        stageManager.stageClear += Cleared;
        clearPanel.SetActive(false);

        StageZero();
    }

    public void Cleared()
    {
        StartCoroutine(ClearStage());
    }

    IEnumerator ClearStage()
    {
        clearPanel.SetActive(true);
        yield return new WaitForSeconds(cleartime);
        currentStage++;
        if (currentStage >= stageManager.stageFiles.Length)
        {
            SceneManager.LoadScene("Clear");
            yield return null;
        }
        stageManager.DestroyStage();
        stageManager.LoadStageFromText(currentStage);
        stageManager.CreateStage();
        clearPanel.SetActive(false);
        StageNumber();
        StageFinish();
    }

    public void OnResetButton()
    {
        stageManager.DestroyStage();
        stageManager.CreateStage();
    }

    public void StageNumber()
    {
        textStageNumber.text = currentStage + "";
        //ステージ数セーブ
        ES3.Save<int>("Stagekey", currentStage);
    }

    //最後のステージクリアしたら
    public void StageFinish()
    {
        if (currentStage == 5) //最終ステージの次の数字にする
        {
            //Debug.Log("ステージ5");
            fnishPanel.SetActive(true);
        }      
    }

    //ステージ0を保存しておく
    public void StageZero()
    {
        if (currentStage == 0)
        {
            Debug.Log("ステージ0");
        }
    }

}

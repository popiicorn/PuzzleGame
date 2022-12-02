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
        Invoke("StageFinish", 0.15f);
        //StageFinish();

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

    }

    public void OnResetButton()
    {
        stageManager.DestroyStage();
        stageManager.CreateStage();
    }

    //リスタートさせる
    public void OnRestartButton()
    {
        stageManager.DestroyStage();

        //ステージ0をロード
        currentStage = ES3.Load("StageZerokey", currentStage);

        //ステージ数表示
        textStageNumber.text = "" + currentStage;

        //ステージ0に置き換える
        stageManager.LoadStageFromText(currentStage);
        stageManager.CreateStage();

        Invoke("FnishPanel", 0.5f);
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
        if (currentStage == 100) //最終ステージの数字にする
        {
            fnishPanel.SetActive(true);
        }      
    }

    //ステージ0を保存しておく
    public void StageZero()
    {
        if (currentStage == 0)
        {
            ES3.Save<int>("StageZerokey", currentStage);
        }
    }

    //ステージ0を保存しておく
    public void FnishPanel()
    {
        fnishPanel.SetActive(false);
    }

}

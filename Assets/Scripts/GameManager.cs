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
        //�X�e�[�W�����[�h
        currentStage = ES3.Load("Stagekey", currentStage);
        //�X�e�[�W���\��
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

    //���X�^�[�g������
    public void OnRestartButton()
    {
        stageManager.DestroyStage();

        //�X�e�[�W0�����[�h
        currentStage = ES3.Load("StageZerokey", currentStage);

        //�X�e�[�W���\��
        textStageNumber.text = "" + currentStage;

        //�X�e�[�W0�ɒu��������
        stageManager.LoadStageFromText(currentStage);
        stageManager.CreateStage();

        Invoke("FnishPanel", 0.5f);
    }

    public void StageNumber()
    {
        textStageNumber.text = currentStage + "";
        //�X�e�[�W���Z�[�u
        ES3.Save<int>("Stagekey", currentStage);
    }

    //�Ō�̃X�e�[�W�N���A������
    public void StageFinish()
    {
        if (currentStage == 100) //�ŏI�X�e�[�W�̐����ɂ���
        {
            fnishPanel.SetActive(true);
        }      
    }

    //�X�e�[�W0��ۑ����Ă���
    public void StageZero()
    {
        if (currentStage == 0)
        {
            ES3.Save<int>("StageZerokey", currentStage);
        }
    }

    //�X�e�[�W0��ۑ����Ă���
    public void FnishPanel()
    {
        fnishPanel.SetActive(false);
    }

}

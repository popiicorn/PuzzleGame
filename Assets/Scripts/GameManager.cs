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
    }

    public void OnResetButton()
    {
        stageManager.DestroyStage();
        stageManager.CreateStage();
    }

    public void StageNumber()
    {
        textStageNumber.text = currentStage + "";
        //�X�e�[�W���Z�[�u
        ES3.Save<int>("Stagekey", currentStage);
    }

}

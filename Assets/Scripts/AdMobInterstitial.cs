using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdMobInterstitial : MonoBehaviour
{
    //��邱��
    //1.�C���^�[�X�e�B�V�����L��ID�̓���
    //2.�C���^�[�X�e�B�V�����N���ݒ�@ShowAdMobInterstitial()���g��

    private InterstitialAd interstitial;//InterstitialAd�^�̕ϐ�interstitial��錾�@���̒��ɃC���^�[�X�e�B�V�����L���̏�񂪓���

    private void Start()
    {
        RequestInterstitial();
        Debug.Log("�ǂݍ��݊J�n");
    }

    //�C���^�[�X�e�B�V�����L����\������֐�
    //�{�^���ȂǂɊ��t�����Ďg�p
    public void ShowAdMobInterstitial()
    {
        //�L���̓ǂݍ��݂��������Ă�����L���\��
        if (interstitial.IsLoaded() == true)
        {
            interstitial.Show();
            Debug.Log("�L���\��");
        }
        else
        {
            Debug.Log("�L���ǂݍ��ݖ�����");
        }
    }


    //�C���^�[�X�e�B�V�����L����ǂݍ��ފ֐�
    private void RequestInterstitial()
    {
        //Android��iOS�ōL��ID���Ⴄ�̂Ńv���b�g�t�H�[���ŏ����𕪂��܂��B
        // �Q�l
        //�yUnity�zAndroid��iOS�ŏ����𕪂�����@
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6736870106967218/4803565733";//������Android�̃C���^�[�X�e�B�V�����L��ID�����

#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-6736870106967218/1034004821";//������iOS�̃C���^�[�X�e�B�V�����L��ID�����

#else
        string adUnitId = "unexpected_platform";
#endif

        //�C���^�[�X�e�B�V�����L��������
        interstitial = new InterstitialAd(adUnitId);


        //InterstitialAd�^�̕ϐ� interstitial�̊e���� �Ɋ֐���o�^
        interstitial.OnAdLoaded += HandleOnAdLoaded;//interstitial�̏�Ԃ��@�C���^�[�X�e�B�V�����ǂݍ��݊����@�ƂȂ������ɋN������֐�(�֐���HandleOnAdLoaded)��o�^
        interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;//interstitial�̏�Ԃ��@�C���^�[�X�e�B�V�����ǂݍ��ݎ��s �@�ƂȂ������ɋN������֐�(�֐���HandleOnAdFailedToLoad)��o�^
        interstitial.OnAdClosed += HandleOnAdClosed;//interstitial�̏�Ԃ�  �C���^�[�X�e�B�V�����\���I���@�ƂȂ������ɋN������֐�(HandleOnAdClosed)��o�^


        //���N�G�X�g�𐶐�
        AdRequest request = new AdRequest.Builder().Build();
        //�C���^�[�X�e�B�V�����Ƀ��N�G�X�g�����[�h
        interstitial.LoadAd(request);
    }

    //�C���^�[�X�e�B�V�����ǂݍ��݊��� �ƂȂ������ɋN������֐�
    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("�C���^�[�X�e�B�V�����ǂݍ��݊���");
    }

    //�C���^�[�X�e�B�V�����ǂݍ��ݎ��s �ƂȂ������ɋN������֐�
    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("�C���^�[�X�e�B�V�����ǂݍ��ݎ��s" + args.LoadAdError);//args.LoadAdError:�G���[���e 
    }


    //�C���^�[�X�e�B�V�����\���I�� �ƂȂ������ɋN������֐�
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        Debug.Log("�C���^�[�X�e�B�V�����L���I��");

        //�C���^�[�X�e�B�V�����L���͎g���̂ĂȂ̂ň�U�j��
        interstitial.Destroy();

        //�C���^�[�X�e�B�V�����ēǂݍ��݊J�n
        RequestInterstitial();
        Debug.Log("�C���^�[�X�e�B�V�����L���ēǂݍ���");
    }

}
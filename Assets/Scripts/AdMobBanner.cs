using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdMobBanner: MonoBehaviour
{
    //��邱��
    //1.�o�i�[�L��ID�����
    //2.�o�i�[�̕\���ʒu�@(����\���ʒu�͉��ɂȂ��Ă��܂��B)
    //3.�o�i�[�\���̃^�C�~���O (���� �N������ɂȂ��Ă��܂��B)

    private BannerView bannerView;//BannerView�^�̕ϐ�bannerView��錾�@���̒��Ƀo�i�[�L���̏�񂪓���


    //�V�[���ǂݍ��ݎ�����o�i�[��\������
    //�ŏ�����o�i�[��\���������Ȃ��ꍇ�͂��̊֐��������Ă��������B
    private void Start()
    {
        RequestBanner();//�A�_�v�e�B�u�o�i�[��\������֐� �Ăяo��
    }


    //�{�^�����Ɋ���t���Ďg�p
    //�o�i�[��\������֐�
    public void BannerStart()
    {
        RequestBanner();//�A�_�v�e�B�u�o�i�[��\������֐� �Ăяo��       
    }

    //�{�^�����Ɋ���t���Ďg�p
    //�o�i�[���폜����֐�
    public void BannerDestroy()
    {
        bannerView.Destroy();//�o�i�[�폜
    }

    //�A�_�v�e�B�u�o�i�[��\������֐�
    private void RequestBanner()
    {
        //Android��iOS�ōL��ID���Ⴄ�̂Ńv���b�g�t�H�[���ŏ����𕪂��܂��B
        // �Q�l
        //�yUnity�zAndroid��iOS�ŏ����𕪂�����@
        // https://marumaro7.hatenablog.com/entry/platformsyoriwakeru

#if UNITY_ANDROID
        string adUnitId = "ca-app-pub-6736870106967218/5659398120";//������Android�̃o�i�[ID�����

#elif UNITY_IPHONE
        string adUnitId = "ca-app-pub-6736870106967218/9537017663";//������iOS�̃o�i�[ID�����

#else
        string adUnitId = "unexpected_platform";
#endif

        // �V�����L����\������O�Ƀo�i�[���폜
        if (bannerView != null)//�����ϐ�bannerView�̒��Ƀo�i�[�̏�񂪓����Ă�����
        {
            bannerView.Destroy();//�o�i�[�폜
        }

        //���݂̉�ʂ̌����������擾���o�i�[�T�C�Y������
        AdSize adaptiveSize =
                AdSize.GetCurrentOrientationAnchoredAdaptiveBannerAdSizeWithWidth(AdSize.FullWidth);


        //�o�i�[�𐶐� new BannerView(�o�i�[ID,�o�i�[�T�C�Y,�o�i�[�\���ʒu)
        bannerView = new BannerView(adUnitId, adaptiveSize, AdPosition.Top);//�o�i�[�\���ʒu��
                                                                               //��ʏ�ɕ\������ꍇ�FAdPosition.Top
                                                                               //��ʉ��ɕ\������ꍇ�FAdPosition.Bottom


        //BannerView�^�̕ϐ� bannerView�̊e���� �Ɋ֐���o�^
        bannerView.OnAdLoaded += HandleAdLoaded;//bannerView�̏�Ԃ� �o�i�[�\������ �ƂȂ������ɋN������֐�(�֐���HandleAdLoaded)��o�^
        bannerView.OnAdFailedToLoad += HandleAdFailedToLoad;//bannerView�̏�Ԃ� �o�i�[�ǂݍ��ݎ��s �ƂȂ������ɋN������֐�(�֐���HandleAdFailedToLoad)��o�^


        //���N�G�X�g�𐶐�
        AdRequest adRequest = new AdRequest.Builder().Build();

        //�L���\��
        bannerView.LoadAd(adRequest);
    }


    #region Banner callback handlers

    //�o�i�[�\������ �ƂȂ������ɋN������֐�
    public void HandleAdLoaded(object sender, EventArgs args)
    {
        Debug.Log("�o�i�[�\������");
    }

    //�o�i�[�ǂݍ��ݎ��s �ƂȂ������ɋN������֐�
    public void HandleAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        Debug.Log("�o�i�[�ǂݍ��ݎ��s" + args.LoadAdError);//args.LoadAdError:�G���[���e 
    }

    #endregion

}
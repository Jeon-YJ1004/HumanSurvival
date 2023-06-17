using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserAchi : MonoBehaviour
{
    [SerializeField] TMP_Text       mMoneyText;
    [SerializeField] TMP_Text       mAchiText;
    [SerializeField] Toggle         mCompleteHide;
    [SerializeField] GameObject[]   mAchiObject;

    private int mAchiCount = 0;
    private void Start()
    {
        SetMoneyText();
        SetCollectionText();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            GetComponent<SceneMove>().ToBack();
        }
    }

    private void SetMoneyText()
    {
        mMoneyText.text = UserInfo.instance.UserDataSet.Gold.ToString();
    }

    private void SetCollectionText()
    {
        for(int i = 0; i < Constants.MaxAchievementNumber; i++)
        {
            if(UserInfo.instance.UserDataSet.Achievements[i])
            {
                mAchiCount++;
            }
        }
        mAchiText.text = "잠금 해제됨 : " + mAchiCount.ToString() + " / " + Constants.MaxAchievementNumber;
    }

    public void CompleteHide()
    {
        if(mCompleteHide.GetComponent<Toggle>().isOn)
        {
            for(int i = 1; i <= Constants.MaxAchievementNumber; i++)
            {
                if(UserInfo.instance.UserDataSet.Achievements[i])
                {
                    mAchiObject[i - 1].SetActive(false);
                }
            }
        }
        else
        {
            for(int i = 1; i <= Constants.MaxAchievementNumber; i++)
            {
                if(UserInfo.instance.UserDataSet.Achievements[i])
                {
                    mAchiObject[i - 1].SetActive(true);
                }
            }
        }
    }
}
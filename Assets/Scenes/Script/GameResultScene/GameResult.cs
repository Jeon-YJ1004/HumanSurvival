using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameResult : MonoBehaviour
{
    //[SerializeField] TMP_Text map = null;
    [SerializeField] TMP_Text time = null;
    float gameTime;
    [SerializeField] TMP_Text coin = null;
    [SerializeField] TMP_Text level = null;
    [SerializeField] TMP_Text kill = null;
    [SerializeField] TMP_Text character = null;
    [SerializeField] Image characterImage = null;

    public GameObject Weaponlist;
    GameObject weaponInfo;

    GameObject Weaponname=null;
    GameObject Weaponlevel=null;
    GameObject Weapondamage=null;
    GameObject WeapondamagePerSec=null;

    void Start()
    {
        level.text = string.Format("{0}", GameManager.instance.level);
        kill.text = string.Format("{0}", GameManager.instance.kill);
        coin.text = string.Format("{0}", GameManager.instance.coin);
        gameTime = GameManager.instance.gameTime;
        float seconds = Mathf.Floor(gameTime % 60);
        float minutes = Mathf.Floor(gameTime / 60);
        time.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        string charName = "" + DataManager.instance.currentCharcter;
        character.text = string.Format("{0}", charName);
        string source = "CharacterSprite/" + charName + "_0";
        characterImage.sprite = Resources.Load<Sprite>(source);
        for (int i = 0; i < GameManager.instance.equipManageSys.Weapons.Count; i++)
        {
            weaponInfo = Resources.Load<GameObject>("Weapons/WeaponInfo");
            GameObject row = Instantiate(weaponInfo);
            row.GetComponent<WeaponListUI>().SetWeaponResultData(GameManager.instance.equipManageSys.Weapons[i].WeaponIndex, gameTime);
            row.transform.SetParent(Weaponlist.transform, false); 
        }
    }
}
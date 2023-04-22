using Enums;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEditor.U2D.Path;
using UnityEngine;

public class Peachone : MonoBehaviour
{
    [SerializeField] Animator animator;
    public Weapon ownWeapon;
    public Transform StartPoint = null;
    public Transform EndPoint = null;
    public Vector3 ControlPoint;
    private Vector3 mDefaultScale = new Vector3(2, 2, 1);

    float Timer = 0;
    bool UsePeach = false;
    private void Start()
    {
        ownWeapon = GetComponent<Weapon>();
        //animator = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        if (!UsePeach)
        {
            return;
        }
        Timer += Time.deltaTime;

        transform.position = calculateBezierPoint();
        if(Timer > 1.1f)
            Destroy(gameObject);
    }
    public void FirePeachone(GameObject objPre, Transform dstTransform, Vector3 p)
    {
        GameObject newobs = Instantiate(objPre, GameObject.Find("SkillFiringSystem").transform);   //skillFiringSystem에서 프리팹 가져오기
        var newObjPeachone = newobs.GetComponent<Peachone>();
        newObjPeachone.StartPoint = GameManager.instance.player.transform;
        newObjPeachone.ControlPoint = p;
        newObjPeachone.EndPoint = dstTransform;
        newObjPeachone.UsePeach = true;
        newobs.transform.position = GameManager.instance.player.transform.position; //시작 위치
    }
    public void CreateCircle(GameObject peachPre, GameObject bounderyPre, Weapon peachone)
    {
        Timer += Time.deltaTime;
        if (Timer > peachone.WeaponTotalStats[((int)Enums.WeaponStat.Cooldown)])
        {
            bounderyPre.GetComponent<PeachBoundery>().CreateCircle(peachPre, bounderyPre, true, peachone);
            Timer = 0;
            peachPre.transform.localScale = mDefaultScale * peachone.WeaponTotalStats[((int)Enums.WeaponStat.Area)];
        }
    }
    private Vector3 calculateBezierPoint()
    {
        float u = 1f - Timer;
        float tt = Timer * Timer;
        float uu = u * u;
        Vector3 nowPos = uu * StartPoint.position;
        nowPos += 2f * u * Timer * ControlPoint;
        nowPos += tt * EndPoint.position;

        return nowPos;
    }
}
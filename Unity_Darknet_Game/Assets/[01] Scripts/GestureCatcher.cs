using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureCatcher : MonoBehaviour
{
    private bool[] weaponCnt;
    private bool coolDown = true;
    private WaitForSeconds ws = new WaitForSeconds(0.25f);

    //손동작 변화에 따른 델리게이트
    //현재 이 이벤트를 구독하는 클래스:
    //TextManager, 현재 손동작(무기)를 표시하기위해 구독 중
    //WeaponShooter, 현재 손동작(무기)에 맞는 총알 발사를 위해 구독 중
    public event VoidIntNotier OnGestured;
    void Start()
    {
        //HandTracking 클래스로부터 오는 손동작 변화 구독
        HandTracking.instance.OnGestureChange += SubscribeGesture;
        weaponCnt = new bool[4];
        for(int i = 0; i < weaponCnt.Length; i++)
        {
            weaponCnt[i] = true;
        }
    }
    void OnDisable()
    {
        HandTracking.instance.OnGestureChange -= SubscribeGesture;
    }
    void SubscribeGesture(int gesture)
    {
        if (coolDown == false) return;
        //아무 동작도 없는 경우 : 추후 업데이트 예정, 현재 아무 기능 없음
        if (gesture == 3)
        {
            OnGestured?.Invoke(gesture);
            return;
        }
        StartCoroutine(ShotWeapon(gesture));
    }

    IEnumerator ShotWeapon(int g)
    {
        coolDown = false;
        //재사용 대기시간 적용되어 총알 발사
        OnGestured?.Invoke(g);
        yield return ws;
        coolDown = true;
    }
}

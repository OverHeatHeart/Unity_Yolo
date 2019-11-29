using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureCatcher : MonoBehaviour
{
    private bool[] weaponCnt;
    private bool coolDown = true;
    private WaitForSeconds ws = new WaitForSeconds(0.25f);

    //손동작 변화에 따른 델리게이트
    public event VoidIntNotier OnGestured;
    void Start()
    {
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
        OnGestured?.Invoke(g);
        yield return ws;
        coolDown = true;
    }
}

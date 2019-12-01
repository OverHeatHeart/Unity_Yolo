using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    public Transform weaponPos;
    public Weapon[] weapons;
    public Queue<Weapon>[] weaponQueue;     //총알 숫자만큼의 큐가 초기화될 것

    private GestureCatcher gc;

    void Start()
    {
        weaponQueue = new Queue<Weapon>[weapons.Length];
        for(int i = 0; i < weaponQueue.Length; i ++)
        {
            //무기 큐 초기화
            weaponQueue[i] = new Queue<Weapon>();
            //해당 무기에 미사일 20발 장전
            for(int j = 0; j < 20; j++)
            {
                var w = Instantiate(weapons[i], transform.position, Quaternion.identity, transform);
                w.InitWeapon(this);
                w.SetIsShot(false);
                weaponQueue[i].Enqueue(w);
            }
        }
        //제스쳐 가져오기
        gc = GetComponent<GestureCatcher>();
        gc.OnGestured += SubscribeGesture;
    }

    //제스쳐 변화를 구독하는 메서드
    private void SubscribeGesture(int g)
    {
        //제스쳐가 2 이상, 아무 동작이 아니라면 반환
        if (g > 2) return;
        if (TextManager.instance.IsGameOver == true) return;
        weaponQueue[g].Dequeue().SetIsShot(true);
        GameManager.instance.audi.PlayOneShot(GameManager.instance.soundPack.weapons[g]);
    }

    public void RequeueMe(Weapon w, DamageType dt)
    {
        //충돌하거나 사거리끝까지 간 총알은 다시 큐로 돌아오기
        weaponQueue[(int)dt].Enqueue(w);
    }
}

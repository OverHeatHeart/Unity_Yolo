using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponShooter : MonoBehaviour
{
    public Transform weaponPos;
    public Weapon[] weapons;
    public Queue<Weapon>[] weaponQueue;

    private GestureCatcher gc;

    // Start is called before the first frame update
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


    private void SubscribeGesture(int g)
    {
        if (g > 2) return;
        if (TextManager.instance.IsGameOver == true) return;
        weaponQueue[g].Dequeue().SetIsShot(true);
        GameManager.instance.audi.PlayOneShot(GameManager.instance.soundPack.weapons[g]);
    }

    public void RequeueMe(Weapon w, DamageType dt)
    {
        weaponQueue[(int)dt].Enqueue(w);
    }
}

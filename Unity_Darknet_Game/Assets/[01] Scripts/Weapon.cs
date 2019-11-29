using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Vector3 desti = Vector3.forward;
    public Vector3 origin = Vector3.zero;
    public float spd = 20f;
    public DamageType type = DamageType.Fist;
    public ParticleSystem hitticle;

    private bool isShot = false;
    private WeaponShooter ws;
    private GameObject[] particles;
    private BoxCollider bc;
    private WaitForSeconds wait = new WaitForSeconds(3f);
    public void InitWeapon(WeaponShooter ws)
    {
        this.ws = ws;
        bc = GetComponent<BoxCollider>();
        //파티클들 찾기
        var a = GetComponentsInChildren<ParticleSystem>();
        particles = new GameObject[a.Length];
        for(int i = 0; i < particles.Length; i++)
        {
            particles[i] = a[i].gameObject;
        }
        bc.enabled = false;
    }

    private void Update()
    {
        if(isShot == true)
            ProcessShoot();
    }

    private void ProcessShoot()
    {
        transform.SetParent(null);
        Vector3 dir = desti - transform.position;
        transform.position += dir.normalized * Time.deltaTime * spd;
        if (Vector3.Distance(transform.position, desti) < 0.5f)
        {
            SetIsShot(false);
            Reset();
        }
    }

    //제자리로 돌아오면서 부모 설정
    private void Reset()
    {
        transform.SetParent(ws.transform);
        transform.localPosition = Vector3.zero;
        ws.RequeueMe(this, type);
    }

    //쏠 때 파티클들 활성화하고 앞으로 나아가게 만듦, 부모 해제
    public void SetIsShot(bool booleana)
    {
        bc.enabled = booleana;
        if(booleana == true)
            desti = transform.position + (transform.forward * 30f);
        isShot = booleana;
        foreach (GameObject g in particles)
        {
            g.SetActive(booleana);
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if(col.CompareTag("ENEMY"))
        {
            col.GetComponent<Enemy>().GetDamage(type);
            Instantiate(hitticle, col.transform.position + Vector3.back * 2f, Quaternion.identity);
            int ran = Random.Range(0, 2);
            GameManager.instance.audi.PlayOneShot(GameManager.instance.soundPack.enemyHit[ran], 2f);
            SetIsShot(false);
            Reset();
        }
    }
}

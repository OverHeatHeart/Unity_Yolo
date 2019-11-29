using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    static public GameManager instance;
    //오디오
    public SoundPack_Scriptable soundPack;
    public ParticleSystem deathTicle;
    public AudioSource audi;

    //파티클
    [SerializeField] int numOfTicle = 8;
    Queue<ParticleSystem> deaTicleQueue;
    WaitForSeconds ws = new WaitForSeconds(2f);

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
        //적 격추시 발생시킬 파티클 오브젝트풀링
        deaTicleQueue = new Queue<ParticleSystem>();
        for (int i = 0; i < numOfTicle; i++)
        {
            var a = Instantiate(deathTicle);
            deaTicleQueue.Enqueue(a);
        }
        audi = GetComponent<AudioSource>();
    }

    public void GetDeathTicle(Vector3 pos)
    {
        StartCoroutine(Delay(pos));
    }

    IEnumerator Delay(Vector3 pos)
    {
        var ticle = deaTicleQueue.Dequeue();
        ticle.Play();
        ticle.transform.position = pos;
        yield return ws;
        ticle.transform.position = Vector3.zero;
        deaTicleQueue.Enqueue(ticle);
    }
}

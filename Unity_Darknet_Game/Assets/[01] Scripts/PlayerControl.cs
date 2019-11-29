using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private Transform tr;
    private float thrust = 0;
    private float skyDir = 1f;
    private float timer = 0;
    private Vector3 lastDir;
    private Vector3 originCamPos;
    private bool isShaking = false;
    private AudioSource audi;

    [SerializeField] float thrustRange = 5f;
    [SerializeField] float spd = 4;
    public float moveRange = 15f;
    public Transform wrath;
    public Transform cam;


    // Start is called before the first frame update
    void Start()
    {
        tr = transform;
        originCamPos = cam.localPosition;
        audi = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TextManager.instance.IsGameOver == true) return;
        SetTimer();
        RenderSettings.skybox.SetFloat("_Rotation", timer);
        float h = 0;
        float v = 0;
        //입력값 받기
        if (Input.GetKey(KeyCode.W)) { v = 1; skyDir = 0.1f; PlayThrustAudio(); }
        if (Input.GetKey(KeyCode.S)) { v = -1; skyDir = 0.1f; PlayThrustAudio(); }
        if (Input.GetKey(KeyCode.A)) { h = -1; skyDir = 1; skyDir *= h; PlayThrustAudio(); }
        if (Input.GetKey(KeyCode.D)) { h = 1; skyDir = 1; skyDir *= h; PlayThrustAudio(); }
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) { v = 0; skyDir = 0.1f; thrust *= 0.5f; }
        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) { h = 0; skyDir = 0.1f; thrust *= 0.5f; }
        Vector3 dir = new Vector3(h, v, 0);
        ProcessMove(dir.normalized);
    }

    void ProcessMove(Vector3 dir)
    {
        //이동시키기
        if(dir.sqrMagnitude != 0)
        {
            thrust += Time.deltaTime * spd;
            if (thrust >= thrustRange) thrust = thrustRange;
            lastDir = dir;
        }
        else
        {
            thrust -= Time.deltaTime * spd * 2f;
            if (thrust < 0) thrust = 0;
        }
        Vector3 desti = tr.position + lastDir * thrust;
        float x = Mathf.Clamp(desti.x, -moveRange, moveRange);
        float y = Mathf.Clamp(desti.y, -moveRange, moveRange);
        Vector3 clamped = new Vector3(x, y, 0);
        tr.position = clamped;


        //로테이션
        //yaw 없음
        //z 축 회전 : x축으로 움직일 때
        //x 축 회전 : y축으로 움직일 때
        float zFactor = dir.x * 2.2f;
        float xFactor = dir.y * 2.2f;
        Quaternion destiRot = Quaternion.Euler(-xFactor, 0, -zFactor);
        Quaternion lerpedRot = Quaternion.Lerp(wrath.localRotation, destiRot, Time.deltaTime * 3.5f);
        wrath.localRotation = lerpedRot;
    }
    void SetTimer()
    {
        timer += Time.deltaTime * skyDir;
    }

    private void OnTriggerEnter(Collider col)
    {
        if (TextManager.instance.IsGameOver == true) return;
        if (col.CompareTag("MISSILE")) return;
        int ran = Random.Range(0, 3);
        audi.PlayOneShot(GameManager.instance.soundPack.collisions[ran]);
        if (isShaking == true) return;
        StartCoroutine(ShakeCam(0.08f, 0.5f));
        TextManager.instance.GetScore(-300);
    }

    public IEnumerator ShakeCam(float _amount, float _duration)
    {
        isShaking = true;
        float timer = 0;
        while (timer <= _duration)
        {
            cam.localPosition = (Vector3)Random.insideUnitCircle * _amount + originCamPos;
            timer += Time.deltaTime;
            yield return null;
        }
        cam.localPosition = originCamPos;
        isShaking = false;
    }

    void PlayThrustAudio()
    {
        int ran = Random.Range(0, 3);
        if(audi.isPlaying == false)
        audi.PlayOneShot(GameManager.instance.soundPack.thrusts[ran]);
    }
}

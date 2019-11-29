using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    static public TextManager instance;
    [SerializeField] private Text curWeapon;
    [SerializeField] private Text curScore;

    private int score = 0;

    public bool IsGameOver { private set; get; } = false;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }
   
    void Start()
    {
        GetComponent<GestureCatcher>().OnGestured += SubscribeGesture;
        curScore.text = score.ToString();
    }

    void Disable()
    {
        GetComponent<GestureCatcher>().OnGestured -= SubscribeGesture;
    }

    void SubscribeGesture(int g)
    {
        switch(g)
        {
            case 0:
                curWeapon.text = "Fist Missle";
                break;
            case 1:
                curWeapon.text = "Point Laser";
                break;
            case 2:
                curWeapon.text = "Victory B";
                break;
            case 3:
                curWeapon.text = "NONE";
                break;
        }
    }

    public void GetScore(int score)
    {
        this.score += score;
        curScore.text = this.score.ToString();
        //게임오버를 사용하는 스크립트 :
        //PlayerControl, 더 이상 충돌하지 않음, 더 이상 움직이지 않음
        //WeaponShooter, 더 이상 발사하지 않음
        if (this.score < -700)
        {
            IsGameOver = true;
            SceneManagery.instance.GameOver();
        }
        
    }
}

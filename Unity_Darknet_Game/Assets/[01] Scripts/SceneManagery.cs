using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneManagery : MonoBehaviour
{
    static public SceneManagery instance;

    public CanvasGroup fadePanal;
    public CanvasGroup gameOverPanal;
    public CanvasGroup mainPanal;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
        DontDestroyOnLoad(this);
    }

    public void GameOver()
    {
        //게임 오버 패널 켜기
        CanvasUtil.CgOnOff(gameOverPanal, true);
    }
    public void GameStart()
    {
        //게임 오버 패널 끄기
        CanvasUtil.CgOnOff(gameOverPanal, false);
        CanvasUtil.CgOnOff(mainPanal, false);
    }

    #region 버튼 캐치

    public void OnButtonStart()
    {
        StartCoroutine(DealFadePanal(GameStart, 1));
    }
    public void OnButtonQuit()
    {
        Application.Quit();
    }

    IEnumerator DealFadePanal(VoidNotier Method, int sceneIndex)
    {
        float fa = fadePanal.alpha;
        float time = 0f;
        while (fa < 1f)
        {
            time += Time.deltaTime;
            fadePanal.alpha = time;
            fa = fadePanal.alpha;
            yield return null;
        }
        fadePanal.alpha = 1f;
        Method();
        //동기? 전환
        AsyncOperation asyncOper = SceneManager.LoadSceneAsync(sceneIndex);
        while (fa > 0f)
        {
            time -= Time.deltaTime;
            fadePanal.alpha = time;
            fa = fadePanal.alpha;
            yield return null;
        }
        fadePanal.alpha = 0;
    }
    #endregion
}

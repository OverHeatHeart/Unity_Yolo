using UnityEngine;

public delegate void VoidIntNotier(int a);
public delegate void VoidNotier();
public enum DamageType { Fist, Point, Victor }

public class CanvasUtil
{
    /// <summary>
    /// CanvasGroup을 껐다 켰다하는 전역 메서드.
    /// </summary>
    /// <param name="cg"></param>
    /// <param name="booleana"></param>
    static public void CgOnOff(CanvasGroup cg, bool booleana)
    {
        cg.blocksRaycasts = booleana;
        cg.interactable = booleana;
        if (booleana == true)
            cg.alpha = 1;
        else
            cg.alpha = 0;
    }
}

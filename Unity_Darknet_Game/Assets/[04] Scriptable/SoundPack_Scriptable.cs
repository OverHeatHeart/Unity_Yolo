using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyAssets/SoundPack")]
public class SoundPack_Scriptable : ScriptableObject
{
    public AudioClip[] weapons;

    public AudioClip[] thrusts;

    public AudioClip desroy;
    public AudioClip[] collisions;

    public AudioClip[] enemyHit;
}

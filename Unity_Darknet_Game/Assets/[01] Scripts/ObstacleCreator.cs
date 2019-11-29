using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCreator : MonoBehaviour
{
    public Transform player;
    public Obstacle[] obstacles;
    public Enemy[] enemies;
    public int spawnCount = 6;
    public int[] enemySpawnCnt;

    // Start is called before the first frame update
    void Awake()
    {
        StartCoroutine(Spawn());
        enemySpawnCnt = new int[enemies.Length];
        for(int i = 0; i < enemySpawnCnt.Length; i++)
        {
            //해당 적의 약점포인트만큼의 스폰 숫자
            int num = ((int)enemies[i].weakPoint + 1) * 3;
            enemySpawnCnt[enemySpawnCnt.Length - i - 1] = num;
        }
    }
    //랜덤한 위치에 장애물들 설치
    IEnumerator Spawn()
    {
        PlayerControl pc = player.GetComponent<PlayerControl>();
        for (int i = 0; i < obstacles.Length; i++)
        {
            for (int j = 0; j < spawnCount; j++)
            {
                float rany = Random.Range(-pc.moveRange * 1.5f, pc.moveRange * 1.5f);
                float ranx = Random.Range(-pc.moveRange * 1.5f, pc.moveRange * 1.5f);
                float ranz = Random.Range(55f, 75f);
                Vector3 spawnPos = new Vector3(ranx, rany, ranz);
                var ob = Instantiate(obstacles[i],  spawnPos, Quaternion.identity, transform);
                ob.spd = Random.Range(15f, 18f);
                ob.origin = new Vector3(ranx, rany, ranz);
                ob.desti = new Vector3(ranx, rany, -ranz);
                yield return new WaitForSeconds(0.5f);
            }
        }
        StartCoroutine(EnemySpawn(pc));
    }
    //랜덤한 위치에 적군 설치
    IEnumerator EnemySpawn(PlayerControl pc)
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            for (int j = 0; j < enemySpawnCnt[i]; j++)
            {
                float rany = Random.Range(-pc.moveRange, pc.moveRange);
                float ranx = Random.Range(-pc.moveRange, pc.moveRange);
                float ranz = Random.Range(55f, 75f);
                Vector3 spawnPos = new Vector3(ranx, rany, ranz);
                var ob = Instantiate(enemies[i], spawnPos, Quaternion.identity, transform);
                ob.spd = Random.Range(10f, 15f);
                ob.origin = new Vector3(ranx, rany, ranz);
                ob.desti = new Vector3(ranx, rany, -ranz);
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}

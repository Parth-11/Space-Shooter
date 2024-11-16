using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class SpawnManager : MonoBehaviour
{

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerUps;
    private bool player_alive = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator SpawnEnemyRoutine(){
        while(player_alive){
            Vector3 spawn_position = new Vector3(Random.Range(-8f,8f),5,0);
            GameObject new_enemy = Instantiate(_enemyPrefab, spawn_position, Quaternion.identity);
            new_enemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine(){
        while(player_alive){
            Vector3 spwan_position = new Vector3(Random.Range(-8f,8f),5,0);
            Instantiate(_powerUps[Random.Range(0,3)], spwan_position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(0,8));
        }
    }

    public void OnPlayerDeath(){
        player_alive = false;
    }
}

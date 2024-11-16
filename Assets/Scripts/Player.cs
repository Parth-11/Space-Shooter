using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.15f;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _is_triple = false;
    private bool _is_speedUp = false;
    private bool _is_shield = false;
    [SerializeField]
    private GameObject _shieldVisualizersulizer;
    private int _score;

    // Start is called before the first frame update
    void Start()
    {   
        _spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        if(_spawnManager==null){
                Debug.LogError("Spawn Manager is null");
            }
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {
       CalculateMovement();
       if (Input.GetKey(KeyCode.Space) && Time.time>_canFire)
       {
            FireLaser();
       }
    }



    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        transform.position = new Vector3 (transform.position.x,Mathf.Clamp(transform.position.y,-3.8f,0),0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser(){
            _canFire = Time.time+_fireRate;
            if(_is_triple == true){
                Instantiate(_tripleShotPrefab,transform.position + new Vector3(-1.4f,0,0) , Quaternion.identity);
            }else{
            Instantiate(_laserPrefab,transform.position + new Vector3(0,1.05f,0),Quaternion.identity);
            }
    }

    public void Damage(){
        if(_is_shield){
            _is_shield = false;
            _shieldVisualizersulizer.SetActive(false);
            return;
        }
        _lives--;
        if(_lives<1){
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }
    }

    public void TripleShotActive(){
        _is_triple = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine(){
        yield return new WaitForSeconds(5.0f);
        _is_triple = false;
    }

    public void SpeedUpActive(){
        _is_speedUp = true;
        _speed*=2;
        StartCoroutine(SpeedUpPowerRoutine());
    }

    IEnumerator SpeedUpPowerRoutine(){
        yield return new WaitForSeconds(5);
        _is_speedUp = false;
        _speed/=2;
    }
    public void ShieldActive(){
        _shieldVisualizersulizer.SetActive(true);
        _is_shield = true;
        StartCoroutine(ShieldCoolDownRoutine());
    }

    IEnumerator ShieldCoolDownRoutine(){
        yield return new WaitForSeconds(5);
        _is_shield = false;
        _shieldVisualizersulizer.SetActive(false);
    }

    public void ScoreUpdate(){
        _score += 10;
    }

}

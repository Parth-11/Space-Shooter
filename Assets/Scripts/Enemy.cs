using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _Speed = 4.0f;

    private Player _player;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _Speed * Time.deltaTime);

        if(transform.position.y < -5f){
            transform.position = new Vector3(Random.Range(-8f,8f),7,0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            Player player = other.transform.GetComponent<Player>();
            if(player != null){
                player.Damage();
            }
            Destroy(this.gameObject);
        }

        if(other.tag == "Laser"){
            Destroy(other.gameObject);
            if(_player != null){
                _player.ScoreUpdate();
            }
            Destroy(this.gameObject);
        }

    }
}

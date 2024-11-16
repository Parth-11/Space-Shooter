using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUp : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int _powerUp_id;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed*Time.deltaTime);
        if(transform.position.y < -4.5f){
            Destroy(this.gameObject);
        }
    }

    void  OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Player player = other.gameObject.GetComponent<Player>();
            if(player != null){
                switch(_powerUp_id){
                    case 0:
                        player.TripleShotActive();
                        break;
                    case 1:
                        player.SpeedUpActive();
                        break;
                    case 2:
                        player.ShieldActive();
                        break;
                    default:
                        break;
                }
            }
            Destroy(this.gameObject);
        }
    }

}

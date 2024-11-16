using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float m_Speed = 8.0f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * m_Speed * Time.deltaTime);

        if(transform.position.y>=8.0f){
            if(transform.parent != null){
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }

    }
}

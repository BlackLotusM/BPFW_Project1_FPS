using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    Transform tr_Player;
    public GameObject player;
    public float sphereRadius;
    float f_RotSpeed = 3.0f, f_MoveSpeed = 13.0f;

    public string state;

    Rigidbody blokkie;

    // Use this for initialization
    void Start()
    {
        blokkie = gameObject.GetComponent<Rigidbody>();
        tr_Player = player.transform;
        state = "Search";
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* Look at Player*/
        transform.rotation = Quaternion.Slerp(transform.rotation
                                              , Quaternion.LookRotation(tr_Player.position - transform.position)
                                              , f_RotSpeed * Time.deltaTime);

        /* Move at Player*/
        transform.position += transform.forward * f_MoveSpeed * Time.deltaTime;

        float dist = Vector3.Distance(tr_Player.position, transform.position);

        if (dist < sphereRadius)
        {
            if (state == "Search")
            {
                state = "Fly";
            }
        }

        if (dist > sphereRadius * 1.5)
        {
            if (state == "Fly")
            {
                state = "Search";
            }
        }

        if (state == "Search")
        {
            blokkie.useGravity = true;
        }

        if (state == "Fly")
        {
            blokkie.useGravity = false;
            if (transform.position.y < tr_Player.position.y)
            {
                transform.position += new Vector3(0, 000.1f, 0);
            }
        }
    }
}

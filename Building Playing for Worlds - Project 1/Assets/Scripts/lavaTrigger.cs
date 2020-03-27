using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lavaTrigger : MonoBehaviour
{
    public GameObject player;
    GameObject yeet;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            ScoreBoard.lavadeath = true;
        }
        else
        {
            Rigidbody test =  other.gameObject.GetComponent<Rigidbody>();
            //other.gameObject.transform.position += new Vector3(0, 25f, 0);
            test.velocity = new Vector3(Random.Range(-10, 15), 50, Random.Range(-10, 15));
        }
    }
}

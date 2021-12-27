using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball : MonoBehaviour
{
    Rigidbody rb;

    void Start(){
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0) && rb.velocity.magnitude <= 0.2){
            rb.AddForce(500f * new Vector3(0,1,0));
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Plant"){
            rb.velocity = new Vector3();
            Vector3 force_vector = new Vector3(-transform.position.x,0,-transform.position.z).normalized * Random.Range(30f, 50f) + new Vector3(0,500,0);
            force_vector = Quaternion.AngleAxis(Random.Range(-90.0f, 90.0f), Vector3.up) * force_vector;
            rb.AddForce(force_vector);
        }if(collision.gameObject.name == "Floor")
            rb.velocity = new Vector3();
        Debug.Log("Touched " + collision.gameObject.name);
    }
}

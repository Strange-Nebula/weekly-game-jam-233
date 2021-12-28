using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void TestDelegate(string behaviour);

public class ball : MonoBehaviour
{

    public event TestDelegate TestEvent;
    int score_count=0;
    Rigidbody rb;
    bool playing = false;
    Transform projection;

    void Start(){
        rb = GetComponent<Rigidbody>();
        projection = transform.GetChild(0);
    }

    void Update(){
        if(playing){
            projection.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        }
    }

    void OnMouseOver(){
        if(Input.GetMouseButtonDown(0) && rb.velocity.magnitude <= 0.2){
            rb.AddForce(new Vector3(0,500,0));
            playing = true;
            projection.gameObject.SetActive(true);
            if(TestEvent!=null)
                TestEvent("PlayBall");
            else
                Debug.LogWarning("TestEvent is NULL");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Plant"){
            score_count ++;
            rb.velocity = new Vector3();
            Vector3 force_vector = new Vector3(-transform.position.x,0,-transform.position.z).normalized * Random.Range(50f + 2*score_count, 70f + 2*score_count) + new Vector3(0,(500 - 10*score_count),0);
            force_vector = Quaternion.AngleAxis(Random.Range(-90.0f, 90.0f), Vector3.up) * force_vector;
            rb.AddForce(force_vector);
        }if(collision.gameObject.name == "Floor"){
            rb.velocity = new Vector3();
            playing = false;
            projection.gameObject.SetActive(false);
            score_count=0;
            if(TestEvent!=null)
                TestEvent("WalkAround");
            else
                Debug.LogWarning("TestEvent is NULL");
        }
        Debug.Log("Touched " + collision.gameObject.name + " score is " + score_count);
    }
}

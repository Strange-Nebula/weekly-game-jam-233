using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_plant : MonoBehaviour
{

    Rigidbody rb;

    string behaviour;
    Vector3 target_position;
    float speed = 0.008f;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        behaviour = "PlayBall";

        StartCoroutine(behaviour);
    }

    // Update is called once per frame
    void Update()
    {
        Plane plane = new Plane(Vector3.up, 0);


        if (Input.GetKeyDown("space"))
        {
            SwitchBehaviour("GoSleep");
        }

        if (Input.GetMouseButtonDown(0)){
            float distance;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (plane.Raycast(ray, out distance))
            {
                target_position = ray.GetPoint(distance);
                target_position = new Vector3 (target_position.x, transform.position.y, target_position.z);
            }
        }
    }

    IEnumerator GoSleep(){
        target_position = new Vector3(-3.8f,transform.position.y, -3.8f);
        while ((target_position-transform.position).magnitude > 2.5f)
        {
            yield return transform.position = Vector3.MoveTowards(transform.position, target_position, speed);
        }
        rb.AddForce(500f * new Vector3(0,1,0));
        yield return null;
        while ((target_position-transform.position).magnitude >= 1.2f)
        {
            yield return transform.position = Vector3.MoveTowards(transform.position, target_position, speed);
        }
    }

    IEnumerator WalkAround(){
        while (true){
            yield return new WaitForSeconds(Random.Range(0f,3f)+Random.Range(0f,2f));
            target_position = new Vector3(-transform.position.x,0,-transform.position.z).normalized * Random.Range(3f, 5f);
            target_position = new Vector3(target_position.x, transform.position.y, target_position.z);
            target_position =  Quaternion.AngleAxis(Random.Range(-90.0f, 90.0f), Vector3.up) * target_position;
            transform.LookAt(target_position);
            while ((target_position-transform.position).magnitude > 1)
            {
                yield return transform.position = Vector3.MoveTowards(transform.position, target_position, speed);
            }
        
        }
    }

    IEnumerator PlayBall(){
        while(true)
            yield return transform.position = Vector3.MoveTowards(transform.position, target_position, speed);
    }

    public void SwitchBehaviour(string behav){
        StopCoroutine(behaviour);
        behaviour = behav;
        StartCoroutine(behaviour);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room_plant : MonoBehaviour
{
    Rigidbody rb;

    IEnumerator behaviour;
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        behaviour = WalkAround();

        StartCoroutine(behaviour);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown("space"))
        {
            StopCoroutine(behaviour);
            behaviour = GoSleep();
            StartCoroutine(behaviour);
        }
    }

    IEnumerator GoSleep(){
        Vector3 target_position = new Vector3(-3.8f,transform.position.y, -3.8f);
        while ((target_position-transform.position).magnitude > 2.5f)
        {
            yield return transform.position = Vector3.MoveTowards(transform.position, target_position, 0.01f);
        }
        rb.AddForce(500f * new Vector3(0,1,0));
        yield return null;
        while ((target_position-transform.position).magnitude >= 1.2f)
        {
            yield return transform.position = Vector3.MoveTowards(transform.position, target_position, 0.01f);
        }
    }

    IEnumerator WalkAround(){
        while (true){
            yield return new WaitForSeconds(Random.Range(0f,3f)+Random.Range(0f,2f));
            Vector3 target_position = new Vector3(-transform.position.x,0,-transform.position.z).normalized * Random.Range(3f, 5f);
            target_position = new Vector3(target_position.x, transform.position.y, target_position.z);
            target_position =  Quaternion.AngleAxis(Random.Range(-90.0f, 90.0f), Vector3.up) * target_position;
            transform.LookAt(target_position);
            while ((target_position-transform.position).magnitude > 1)
            {
                Debug.Log("moving");
                yield return transform.position = Vector3.MoveTowards(transform.position, target_position, 0.01f);
            }
        
        }
    }
}

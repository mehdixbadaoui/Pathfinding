using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Vector2 getPosition(){
        return rb.position;
    }
    public float player_speed = 5f;
    public Rigidbody2D rb;
    Vector2 move;
    Vector2 start = new Vector2(6,4);

    // Start is called before the first frame update
    void Start(){

    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate(){

        transform.position = Vector2.MoveTowards(transform.position, move, player_speed* Time.deltaTime);
        if(Vector2.Distance(transform.position, move) <= 0.05f){

            move.x = Input.GetAxisRaw("Horizontal");

            //Debug.Log(move.x);
            move.y = Input.GetAxisRaw("Vertical");
        }

        rb.MovePosition(rb.position + move * player_speed * Time.fixedDeltaTime);
        
    }
}

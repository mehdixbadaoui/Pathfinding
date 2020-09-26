using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyMovement : MonoBehaviour
{

    private Vector2 player_pos;
    public float enemy_speed = 10f;
    public Rigidbody2D rb_enemy;
    List<point> allpoints = new List<point>();
    point player_point;
    point enemy_point;
    List<node_astar> chemin = new List<node_astar>();
    public List <node_astar> list;
    AStar astar = new AStar();

    int width = 12;
    int height = 10; 


    void Start()
    {
        
        node_astar start = new node_astar(-4,-1);
        node_astar end = new node_astar(4,3);
        chemin = astar.path(start, end, height, width);
        foreach (node_astar node in chemin)
        {
            // Debug.Log(node.x + " " + node.y);
        }
    }

    void FixedUpdate()
    {
        player_pos = GameObject.Find("Hero").transform.position;
        // player_point = new point(player_pos.x, player_pos.y);
        // enemy_point = new point(rb_enemy.position.x, rb_enemy.position.y);

        node_astar start = new node_astar((int)rb_enemy.position.x, (int)rb_enemy.position.y);
        node_astar end = new node_astar((int)player_pos.x, (int)player_pos.y);

        chemin = astar.path(start, end, height, width);

        for(int i = 0; i< chemin.Count - 1; i++){
            Debug.DrawLine(new Vector2(chemin[i].x, chemin[i].y), new Vector2(chemin[i+1].x,
             chemin[i+1].y), Color.green,2,false);
        }

        foreach (node_astar node in chemin)
        {
            // Debug.Log(node.x + " " + node.y);
            rb_enemy.position = Vector2.MoveTowards(rb_enemy.position, new Vector2(node.x, node.y), enemy_speed * Time.deltaTime);
        }


        // // enemy_point = new point(-5,-5);
        // Debug.Log("enemy "+rb_enemy.position);

        // chemin = dijkstra.path(player_point, enemy_point, allpoints);
        // foreach (point p in chemin)
        // {
        //     rb_enemy.MovePosition(rb_enemy.position + p.getPoint() * enemy_speed * Time.fixedDeltaTime);
        //     //Debug.Log("vers "+p.getPoint());
        // }
        // Debug.Log("one update");


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node_astar
{
    public int x;
    public int y;

    public int g_cost;
    public int h_cost;
    public int f_cost;

    public node_astar parent = null;

    public node_astar(int x, int y){
        this.x = x;
        this.y = y;
    }
    public Vector2 get_xy(){
        return new Vector2(this.x, this.y);
    }

    public void updateFCost(){
        this.f_cost = this.g_cost + this.h_cost; 
    }
    
}

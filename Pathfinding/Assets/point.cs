using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class point{

    private Vector2 xy;
    private bool visitable;
    public bool visited = false;
    //private int dist;
    private int weight = 1;
    private int dist;

    public point(float x, float y){
        xy.x = x;
        xy.y = y;
    }

    public Vector2 getPoint(){
        return this.xy;
    }

    public int getDist(){
        return this.dist;
    }
    public int getWeight(){
        return this.weight;
    }

    public void incDist(){
        this.dist += 1;
    }

    public void setVisited(bool b){
        this.visited = b;
    }
    public void setDist(int d){
        this.dist = d;
    }
}
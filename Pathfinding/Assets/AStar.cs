using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class AStar
{
    int straight_cost = 10;
    int diag_cost = 14;

    node_astar[,] grid;
    private List<node_astar> available;
    private List<node_astar> visited;
    private List<node_astar> all;

    // public float path (int sx, int sy, int ex, int ey, int width , int height){
    // public float path (node_astar start, node_astar end, int width, int height){
    public List<node_astar> path (node_astar start, node_astar end, int width, int height){

        grid = new node_astar [height, width];
        grid[start.x + height/2, start.y + width/2] = start;
        grid[end.x + height/2, end.y + width/2] = end;
        node_astar s = grid[start.x + height/2, start.y + width/2];
        node_astar e = grid[end.x + height/2, end.y + width/2];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                node_astar node = new node_astar(i - 6, j - 5);
                if(node.x == start.x && node.y == start.y) continue;
                if(node.x == end.x && node.y == end.y) continue;
                node.g_cost = int.MaxValue;
                node.h_cost = distance(node, end);
                node.updateFCost();
                node.parent  = null;
                grid[i,j] = node;
                // Debug.Log(grid[i,j] == null);
             }
        }

        available = new List<node_astar>{start};
        visited = new List<node_astar>();
        
        s.g_cost = 0;
        s.h_cost = distance(start, end);
        // Debug.Log("distance start end " + start.h_cost);
        s.updateFCost();
        node_astar lowest;
        node_astar current;

        while (available.Count > 0)
        // for(int p = 0; p<1; p++)
        {
            lowest = available.OrderBy(x => x.f_cost).ToList()[0];
            current = grid[lowest.x + 6, lowest.y + 5];
            if (current == e)
            {
                /// kemelna
                return ourPath(current);
            }
            available.Remove(current);
            visited.Add(current);
            
            foreach (node_astar neighbour in neighbours(current))
            {
                
                if (visited.Contains(neighbour)) continue;

                int new_gcost = current.g_cost + distance(current, neighbour);
                if(new_gcost < neighbour.g_cost){
                    neighbour.g_cost = new_gcost;
                }

                neighbour.h_cost = distance(neighbour, end);
                neighbour.updateFCost();

                neighbour.parent = current;
                if(!available.Contains(neighbour)){
                    available.Add(neighbour);
                }
                
                
            }
        }
        return null;
    }

    private int distance(node_astar s, node_astar e){
        int x_dist = Math.Abs(s.x - e.x);
        int y_dist = Math.Abs(s.y - e.y);
        int rest = Math.Abs(x_dist - y_dist);
        return(Math.Min(x_dist, y_dist)* diag_cost + rest * straight_cost);
    }

    List<node_astar> ourPath(node_astar end){
        List<node_astar> p = new List<node_astar>();
        p.Add(end);
        node_astar curr = end;
        while(curr.parent != null){
            p.Add(curr.parent);
            curr = curr.parent;
        }
        p.Reverse();
        return p;
    }

    List<node_astar> neighbours(node_astar node){
        List<node_astar> n = new List<node_astar>();
        // Debug.Log(node.x + " " + node.y);
        for (int x = node.x -1; x <= node.x +1; x++)
        {
            if(x < -6 || x > 6) continue;
            for (int y = node.y -1; y <= node.y +1; y++)
            {
                if(y < -5 || y > 5) continue;
                if(x+6 >= grid.GetLength(0) || y+5 >= grid.GetLength(1)) continue;
                if(x == node.x && y == node.y) continue;
                n.Add(grid[x + 6, y + 5]);
            }
        }
        // Debug.Log("n.count " + n.Count);
        return n;
    }

}

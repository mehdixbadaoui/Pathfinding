using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
public class dijkstra{


    public static List<point> getAdj(point a){
        List<point> adjacents = new List<point>();
        
        for (int i = (int)a.getPoint().x -1; i <= (int)a.getPoint().x +1; i++)
        {
            for (int j = (int)a.getPoint().y -1; j <= (int)a.getPoint().y +1; j++)
            {
                if (((int)a.getPoint().x!=i) || ((int)a.getPoint().y!=j)){
                    if(!new point(i,j).visited){
                        adjacents.Add(new point(i,j));
                    }
                    else{
                        Debug.Log(i+","+j+" deja visité");
                    }
                    
                }
            }
        }
        //Debug.Log(adjacents.Count);
        return adjacents;
    }

    public static List<point> path(point src, point dest, List<point> allpoints){



        List<point> chemin = new List<point>();
        List<Vector2> adj;
        List<point> unvisited = new List<point>();
        List<point> visited = new List<point>();
        List<point> unlocked = new List<point>();
        int total = allpoints.Count;

        
        //initialisation of all points
        foreach(point p in allpoints){
            p.setDist(999);
            unvisited.Add(p);
        }
        
        src.setDist(0);
        unvisited.Remove(src);
    
    point nxt;
    point curr = src;
    int tmp = 0;
    Debug.Log("total " + total);
    while(unlocked.Count < total){

        curr.setVisited(true);
        chemin.Add(curr);

        foreach (point p in getAdj(curr))
        {
            if(!unlocked.Contains(p)){
                unlocked.Add(p);
            }
            else{
                Debug.Log(p.getPoint() + "deja visité");
            }
            p.setDist(Math.Min(curr.getDist() + 1,p.getDist()));

        }
    

        nxt = unlocked.OrderBy(x => x.getDist()).ToList()[0];//.Union(unlocked).ToList()[0];
        Debug.Log(nxt.getPoint());

        if( nxt.getDist() < curr.getDist()){
            for (int i = 0; i < curr.getDist() - nxt.getDist(); i++)
            {
                chemin.RemoveAt(chemin.Count - 1);
            }
        }
        
        Debug.Log("count "+chemin.Count);
        curr  = nxt;
        tmp++;
    }


    //Debug.Log(index);

    return chemin;
    //return index;
        //return new Vector2(1,1);
    }
    
 }

 /////////////////////////////////////////////////////////////////////////////

// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ShortestPath : MonoBehaviour
// {

//     private GameObject[] nodes;

//     /// <summary>
//     /// Finding the shortest path and return in a List
//     /// </summary>
//     /// <param name="start">The start point</param>
//     /// <param name="end">The end point</param>
//     /// <returns>A List of transform for the shortest path</returns>
//     public List<Transform> findShortestPath(Transform start, Transform end)
//     {

//         nodes = GameObject.FindGameObjectsWithTag("Node");

//         List<Transform> result = new List<Transform>();
//         Transform node = DijkstrasAlgo(start, end);

//         // While there's still previous node, we will continue.
//         while (node != null)
//         {
//             result.Add(node);
//             Node currentNode = node.GetComponent<Node>();
//             node = currentNode.getParentNode();
//         }

//         // Reverse the list so that it will be from start to end.
//         result.Reverse();
//         return result;
//     }

//     /// <summary>
//     /// Dijkstra Algorithm to find the shortest path
//     /// </summary>
//     /// <param name="start">The start point</param>
//     /// <param name="end">The end point</param>
//     /// <returns>The end node</returns>
//     private Transform DijkstrasAlgo(Transform start, Transform end)
//     {
//         double startTime = Time.realtimeSinceStartup;

//         // Nodes that are unexplored
//         List<Transform> unexplored = new List<Transform>();

//         // We add all the nodes we found into unexplored.
//         foreach (GameObject obj in nodes)
//         {
//             Node n = obj.GetComponent<Node>();
//             if (n.isWalkable())
//             {
//                 n.resetNode();
//                 unexplored.Add(obj.transform);
//             }
//         }

//         // Set the starting node weight to 0;
//         Node startNode = start.GetComponent<Node>();
//         startNode.setWeight(0);

//         while (unexplored.Count > 0)
//         {
//             // Sort the explored by their weight in ascending order.
//             unexplored.Sort((x, y) => x.GetComponent<Node>().getWeight().CompareTo(y.GetComponent<Node>().getWeight()));

//             // Get the lowest weight in unexplored.
//             Transform current = unexplored[0];

//             // Note: This is used for games, as we just want to reduce compuation, better way will be implementing A*
//             /*
//             // If we reach the end node, we will stop.
//             if(current == end)
//             {   
//                 return end;
//             }*/

//             //Remove the node, since we are exploring it now.
//             unexplored.Remove(current);

//             Node currentNode = current.GetComponent<Node>();
//             List<Transform> neighbours = currentNode.getNeighbourNode();
//             foreach (Transform neighNode in neighbours)
//             {
//                 Node node = neighNode.GetComponent<Node>();

//                 // We want to avoid those that had been explored and is not walkable.
//                 if (unexplored.Contains(neighNode) && node.isWalkable())
//                 {
//                     // Get the distance of the object.
//                     float distance = Vector3.Distance(neighNode.position, current.position);
//                     distance = currentNode.getWeight() + distance;

//                     // If the added distance is less than the current weight.
//                     if (distance < node.getWeight())
//                     {
//                         // We update the new distance as weight and update the new path now.
//                         node.setWeight(distance);
//                         node.setParentNode(current);
//                     }
//                 }
//             }

//         }

//         double endTime = (Time.realtimeSinceStartup - startTime);
//         print("Compute time: " + endTime);

//         print("Path completed!");

//         return end;
//     }

// }
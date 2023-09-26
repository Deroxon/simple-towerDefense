using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    
    public Point GridPosition { get; private set; }

    public Vector2 WorldPosition { 
        
        get
        {                       // we taking x position - top left corner || we get sprite render and we taking its length and divide by 2 and same thing for y
            return new Vector2(transform.position.x + (GetComponent<SpriteRenderer>().bounds.size.x / 2), transform.position.y - (GetComponent<SpriteRenderer>().bounds.size.y/2) ) ;
        }

    }

    
    public void Setup(Point gridPos, Vector3 worldPos)
    {
        this.GridPosition = GridPosition;
        transform.position = worldPos;
    }

}

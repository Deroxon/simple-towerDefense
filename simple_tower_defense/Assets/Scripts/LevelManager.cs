using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private cameraMovement CameraMovement;

    public float TileSize
    {
        // calculate how big our tiles are, this is used to place out tiles on the correct positions
        get
        {
            return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void CreateLevel()
    {

        string[] mapData = ReadLevelText();

        int mapX = mapData[0].ToCharArray().Length;
        // calculates the y map size
        int mapY = mapData.Length;

        Vector3 maxTile = Vector3.zero;

        // Calculates the world start point, this is the top left corner of the screen
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));


        for (int y = 0; y < mapY; y++)
        {
            char[] newTiles = mapData[y].ToCharArray(); // Gets all the tiles, that we need to place on 
            for (int x = 0; x < mapX; x++)
            {
                // places the tile in the world
               maxTile = PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }

        }
        // beacuse there was problem with getting last tile we adding one more TileSize to our coordiantes, which mean we achieve the last position of tileSize
        CameraMovement.setLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));
    }

    private Vector3 PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        // "1" = 1 Parses the tiletype to an int, so that we can use it as indexer when we create a new tile
        int tileIndex = int.Parse(tileType);

        //Creates a new tile and makes a reference to that tile in the newTile variable
        GameObject newTile = Instantiate(tilePrefabs[tileIndex]);
        newTile.transform.position = new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0);

        return newTile.transform.position;

    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string tmpData = bindData.text.Replace(Environment.NewLine, string.Empty);
        

        return tmpData.Split("-");
    }
    
}

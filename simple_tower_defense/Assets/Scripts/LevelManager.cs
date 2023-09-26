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

    private Point spawner;

    [SerializeField]
    private GameObject coinPortalPrefab;

    // dictionary hold the point as key and hold this tile on some point
    public Dictionary<Point, TileScript> Tiles { get; set; } 

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
        Point p = new Point(0, 0);
        Debug.Log(p.X);

        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {


    }




    private void CreateLevel()
    {

        Tiles = new Dictionary<Point, TileScript>();

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
                PlaceTile(newTiles[x].ToString(), x, y, worldStart);
            }

        }
        // finding the max tile on the last position
        maxTile = Tiles[new Point(mapX-1, mapY-1) ].transform.position;

        // beacuse there was problem with getting last tile we adding one more TileSize to our coordiantes, which mean we achieve the last position of tileSize
        CameraMovement.setLimits(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortal();
    }

    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        // "1" = 1 Parses the tiletype to an int, so that we can use it as indexer when we create a new tile
        int tileIndex = int.Parse(tileType);

        //Creates a new tile and makes a reference to that tile in the newTile variable and using Component TileScript to get access to Setup() function
        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();
   
        // Uses the new tile variable to change the position of the tile
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0) );

        Tiles.Add(new Point(x, y), newTile);



    }

    private string[] ReadLevelText()
    {
        TextAsset bindData = Resources.Load("Level") as TextAsset;

        string tmpData = bindData.text.Replace(Environment.NewLine, string.Empty);


        return tmpData.Split("-");
    }

    private void SpawnPortal()
    {
        spawner = new Point(0, 0);
        // spawning object coinPortalPrefab on Tiles dictionary spawner - position 0 - left top corner, quaternion - no rotate
        Instantiate(coinPortalPrefab, Tiles[spawner].GetComponent<TileScript>().WorldPosition, Quaternion.identity) ;


        //to present that we can manipulate tiles
       //  Instantiate(coinPortalPrefab, Tiles[new Point(13,7)].GetComponent<TileScript>().WorldPosition, Quaternion.identity);
    }

}

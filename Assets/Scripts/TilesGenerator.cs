using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class TilesGenerator : MonoBehaviour
{
    public Transform boardHolder;
    public Transform boardHolder2;
    public GameObject floorTiles;
    public GameObject wallTiles;
    public GameObject[] wallTiless;
    public int columns = 8;
    public int rows = 8;
    public float fm = 1.1f;
    int counter = 0;
    
    private List<ObjectPosition> objectPositionList = new List<ObjectPosition>();

    private List<ObjectPosition> corridorPositionlist = new List<ObjectPosition>();

    private List <Vector3> gridPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        boardHolder = new GameObject("Board").transform;

        instantiateHall(rows, columns, 0, 0);
        var selectEdgeTiles = objectPositionList.Where(o => o.IsEdge).ToList();
        var chosenTile = selectEdgeTiles[Random.Range(0, selectEdgeTiles.Count)];
        // var corridorEnd = instantiateCorridor(chosenTile);
        // foreach(var cor in corridorPositionlist){
        //     //Debug.Log($"Corridor is {cor.Counter}, X is {cor.X}, Z is {cor.Z}");
        // }

        // instantiateHall(rows, columns, corridorEnd.X, corridorEnd.Z);
        SpawnWalls(selectEdgeTiles);
        //SpawnWalls(corridorPositionlist);
        //LayoutObjectAtRandom(wallTiles, 5, 9);
        //Debug.Log($"Counter is {chosenTile.Counter}, X is {chosenTile.X}, Z is {chosenTile.Z}");
        //instantiateCorridor()
    }

    private void instantiateHall(int columns, int rows, int xInitial, int zInitial) {
        var xStandardised = xInitial < 0 ? -1F : 1F;
        var zStandardised = zInitial < 0 ? -1F : 1F;
        int xInitStandardised = xInitial * (int)xStandardised;
        int zInitStandardised = zInitial * (int)zStandardised;

        for (int x = xInitStandardised; x < columns + xInitStandardised; x++) {
            for(int z = zInitStandardised; z < rows + zInitStandardised;  z++) {
                GameObject instance = Instantiate(floorTiles, new Vector3(x * fm * xStandardised, 0, z * fm * zStandardised), Quaternion.identity) as GameObject;

                var newPosition = new ObjectPosition
                {
                    X = x,
                    Z = z,
                    Counter = counter,
                    ObjectInstance = instance,
                    IsEdge = false
                };

                if(x == xInitial) {
                    newPosition.IsEdge = true;
                    newPosition.EdgeStance = 3;
                }
                else if (x == columns - 1)
                {
                    newPosition.IsEdge = true;
                    newPosition.EdgeStance = 1;
                }
                else if (z == zInitial)
                {
                    newPosition.IsEdge = true;
                    newPosition.EdgeStance = 2;
                }
                else if (z == rows - 1)
                {
                    newPosition.IsEdge = true;
                    newPosition.EdgeStance = 0;
                }

                objectPositionList.Add(newPosition);
                instance.transform.SetParent(boardHolder);
                counter++;
            }
        }
    }

    private void SpawnWalls(List<ObjectPosition> tiles, ObjectPosition dontSpawntile = null, bool isHall = false)
    {
        var zMax = tiles.Select(t => t.Z).Max() + 1;
        //var zMax = tiles.Select(t => t.Z = t.Z + 1);
        var xMin = tiles.Select(t => t.X).Min();
        var xMax = tiles.Select(t => t.X).Max();

        foreach(var tile in tiles)
        {   
            var dontSpawnTileX = dontSpawntile == null ? false : tile.X == dontSpawntile.X;
            var dontSpawnTileZ = dontSpawntile == null ? false : tile.Z == dontSpawntile.Z;
            //Debug.Log($"xTile is {tile.X} = {dontSpawntile.X}, zTile is {tile.Z} = {dontSpawntile.Z}");
            //if(tile.EdgeStance == 2 || tile.EdgeStance == 3|| (tile.X == dontSpawntile.X && tile.Z == dontSpawntile.Z)) {
            if(tile.EdgeStance == 2 || (dontSpawnTileX && dontSpawnTileZ)) {
                continue;
            }
            var xTile = tile.X;
            var zTile = tile.Z;
            switch(tile.EdgeStance)
            {
                case 0:
                    zTile = zTile + 1;
                    break;
                case 1:
                    xTile = xTile + 1;
                    break;
                case 3:
                    xTile = xTile - 1;
                    break;
            }
            GameObject instance1 = Instantiate(SelectRandomObjects(wallTiless), new Vector3(xTile * fm, 0, zTile * fm), Quaternion.identity) as GameObject;
        }

        GameObject instance2 = Instantiate(SelectRandomObjects(wallTiless), new Vector3(xMin * fm, 0, zMax * fm), Quaternion.identity) as GameObject;
        GameObject instance3 = Instantiate(SelectRandomObjects(wallTiless), new Vector3(xMax * fm, 0, zMax * fm), Quaternion.identity) as GameObject;
    }

    Vector3 RandomPosition()
    {
        var randomIndex = Random.Range(0, gridPositions.Count);
        var randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    GameObject SelectRandomObjects(GameObject[] gameObjects)
    {
        var randomIndex = Random.Range(0, gameObjects.Length);
        var randomPosition = gameObjects[randomIndex];
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int min, int max)
    {
        var objectCount = Random.Range(min, max + 1);
        for(int i = 0; i < objectCount; i++)
        {
            var randomPosition = RandomPosition();
            var tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

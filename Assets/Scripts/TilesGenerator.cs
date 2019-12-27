using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class TilesGenerator : MonoBehaviour
{
    public Transform boardHolder;
    public Transform boardHolder2;
    public GameObject floorTile;
    public int columns = 8;
    public int rows = 8;
    int counter = 0;
    private List<ObjectPosition> objectPositionList = new List<ObjectPosition>();

    private List <Vector3> gridPositions = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        boardHolder = new GameObject("Board").transform;

        instantiateHall(rows, columns, 0, 0);
        var selectEdgeTiles = objectPositionList.Where(o => o.IsEdge == true).ToList();
        var chosenTile = selectEdgeTiles[Random.Range(0, selectEdgeTiles.Count)];
        var corridorEnd = instantiateCorridor(chosenTile);

        instantiateHall(rows, columns, corridorEnd.X, corridorEnd.Z);
        
        Debug.Log($"Counter is {chosenTile.Counter}, X is {chosenTile.X}, Z is {chosenTile.Z}");
        //instantiateCorridor()
    }

    private void instantiateHall(int columns, int rows, int xInitial, int zInitial) {
        var xStandardised = xInitial < 0 ? -1F : 1F;
        var zStandardised = zInitial < 0 ? -1F : 1F;
        int xInitStandardised = xInitial * (int)xStandardised;
        int zInitStandardised = zInitial * (int)zStandardised;

        for (int x = xInitStandardised; x < columns + xInitStandardised; x++) {
            for(int z = zInitStandardised; z < rows + zInitStandardised;  z++) {
                GameObject instance = Instantiate(floorTile, new Vector3(x * 1.1F * xStandardised, 0, z * 1.1F * zStandardised), Quaternion.identity) as GameObject;

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

    private ObjectPosition instantiateCorridor(ObjectPosition objectPosition) {
        GameObject instanceCorridor = null;
        var corridorSize = Random.Range(2, 5);
        float xVector = objectPosition.X;
        float zVector = objectPosition.Z;

        for(int i = 0; i <= corridorSize; i++) {
            switch(objectPosition.EdgeStance){
                case 3:
                    xVector = objectPosition.X + (i * -1);
                    break;
                case 2:
                    zVector = objectPosition.Z + (i * -1);
                    break;
                case 0:
                    zVector = objectPosition.Z + i;
                    break;
                case 1:
                    xVector = objectPosition.X + i;
                    break;
            }

            instanceCorridor = Instantiate(floorTile, new Vector3(xVector * 1.1F, 0, zVector * 1.1F), Quaternion.identity) as GameObject;
            instanceCorridor.transform.SetParent(boardHolder);
        }

        var corridorEnd = new ObjectPosition
        {
            X = (int)xVector,
            Z = (int)zVector,
            Counter = counter,
            ObjectInstance = instanceCorridor,
            IsEdge = true
        };
        Debug.Log($"Corridor: x is {xVector}, z is {zVector}");
        
        counter++;

        return corridorEnd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

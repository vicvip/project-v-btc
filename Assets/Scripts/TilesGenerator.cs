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

    void initialiseList() 
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        boardHolder = new GameObject("Board").transform;

        instantiateHall(rows, columns, 0, 0);

        var selectEdgeTiles = objectPositionList.Where(o => o.IsEdge == true).ToList();
        var chosenTile = selectEdgeTiles[Random.Range(0, selectEdgeTiles.Count)];
        
        instantiateCorridor(chosenTile);

        Debug.Log($"Counter is {chosenTile.Counter}, X is {chosenTile.X}, Z is {chosenTile.Z}");
        //instantiateCorridor()
    }

    private void instantiateHall(int columns, int rows, int xInitial, int zInitial) {
        for (int x = xInitial; x < columns + xInitial; x++) {
            for(int z = zInitial; z < rows + zInitial; z++) {
                GameObject instance = Instantiate(floorTile, new Vector3(x * 1.1F, 0, z * 1.1F), Quaternion.identity) as GameObject;

                var newPosition = new ObjectPosition
                {
                    X = x,
                    Z = z,
                    Counter = counter,
                    ObjectInstance = instance,
                    IsEdge = false
                };

                // if(x == xInitial || x == columns - 1 || z == zInitial || z == rows - 1) {
                //     newPosition.IsEdge = true;
                // }

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


                // if(x == 0 && z == 0) {
                //     instantiateCorridor(-1, 0);
                // }

                objectPositionList.Add(newPosition);
                instance.transform.SetParent(boardHolder);
                
                //Debug.Log($"x is {x}, z is {z}");
                counter++;

            }
        }
    }

    //private void instantiateCorridor(int row, int column) {
    private void instantiateCorridor(ObjectPosition objectPosition) {
        var corridorSize = Random.Range(1, 5);
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

            GameObject instanceCorridor = Instantiate(floorTile, new Vector3(xVector * 1.1F, 0, zVector * 1.1F), Quaternion.identity) as GameObject;
            instanceCorridor.transform.SetParent(boardHolder);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

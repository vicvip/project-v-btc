using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    public int columns = 8;
    public int rows = 8;
    public Transform prefab;
    public ArrayList groundContainer = new ArrayList();
    public Hashtable asd = new Hashtable();
    List<int[,]> testList = new List<int[,]>();
    Dictionary<int, int[,]> test = new Dictionary<int, int[,]>();
    Dictionary<int, int[][]> test1 = new Dictionary<int, int[][]>();
    List<string> test2 = new List<string>();
    int counter = 0;

    public List<Vector3> gridPosition = new List<Vector3>();
    public GameObject floorTile;
    public Transform boardHolder;

    private List<ObjectPosition> objectPositionList = new List<ObjectPosition>();

    // Use this for initialization
    void Start()
    {
        boardHolder = new GameObject("Board").transform;

        InstantiateHall(5, 5, 0, 0);
        var testHall = objectPositionList.Where(o => o.IsEdge == true && o.HasBeenSelected == false).ToList();
        var chosenPath = testHall[Random.Range(0, testHall.Count)];
        var edgesOnly = testHall.Where(t => t.HasBeenSelected = true).ToList();

        InstantiateHallway(edgesOnly[Random.Range(0, edgesOnly.Count)], 1, 5);

        var lastObject1 = objectPositionList.LastOrDefault();

        InstantiateHall(3, 3, lastObject1.X, lastObject1.Z);
        var testHall1 = objectPositionList.Where(o => o.IsEdge == true && o.HasBeenSelected == false).ToList();
        var chosenPath1 = testHall1[Random.Range(0, testHall.Count)];
        var edgesOnly1 = testHall1.Where(t => t.HasBeenSelected = true).ToList();
        
        InstantiateHallway(edgesOnly1[Random.Range(0, edgesOnly1.Count)], 1, 5);

        var lastObject2 = objectPositionList.LastOrDefault();
        InstantiateHall(4, 4, lastObject2.X, lastObject2.Z);

        // InstantiateHallway(testHall[Random.Range(0, testHall.Count)], 1, 10);
        // InstantiateHallway(testHall[Random.Range(0, testHall.Count)], 1, 9);
        // InstantiateHallway(testHall[Random.Range(0, testHall.Count)], 1, 8);
        // InstantiateHallway(testHall[Random.Range(0, testHall.Count)], 1, 5);

        //InstantiateHallway(objectPositionList[0], 1, 5);

        //x left, -1, 0, 0
        //x right 1, 0, 0
        //y up 0, 0, 1
        //y down 0, 0, -1

        //X axis going left 
        for (int x = 1; x <= 3; x++)
        {
            //Vector3 calculatedVector;
            //if (objectPositionList[1].EdgeStance == 0 || objectPositionList[1].EdgeStance == 2)
            //{
            //    var determinedPosition = objectPositionList[1].Y + x;
            //    calculatedVector = Vector3.Scale(new Vector3(2 * 1.1f, 0, determinedPosition * 1.1F), objectPositionList[1].ItsVector);
            //}
            //else
            //{
            //    var determinedPosition = objectPositionList[1].X + x;
            //    calculatedVector = Vector3.Scale(new Vector3(determinedPosition * 1.1f, 0, 2 * 1.1F), objectPositionList[1].ItsVector);
            //}

            ////var newX = objectPositionList[1].X + x;
            //GameObject instance = Instantiate(floorTile, calculatedVector, Quaternion.identity) as GameObject;

            //var newPosition = new ObjectPosition
            //{
            //    X = objectPositionList[1].X,
            //    Y = objectPositionList[1].Y,
            //    Counter = counter,
            //    ObjectInstance = instance
            //};

            //objectPositionList.Add(newPosition);
            //instance.transform.SetParent(boardHolder);

            //X axis going left 1
            //X axis going right 92
            //Y axis going up 19
            //Y axis going down 10
        }

        //InstantiateHall(10, -10, 5, 0);
        //for (int x = -5; x > -10; x--)
        //{
        //    for (int z = 0; z > -10; z--)
        //    {
        //        GameObject instance = Instantiate(floorTile, new Vector3(x * 1.1F, 0, z * 1.1F), Quaternion.identity) as GameObject;

        //        var newPosition = new ObjectPosition
        //        {
        //            X = x,
        //            Y = z,
        //            Counter = counter,
        //            ObjectInstance = instance
        //        }; 

        //        objectPositionList.Add(newPosition);
                
        //        instance.transform.SetParent(boardHolder);
        //        counter++;
        //    }
        //}

        foreach (var x in testList)
        {
            Debug.Log(x);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void InstantiateHallway(ObjectPosition objectPosition, int hallwayStart, int hallwayEnd)
    {
        var determinedPositionX = 0;
        var determinedPositionZ = 0;

        for (int i = hallwayStart; i <= hallwayEnd; i++)
        {
            Vector3 calculatedVector;
            if (objectPosition.EdgeStance == 0 || objectPosition.EdgeStance == 2)
            {
                determinedPositionZ = objectPosition.Z + i;
                calculatedVector = Vector3.Scale(new Vector3(objectPosition.X * 1.1f, 0, determinedPositionZ * 1.1F), objectPosition.ItsVector);
                determinedPositionX = objectPosition.X;
                if(objectPosition.EdgeStance == 2)
                {
                    determinedPositionZ = -determinedPositionZ;
                }
            }
            else
            {
                determinedPositionX = objectPosition.X + i;
                calculatedVector = Vector3.Scale(new Vector3(determinedPositionX * 1.1f, 0, objectPosition.Z * 1.1F), objectPosition.ItsVector);
                determinedPositionZ = objectPosition.Z;
                if (objectPosition.EdgeStance == 3)
                {
                    determinedPositionX = -determinedPositionX;
                }
            }

            GameObject instance = Instantiate(floorTile, calculatedVector, Quaternion.identity) as GameObject;

            var newPosition = new ObjectPosition
            {
                X = determinedPositionX,
                Z = determinedPositionZ,
                Counter = counter,
                ObjectInstance = instance
            };

            objectPositionList.Add(newPosition);
            instance.transform.SetParent(boardHolder);
        }
    }

    private void InstantiateHall(int column, int row, int xInitialPoint, int zInitialPoint)
    {
        
        if (xInitialPoint >= 0)
        {
            for (int x = xInitialPoint; x < column + xInitialPoint; x++)
            {
                ContinuationInstantiateHall(x, column, row, xInitialPoint, zInitialPoint);
            }
        }
        else 
        {
            for (int x = xInitialPoint; x > -column + xInitialPoint; x--)
            {
                ContinuationInstantiateHall(x, column, row, xInitialPoint, zInitialPoint);
            }
        }
    }

    private void ContinuationInstantiateHall(int x, int column, int row, int xInitialPoint, int zInitialPoint)
    {
        if (zInitialPoint >= 0)
        {
            for (int z = zInitialPoint; z < row + zInitialPoint; z++)
            {
                PopulateTiles(x, z, column, row, xInitialPoint, zInitialPoint);
            }
        }
        else
        {
            for (int z = zInitialPoint; z > -row + zInitialPoint; z--)
            {
                PopulateTiles(x, z, column, row, xInitialPoint, zInitialPoint);
            }
        }
    }

    
    private void PopulateTiles(int x, int z, int column, int row, int xInitialPoint, int zInitialPoint)
    {
        GameObject instance = Instantiate(floorTile, new Vector3(x * 1.1F, 0, z * 1.1F), Quaternion.identity) as GameObject;

        //MyScript script = obj.AddComponent<MyScript>();
        var newPosition = new ObjectPosition
        {
            X = x,
            Z = z,
            Counter = counter,
            ObjectInstance = instance,
            IsEdge = false
        };

        if (x == xInitialPoint || x == column || z == zInitialPoint || z == row)
        {
            if (x == xInitialPoint)
            {
                newPosition.EdgeStance = 3;
                newPosition.ItsVector = new Vector3(-1, 1, 1);
            }
            else if (x == column)
            {
                newPosition.EdgeStance = 1;
                newPosition.ItsVector = new Vector3(1, 1, 1);
            }
            else if (z == zInitialPoint)
            {
                newPosition.EdgeStance = 2;
                newPosition.ItsVector = new Vector3(1, 1, -1);
            }
            else if (z == row)
            {
                newPosition.EdgeStance = 0;
                newPosition.ItsVector = new Vector3(1, 1, 1);
            }
            newPosition.IsEdge = true;
        }

        objectPositionList.Add(newPosition);
        instance.transform.SetParent(boardHolder);
        counter++;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesGenerator : MonoBehaviour
{
    public Transform boardHolder;
    public Transform boardHolder2;
    public GameObject floorTile;
    public int columns = 8;
    public int rows = 8;

    private List <Vector3> gridPositions = new List<Vector3>();

    void initialiseList() 
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        boardHolder = new GameObject("Board").transform;
        boardHolder2 = new GameObject("Board2").transform;
        //floorTile.transform.position = new Vector3(1,0,1);

        for (int x = 0; x < columns; x++) {
            for(int z = 0; z < rows; z++) {
                GameObject instance = Instantiate(floorTile, new Vector3(x * 1.1F, 0, z * 1.1F), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }

        for (int x = 0; x < columns; x++) {
            for(int z = 0; z < rows; z++) {
                GameObject instance = Instantiate(floorTile, new Vector3(x * 1.1F, 0, z * 1.1F), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder2);
            }
        }

        
        boardHolder2.position = new Vector3(0, 0, 20);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGeneratedCode : MonoBehaviour
{
    [Tooltip("X width of the area that objects can be placed")]
    public static int X_VAL = 2;

    [Tooltip("Y width of the area that objects can be placed")]
    public static int Y_VAL = 2;

    [Tooltip("Size of each tile that the placed area is divied up into")]
    public Vector3 TileSize = new Vector3(1, 1, 1);

    [Tooltip("Array of objects holding the gameobjects that has been auto generated")]
    public static GameObject[,] ObjectsMade = new GameObject[X_VAL, Y_VAL];

    [Tooltip("List holding the types of objects that can be auto generated")]
    public List<ObjectScriptable> objects = new List<ObjectScriptable>();

    public ObjectScriptable air;

    /// <summary>
    /// random number that declared what type of object should be placed
    /// </summary>
    private int rngNbr;

    ///FOR DEBUGING PURPOSES ONLY
    [Tooltip("Shows the components in the Array of Objects Made")]

    private int xIndex = 0;
    private int yIndex = 0;

    private int zIndex = 0;
    public static int Z_VAL = 2;

    public static ObjectPlaced[,,] objM = new ObjectPlaced[X_VAL, Y_VAL, Z_VAL];
    private void Start()
    {
        StartCoroutine(LoopThroughElements());
    }

    private IEnumerator LoopThroughElements()
    {
        xIndex = yIndex = zIndex = 0;
        while (zIndex < Z_VAL)
        {
            while (yIndex < Y_VAL)
            {
                while (xIndex < X_VAL)
                {
                    //objM[xIndex, yIndex, zIndex].gameObject.GetComponent<ObjectPlaced>()
                    rngNbr = Random.Range(0, objects.Count + 1);
                    Check(rngNbr);
                    xIndex++;
                }
                xIndex = 0;
                yIndex++;
            }
            yIndex = 0;
            zIndex++;
        }
        yield return null;
    }

    private void Check(int rngNumStart)
    {
        Vector3 b = GetComponent<MeshRenderer>().bounds.min;
        Vector3 p = transform.position;
        Vector3 k = b - p;
        if (rngNbr >= objects.Count)
        {
            objM[xIndex, yIndex, zIndex] = null;
        }
        else if (objects[rngNbr].size.x >= TileSize.x && (xIndex + objects[rngNbr].size.x - 1 < X_VAL)
            && objects[rngNbr].size.y >= TileSize.y && (yIndex + objects[rngNbr].size.y - 1 < Y_VAL)
            && objects[rngNbr].size.z >= TileSize.z && (zIndex + objects[rngNbr].size.z - 1 < Z_VAL))
        {
            if (!IsOverlapping())
            {
                if (zIndex > 0 && objM[xIndex, yIndex, zIndex-1] != null || zIndex == 0 && objM[xIndex, yIndex, zIndex] == null)
                {
                    var obj = Instantiate(objects[rngNbr].model, (new Vector3(xIndex, zIndex, yIndex) + objects[rngNbr].offset) + k, Quaternion.identity, transform);
                    objM[xIndex, yIndex, zIndex] = obj.GetComponent<ObjectPlaced>();
                }

                //need to change to check based on size

                if (!objects[rngNbr].canBePlacedOnTopOf)
                {
                    CannotPlaceOnTop();
                }
                else
                {
                    Loops();
                }
            }
            else
            {
                rngNbr = (rngNbr + 1) % objects.Count;
                if (rngNbr == rngNumStart)
                {
                    return;
                }
                Check(rngNumStart);
            }
        }
        else
        {
            rngNbr = (rngNbr + 1) % objects.Count;
            if (rngNbr == rngNumStart)
            {
                return;
            }
            Check(rngNumStart);
        }

    }

    void CannotPlaceOnTop()
    {
        Vector3 endpts = new Vector3(xIndex + objects[rngNbr].size.x, yIndex + objects[rngNbr].size.y, Z_VAL);
        for (int x = xIndex; x < endpts.x; x++)
        {
            for (int y = yIndex; y < endpts.y; y++)
            {
                for (int z = zIndex; z < endpts.z; z++)
                {
                    objM[x, y, z] = objM[xIndex, yIndex, zIndex];
                    print(y);
                }
            }
        }
    }

    void Loops()
    {
        Vector3 endpts = new Vector3(xIndex + objects[rngNbr].size.x, yIndex + objects[rngNbr].size.y, zIndex + objects[rngNbr].size.z);
        for (int x = xIndex; x < endpts.x; x++)
        {
            for (int y = yIndex; y < endpts.y; y++)
            {
                for (int z = zIndex; z < endpts.z; z++)
                {
                    objM[x, y, z] = objM[xIndex, yIndex, zIndex];
                }
            }
        }
    }

    bool IsOverlapping()
    {
        if (objM[xIndex,yIndex,zIndex] != null)
        {
            return true;
        }
        return false;
    }

    bool IsObjectsMadeEmpty()
    {
        foreach (ObjectPlaced g in objM)
        {
            if (g != null)
            {
                return false;
            }
        }
        return true;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (IsObjectsMadeEmpty())
            {
                StopAllCoroutines();
                StartCoroutine(LoopThroughElements());
            }
            else
            {
                foreach (ObjectPlaced g in objM)
                {
                    if (g != null)
                        Destroy(g.gameObject);
                }
            }

        }
    }

    

    

    /*
    private void Start()
    {
        AutoGenerateObjects();
    }

    /// <summary>
    /// Loops through to add objects to the array
    /// </summary>
    private void AutoGenerateObjects()
    {
        xIndex = yIndex = 0;
        
        while (xIndex < X_VAL)
        {
            while (yIndex < Y_VAL)
            {
                rngNbr = Random.Range(0, objects.Count);
                Check(rngNbr);
                yIndex++;
            }
            yIndex = 0;
            xIndex++;
        }
        PrintObjectsMade();
    }

    #region Debuging

    bool IsObjectsMadeEmpty()
    {
        foreach (GameObject g in ObjectsMade)
        {
            if (g != null)
            {
                print(g);
                return false;
            }
        }
        return true;
    }

    private void PrintObjectsMade()
    {
        string k = "";
        for (int a = 0; a < X_VAL; a++)
        {
            for (int b = 0; b < Y_VAL; b++)
            {
                if (ObjectsMade[a, b] != null)
                    k += ObjectsMade[a, b].gameObject.name + " ";
                else
                    k += "null ";
            }
            currentObjects.Add(k);
            print(k);
            k = "";
        }
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.Space))
        {
            if (IsObjectsMadeEmpty())
            {
                currentObjects.Clear();
                AutoGenerateObjects();
            }
            else
            {
                foreach (GameObject g in ObjectsMade)
                {
                    if (g != null)
                        Destroy(g);
                }
            }
            
        }
    }

    #endregion

    /// <summary>
    /// Checks if random object to be placed can be placed and places object. Is recursive
    /// </summary>
    /// <param name="rngNumStart">Starting Random Number when method is invoked</param>
    private void Check(int rngNumStart)
    {
        if (objects[rngNbr].size.x >= TileSize.x && (xIndex + objects[rngNbr].size.x -1< X_VAL)
            && objects[rngNbr].size.y >= TileSize.y && (yIndex + objects[rngNbr].size.y -1< Y_VAL))
        {
            if (!IsOverlap())
            {
                ObjectsMade[xIndex, yIndex] = Instantiate(objects[rngNbr].model, new Vector3(xIndex, transform.position.y, yIndex) + objects[rngNbr].offset, Quaternion.identity, transform);
                Loops();
            }
            else
            {
                rngNbr = (rngNbr + 1) % objects.Count;
                if (rngNbr == rngNumStart)
                {
                    return;
                }
                Check(rngNumStart);
            }
        }
        else
        {
            rngNbr = (rngNbr + 1) % objects.Count;
            if (rngNbr == rngNumStart)
            {
                return;
            }
            Check(rngNumStart);
        }

    }

    /// <summary>
    /// Checks if object going to be made overlaps with any objects that are already made
    /// </summary>
    /// <returns>If Object going to be made is overlapping something</returns>
    private bool IsOverlap()
    {
        for (int a = xIndex; a < xIndex + objects[rngNbr].size.x; a++)
        {
            for(int b = yIndex; b < yIndex + objects[rngNbr].size.y; b++)
            {
                if (ObjectsMade[a, b] != null)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// Loops through to make sure no other objects can be placed in these areas determined by the object being placed size
    /// </summary>
    private void Loops()
    {
        Vector2 endpts = new Vector2(xIndex + objects[rngNbr].size.x, yIndex + objects[rngNbr].size.y);
        for(int x = xIndex; x < endpts.x; x++)
        {
            for (int y = yIndex; y < endpts.y; y++)
            {
                ObjectsMade[x, y] = ObjectsMade[xIndex, yIndex];
            }
        }
    }
    */

}
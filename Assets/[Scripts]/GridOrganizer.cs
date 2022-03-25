using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class SList<T>
{
    public List<T> subList;

    public SList()
    {
        subList = new List<T>();
    }

    public SList(IEnumerable<T> collection)
    {
        subList = new List<T>(collection);
    }

    public SList(int capacity)
    {
        subList = new List<T>(capacity);
    }
}

[System.Serializable]
public struct MatchImage
{
    public List<Sprite> assets;
}

public class GridOrganizer : MonoBehaviour
{
    public static GridOrganizer instance;

    [SerializeField]
    GameObject MatchObjPrefab;
    GridLayoutGroup gridLayout;
    [SerializeField]
    Vector2Int GridDimensions = new Vector2Int(6, 6);
    public MatchImage assetList;
    
    [SerializeField]
    public List<SList<MatchObj>> matchObjs;
    
    void Awake() 
    {
        instance = this;
        
        InstantiateMatchObjects();
        Populate2DList();
    }





    // Initial setup gathers and defines all data about match 3 objects
    void Populate2DList()
    {
        matchObjs = new List<SList<MatchObj>>();
        int cellCount = 0;
        for(int i = 0; i < GridDimensions.y; i++)
        {
            matchObjs.Add(new SList<MatchObj>());
            for(int j = 0; j < GridDimensions.x; j++)
            {
                matchObjs[i].subList.Add(transform.GetChild(cellCount).GetComponent<MatchObj>());
                cellCount++;
            }
        }
        Debug.Log("Generated Item Slots");
    }

    void InstantiateMatchObjects()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = GridDimensions.y;

        // Editing already existing children
        MatchObj[] objs = GetComponentsInChildren<MatchObj>();

        if(objs == null) Debug.Log("no prior child objects");
        
        if(objs != null)
        {
            foreach(MatchObj obj in objs)
            {
                int randomNum = Random.Range(0, assetList.assets.Count);
                obj.sprite = assetList.assets[randomNum];
            }
        }

        // creating more children using prefab to meet dimension requirements
        int numCells = GridDimensions.x * GridDimensions.y;
        while (transform.childCount < numCells)
        {
            GameObject newObject = Instantiate(MatchObjPrefab, this.transform);
            MatchObj instance = newObject.GetComponent<MatchObj>();
            int randomNum = Random.Range(0, assetList.assets.Count);
            // sets a random sprite for each match created
            instance.sprite = assetList.assets[randomNum];
            //instance.image.overrideSprite = assetList.assets[randomNum];
        }
    }
    
}

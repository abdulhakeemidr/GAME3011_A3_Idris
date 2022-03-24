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
    }

    void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = GridDimensions.y;

        int numCells = GridDimensions.x * GridDimensions.y;
        while (transform.childCount < numCells)
        {
            GameObject newObject = Instantiate(MatchObjPrefab, this.transform);
            MatchObj instance = newObject.GetComponent<MatchObj>();
            int randomNum = Random.Range(0, assetList.assets.Count);
            instance.image.overrideSprite = assetList.assets[randomNum];
        }

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


        /* matchObjs = new List<MatchObj>();
        int cellCount = 0;
        for(int i = 0; i < GridDimensions.y; i++)
        {
            matchObjs.Add(transform.GetChild(i).GetComponent<MatchObj>());
        }
        Debug.Log("Generated Item Slots"); */
    }

    // used only by custom list editor
    public void AddNewArray()
    {
        matchObjs.Add(new SList<MatchObj>());
    }
}

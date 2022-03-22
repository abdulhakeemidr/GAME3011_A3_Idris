using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridOrganizer : MonoBehaviour
{
    public static GridOrganizer instance;

    [SerializeField]
    GameObject MatchObjPrefab;
    GridLayoutGroup gridLayout;
    [SerializeField]
    Vector2Int GridDimensions = new Vector2Int(6, 6);

    void Awake() 
    {
        instance = this;
    }

    void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = GridDimensions.y;

        //slotState = ResourceSlotState.ExtractMode;

        int numCells = GridDimensions.x * GridDimensions.y;
        while (transform.childCount < numCells)
        {
            GameObject newObject = Instantiate(MatchObjPrefab, this.transform);
        }

    }
    /*
    [SerializeField]
    public List<List<ResourceSlotController>> resourceSlots;
    [SerializeField]
    public ResourceSlotState slotState;

    

    void Start()
    {
        gridLayout = GetComponent<GridLayoutGroup>();
        gridLayout.constraintCount = GridDimensions.y;


        slotState = ResourceSlotState.ExtractMode;
        

        int numCells = GridDimensions.x * GridDimensions.y;
        while (transform.childCount < numCells)
        {
            GameObject newObject = Instantiate(MatchObjPrefab, this.transform);
        }

        resourceSlots = new List<List<ResourceSlotController>>();
        int cellCount = 0;
        for(int i = 0; i < GridDimensions.y; i++)
        {
            resourceSlots.Add(new List<ResourceSlotController>());
            for(int j = 0; j < GridDimensions.x; j++)
            {
                resourceSlots[i].Add(transform.GetChild(cellCount).GetComponent<ResourceSlotController>());
                cellCount++;
            }
        }
        Debug.Log("Generated Item Slots");
    }


    // This function is called by the toggle button UI
    public void ToggleResourceState()
    {
        if(slotState == ResourceSlotState.ExtractMode)
        {
            slotState = ResourceSlotState.ScanMode;
        }
        else
        {
            slotState = ResourceSlotState.ExtractMode;
        }
    } */
}

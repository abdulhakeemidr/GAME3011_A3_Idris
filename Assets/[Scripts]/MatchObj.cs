using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MatchObj : MonoBehaviour
{
    public Image image;
    public Sprite sprite;
    Button button;
    [SerializeField]
    Vector2Int slotIndex;

    ImgProbability[] probability;

    int difficulty = 4;

    void Awake()
    {
        image = GetComponent<Image>();
        button = GetComponent<Button>();  
        //probability = new ImgProbability[difficulty];
    }

    void Start()
    {
        image.overrideSprite = sprite;
        FindThisMatchObjIndex();
        button.onClick.AddListener(PrintIndexOnClick);

        CheckHorizontalPeripheralSimilarity();
        CheckVerticalPeripheralSimilarity();
    }

    void FindThisMatchObjIndex()
    {
        bool isTerminated = false;
        for(int i = 0; i < GridOrganizer.instance.matchObjs.Count; i++)
        {
            //thisSlotIndex.y = i;
            // used to completely break out of nested for loop
            if(!isTerminated)
            {
                for(int j = 0; j < GridOrganizer.instance.matchObjs.Count; j++)
                {
                    slotIndex.y = j;
                    if(GridOrganizer.instance.matchObjs[i].subList[j] == this)
                    {
                        isTerminated = true;
                        break;
                    }
                }
            }
            else break;
            slotIndex.x = i;
        }
    }

    void PrintIndexOnClick()
    {
        Debug.Log(slotIndex.x + ", " + slotIndex.y);
    }

    MatchObj GetPeripheralMatch(Peripheral position, Vector2Int matchPosIndex)
    {
        MatchObj peripheralSlot = null;
        Vector2Int index = new Vector2Int();
        switch(position)
        {
            case Peripheral.TOP:
                index = slotIndex + new Vector2Int(0, -1);
                peripheralSlot = GridOrganizer.instance.matchObjs[index.x].subList[index.y];
            break;

            case Peripheral.BOTTOM:
                index = matchPosIndex + new Vector2Int(0, 1);
                peripheralSlot = GridOrganizer.instance.matchObjs[index.x].subList[index.y];
            break;

            case Peripheral.LEFT:
                index = matchPosIndex + new Vector2Int(-1, 0);
                peripheralSlot = GridOrganizer.instance.matchObjs[index.x].subList[index.y];
            break;

            case Peripheral.RIGHT:
                index = matchPosIndex + new Vector2Int(1, 0);
                peripheralSlot = GridOrganizer.instance.matchObjs[index.x].subList[index.y];
            break;

            case Peripheral.TOPLEFT:
                index = matchPosIndex + new Vector2Int(-1, -1);
                peripheralSlot = GridOrganizer.instance.matchObjs[index.x].subList[index.y];
            break;

            case Peripheral.TOPRIGHT:
                index = matchPosIndex + new Vector2Int(1, -1);
                peripheralSlot = GridOrganizer.instance.matchObjs[index.x].subList[index.y];
            break;

            case Peripheral.BOTTOMLEFT:
                index = matchPosIndex + new Vector2Int(-1, 1);
                peripheralSlot = GridOrganizer.instance.matchObjs[index.x].subList[index.y];
            break;

            case Peripheral.BOTTOMRIGHT:
                index = matchPosIndex + new Vector2Int(1, 1);
                peripheralSlot = GridOrganizer.instance.matchObjs[index.x].subList[index.y];
            break;
        }

        // peripheralSlot.m_displayedSlotColor.color = peripheralSlot.m_ScoreColor;
        // peripheralSlot.isScanned = true;
        //Debug.Log("Scanned Resource");
        return peripheralSlot;
    }

    void CheckHorizontalPeripheralSimilarity()
    {
        if(slotIndex.x == 0) return;
        if(slotIndex.x == GridOrganizer.instance.matchObjs.Count - 1) return;
        
        var leftSide = GetPeripheralMatch(Peripheral.LEFT, slotIndex);
        var rightSide = GetPeripheralMatch(Peripheral.RIGHT, slotIndex);

        if(leftSide.sprite == rightSide.sprite)
        {
            if(rightSide.sprite == this.sprite)
            {
                Debug.Log("Horizontal match at " + slotIndex.x + ", " + slotIndex.y
                + " is complete match");

                Color debugColor = new Color(1f, 1f, 1f, 0.2f);
                leftSide.image.color = debugColor;
                rightSide.image.color = debugColor;
                this.image.color = debugColor;
            }
        }
    }

    void CheckVerticalPeripheralSimilarity()
    {
        if(slotIndex.y == 0) return;
        if(slotIndex.y == GridOrganizer.instance.matchObjs.Count - 1) return;
        
        var topSide = GetPeripheralMatch(Peripheral.TOP, slotIndex);
        var bottomSide = GetPeripheralMatch(Peripheral.BOTTOM, slotIndex);

        if(topSide.sprite == bottomSide.sprite)
        {
            if(bottomSide.sprite == this.sprite)
            {
                Debug.Log("Vertical match at " + slotIndex.x + ", " + slotIndex.y
                + " is complete match");

                Color debugColor = new Color(1f, 1f, 1f, 0.2f);
                topSide.image.color = debugColor;
                bottomSide.image.color = debugColor;
                this.image.color = debugColor;
            }
        }
    }
}
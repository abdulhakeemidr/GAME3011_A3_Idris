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
    public Vector2Int slotIndex;

    void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        button = GetComponent<Button>();
    }

    void Start()
    {
        image.overrideSprite = sprite;
        FindThisMatchObjIndex();
        //button.onClick.AddListener(PrintIndexOnClick);

        FixRandomHorizontalSimilarity();
        FixRandomVerticalSimilarity();
        
        //Debug.Log(CheckHorizontalPeripheralSimilarity());
        //Debug.Log(CheckVerticalPeripheralSimilarity());

        CheckHorizontalPeripheralSimilarity();
        CheckVerticalPeripheralSimilarity();
    }

    void PrintIndexOnClick()
    {
        Debug.Log(slotIndex.x + ", " + slotIndex.y);
    }

    void FixRandomHorizontalSimilarity()
    {
        while(CheckHorizontalPeripheralSimilarity())
        {
            var leftSide = GetPeripheralMatch(Peripheral.LEFT, slotIndex);
            var rightSide = GetPeripheralMatch(Peripheral.RIGHT, slotIndex);
            // changes the sprite of this match3
            GridOrganizer.instance.AssignRandomSprite(this);

            leftSide.image.color = Color.white;
            rightSide.image.color = Color.white;
            this.image.color = Color.white;
        }
    }

    void FixRandomVerticalSimilarity()
    {
        while(CheckVerticalPeripheralSimilarity())
        {
            var topSide = GetPeripheralMatch(Peripheral.TOP, slotIndex);
            var bottomSide = GetPeripheralMatch(Peripheral.BOTTOM, slotIndex);

            GridOrganizer.instance.AssignRandomSprite(this);

            topSide.image.color = Color.white;
            bottomSide.image.color = Color.white;
            this.image.color = Color.white;
        }
    }

    bool CheckHorizontalPeripheralSimilarity()
    {
        if(slotIndex.x == 0) return false;
        if(slotIndex.x == GridOrganizer.instance.matchObjs.Count - 1) return false;
        
        var leftSide = GetPeripheralMatch(Peripheral.LEFT, slotIndex);
        var rightSide = GetPeripheralMatch(Peripheral.RIGHT, slotIndex);

        if(leftSide.sprite == rightSide.sprite)
        {
            if(rightSide.sprite == this.sprite)
            {
                //Debug.Log("Horizontal match at " + slotIndex.x + ", " + slotIndex.y
                //+ " is complete match");

                Color debugColor = new Color(1f, 1f, 1f, 0.2f);
                leftSide.image.color = debugColor;
                rightSide.image.color = debugColor;
                this.image.color = debugColor;

                return true;
            }
        }

        return false;
    }

    bool CheckVerticalPeripheralSimilarity()
    {
        if(slotIndex.y == 0) return false;
        if(slotIndex.y == GridOrganizer.instance.matchObjs.Count - 1) return false;
        
        var topSide = GetPeripheralMatch(Peripheral.TOP, slotIndex);
        var bottomSide = GetPeripheralMatch(Peripheral.BOTTOM, slotIndex);

        if(topSide.sprite == bottomSide.sprite)
        {
            if(bottomSide.sprite == this.sprite)
            {
                //Debug.Log("Vertical match at " + slotIndex.x + ", " + slotIndex.y
                //+ " is complete match");

                Color debugColor = new Color(1f, 1f, 1f, 0.2f);
                topSide.image.color = debugColor;
                bottomSide.image.color = debugColor;
                this.image.color = debugColor;

                return true;
            }
        }

        return false;
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
        return peripheralSlot;
    }

    void FindThisMatchObjIndex()
    {
        bool isTerminated = false;
        for(int i = 0; i < GridOrganizer.instance.matchObjs.Count; i++)
        {
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
}
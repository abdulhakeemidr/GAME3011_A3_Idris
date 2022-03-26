using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetDrop : MonoBehaviour
{
    [SerializeField]
    MatchObj movedObj;
    [SerializeField]
    MatchObj shiftedObj;

    GridOrganizer thisOrganizer;

    void Start()
    {
        thisOrganizer = GetComponent<GridOrganizer>();
    }
    
    public void GatherSwappedMatches(MatchObj moved, MatchObj newObj)
    {
        movedObj = moved;
        shiftedObj = newObj;
    }

    public void FindMatchOnMoved()
    {
        int XAxisFixed = movedObj.slotIndex.x;
        int YCount = thisOrganizer.matchObjs[XAxisFixed].subList.Count;

        for(int i = 0; i < YCount; i++)
        {
            var MatchIter = thisOrganizer.matchObjs[XAxisFixed].subList[i];
            GameObject child = MatchIter.transform.GetChild(0).gameObject;
            Image childImage = child.GetComponent<Image>();
            childImage.color = new Color(1f, 1f, 1f, 0.2f);
        }

        int YAxisFixed = movedObj.slotIndex.y;
        int XCount = thisOrganizer.matchObjs.Count;

        for(int i = 0; i < XCount; i++)
        {
            var MatchIter = thisOrganizer.matchObjs[i].subList[YAxisFixed];
            GameObject child = MatchIter.transform.GetChild(0).gameObject;
            Image childImage = child.GetComponent<Image>();
            childImage.color = new Color(1f, 1f, 1f, 0.2f);
        }
    }
}

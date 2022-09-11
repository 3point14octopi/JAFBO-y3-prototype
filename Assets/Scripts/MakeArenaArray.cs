using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Generation_Tools;
using UnityEngine.UI;
using TMPro;

public class MakeArenaArray : MonoBehaviour
{
    public Space2D arena;
    public TextMeshProUGUI iAmLessSad;
    public bool OhNo = true;
    // Start is called before the first frame update
    void Start()
    {
        arena = new Space2D(30, 30);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown)
        {
            if (OhNo)
            {
                ArenaGenFunctions_Prototype.ArenaPrototype(arena);
                PrintToTextMesh();
            }else
            {
                Space2D myEntireAss = new Space2D(28, 28);
                BasicBuilderFunctions.Flood(myEntireAss, new Cell(0), new Cell(1));
                myEntireAss.worldOrigin = new Coord(1, 1);
                BasicBuilderFunctions.CopySpaceAToB(myEntireAss, arena, new List<Cell> { });
                PrintToTextMesh();
            }
            OhNo = !OhNo;
        }
    }

    protected void PrintToTextMesh()
    {
        iAmLessSad.text = "\n";
        for(int i = 0; i < arena.height; i++)
        {
            for(int j = 0; j < arena.width; j++)
            {
                iAmLessSad.text =  iAmLessSad.text + (arena.GetCell(new Coord(j, i))) + " ";
            }
            iAmLessSad.text = iAmLessSad.text + "\n";
        }
    }
}

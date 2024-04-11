using UnityEngine;

public class PlayManager : MonoBehaviour
{
    // Offense 
    public GameObject QuarterB;
    public GameObject RunningB;
    public GameObject LeftT;
    public GameObject LeftG;
    public GameObject Center;
    public GameObject RightG;
    public GameObject RightT;
    public GameObject TEnd;
    public GameObject Wide1;
    public GameObject Wide2;
    public GameObject Wide3;
    public GameObject QBSH;
    public GameObject RBSH;
    public GameObject LT;
    public GameObject LG;
    public GameObject C;
    public GameObject RG;
    public GameObject RT;
    public GameObject TE;
    public GameObject WR1;
    public GameObject WR2;
    public GameObject WR3;

    // Defense
    public GameObject CornerB1;
    public GameObject CornerB2;
    public GameObject CornerB3;
    public GameObject DTackle1;
    public GameObject Dtackle2;
    public GameObject Safety1;
    public GameObject Safety2;
    public GameObject LineB1;
    public GameObject LineB2;
    public GameObject RightE;
    public GameObject LeftE;
    public GameObject CB1;
    public GameObject CB2;
    public GameObject CB3;
    public GameObject LB1;
    public GameObject LB2;
    public GameObject DT1;
    public GameObject DT2;
    public GameObject S1;
    public GameObject S2;
    public GameObject RE;
    public GameObject LE;

    // Plays
    public GameObject shotGunPositions;
    public GameObject pistolPositions;

    void Start()
    {
        // Choose random formation
        RandomFormation();
    }

    void ShotgunFormation()
    {
        shotGunPositions.SetActive(true);
        int formationChoice = Random.Range(1, 3);

        // Offense 
        Transform offenseParent = GameObject.Find("LineOfS/ShotgunPositions/Offense").transform;
        // Set QB and Running back positions
        if (formationChoice == 1)
        {
            QuarterB.transform.position = GameObject.Find("QBUC").transform.position;
            RunningB.transform.position = GameObject.Find("QBSH").transform.position;
        }
        else if (formationChoice == 2)
        {
            QuarterB.transform.position = GameObject.Find("QBSH").transform.position;
            RunningB.transform.position = GameObject.Find("RBSH").transform.position;
        }
        LeftT.transform.position = offenseParent.Find("LT").position;
        LeftG.transform.position = offenseParent.Find("LG").position;
        Center.transform.position = offenseParent.Find("C").position;
        RightG.transform.position = offenseParent.Find("RG").position;
        RightT.transform.position = offenseParent.Find("RT").position;
        TEnd.transform.position = offenseParent.Find("TE").position;
        Wide1.transform.position = offenseParent.Find("WR1").position;
        Wide2.transform.position = offenseParent.Find("WR2").position;
        Wide3.transform.position = offenseParent.Find("WR3").position;

        // Defense
        Transform defenseParent = GameObject.Find("LineOfS/ShotgunPositions/Defense").transform;
        CornerB1.transform.position = defenseParent.Find("CB1").position;
        CornerB2.transform.position = defenseParent.Find("CB2").position;
        CornerB3.transform.position = defenseParent.Find("CB3").position;
        DTackle1.transform.position = defenseParent.Find("DT1").position;
        Dtackle2.transform.position = defenseParent.Find("DT2").position;
        RightE.transform.position = defenseParent.Find("RE").position;
        LeftE.transform.position = defenseParent.Find("LE").position;
        Safety1.transform.position = defenseParent.Find("S1").position;
        Safety2.transform.position = defenseParent.Find("S2").position;
        LineB1.transform.position = defenseParent.Find("LB1").position;
        LineB2.transform.position = defenseParent.Find("LB2").position;
    }

    void PistolFormation()
    {
        pistolPositions.SetActive(true);
        int formationChoice = Random.Range(1, 3);

        // Offense 
        Transform offenseParent = GameObject.Find("LineOfS/PistolPositions/Offense").transform;
        // Set QB and Running back positions
        if (formationChoice == 1)
        {
            QuarterB.transform.position = GameObject.Find("QBUC").transform.position;
            RunningB.transform.position = GameObject.Find("QBSH").transform.position;
        }
        else if (formationChoice == 2)
        {
            QuarterB.transform.position = GameObject.Find("QBSH").transform.position;
            RunningB.transform.position = GameObject.Find("RBSH").transform.position;
        }
        LeftT.transform.position = offenseParent.Find("LT").position;
        LeftG.transform.position = offenseParent.Find("LG").position;
        Center.transform.position = offenseParent.Find("C").position;
        RightG.transform.position = offenseParent.Find("RG").position;
        RightT.transform.position = offenseParent.Find("RT").position;
        TEnd.transform.position = offenseParent.Find("TE").position;
        Wide1.transform.position = offenseParent.Find("WR1").position;
        Wide2.transform.position = offenseParent.Find("WR2").position;
        Wide3.transform.position = offenseParent.Find("WR3").position;

        // Defense
        Transform defenseParent = GameObject.Find("LineOfS/PistolPositions/Defense").transform;
        CornerB1.transform.position = defenseParent.Find("CB1").position;
        CornerB2.transform.position = defenseParent.Find("CB2").position;
        CornerB3.transform.position = defenseParent.Find("CB3").position;
        DTackle1.transform.position = defenseParent.Find("DT1").position;
        Dtackle2.transform.position = defenseParent.Find("DT2").position;
        RightE.transform.position = defenseParent.Find("RE").position;
        LeftE.transform.position = defenseParent.Find("LE").position;
        Safety1.transform.position = defenseParent.Find("S1").position;
        Safety2.transform.position = defenseParent.Find("S2").position;
        LineB1.transform.position = defenseParent.Find("LB1").position;
        LineB2.transform.position = defenseParent.Find("LB2").position;
    }

    void RandomFormation()
    {
        // Call random formation
        int formationChoice = Random.Range(1, 3);

        if (formationChoice == 1)
        {
            ShotgunFormation();
        }
        else if (formationChoice == 2)
        {
            PistolFormation();
        }
    }
}
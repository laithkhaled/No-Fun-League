using UnityEngine;

public class PlayManagerAway : MonoBehaviour
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
        Transform offenseParent = GameObject.Find("LineOfS_Away/ShotgunPositions/Offense").transform;
        // Set QB and Running back positions
        if (formationChoice == 1)
        {
            QuarterB.transform.position = GameObject.Find("QBUC_Away").transform.position;
            RunningB.transform.position = GameObject.Find("QBSH_Away").transform.position;
        }
        else if (formationChoice == 2)
        {
            QuarterB.transform.position = GameObject.Find("QBSH_Away").transform.position;
            RunningB.transform.position = GameObject.Find("RBSH_Away").transform.position;
        }
        LeftT.transform.position = offenseParent.Find("LT_Away").position;
        LeftG.transform.position = offenseParent.Find("LG_Away").position;
        Center.transform.position = offenseParent.Find("C_Away").position;
        RightG.transform.position = offenseParent.Find("RG_Away").position;
        RightT.transform.position = offenseParent.Find("RT_Away").position;
        TEnd.transform.position = offenseParent.Find("TE_Away").position;
        Wide1.transform.position = offenseParent.Find("WR1_Away").position;
        Wide2.transform.position = offenseParent.Find("WR2_Away").position;
        Wide3.transform.position = offenseParent.Find("WR3_Away").position;

        // Defense
        Transform defenseParent = GameObject.Find("LineOfS_Away/ShotgunPositions/Defense").transform;
        CornerB1.transform.position = defenseParent.Find("CB1_Home").position;
        CornerB2.transform.position = defenseParent.Find("CB2_Home").position;
        CornerB3.transform.position = defenseParent.Find("CB3_Home").position;
        DTackle1.transform.position = defenseParent.Find("DT1_Home").position;
        Dtackle2.transform.position = defenseParent.Find("DT2_Home").position;
        RightE.transform.position = defenseParent.Find("RE_Home").position;
        LeftE.transform.position = defenseParent.Find("LE_Home").position;
        Safety1.transform.position = defenseParent.Find("S1_Home").position;
        Safety2.transform.position = defenseParent.Find("S2_Home").position;
        LineB1.transform.position = defenseParent.Find("LB1_Home").position;
        LineB2.transform.position = defenseParent.Find("LB2_Home").position;
    }

    void PistolFormation()
    {
        pistolPositions.SetActive(true);
        int formationChoice = Random.Range(1, 3);

        // Offense 
        Transform offenseParent = GameObject.Find("LineOfS_Away/PistolPositions/Offense").transform;
        // Set QB and Running back positions
        if (formationChoice == 1)
        {
            QuarterB.transform.position = GameObject.Find("QBUC_Away").transform.position;
            RunningB.transform.position = GameObject.Find("QBSH_Away").transform.position;
        }
        else if (formationChoice == 2)
        {
            QuarterB.transform.position = GameObject.Find("QBSH_Away").transform.position;
            RunningB.transform.position = GameObject.Find("RBSH_Away").transform.position;
        }
        LeftT.transform.position = offenseParent.Find("LT_Away").position;
        LeftG.transform.position = offenseParent.Find("LG_Away").position;
        Center.transform.position = offenseParent.Find("C_Away").position;
        RightG.transform.position = offenseParent.Find("RG_Away").position;
        RightT.transform.position = offenseParent.Find("RT_Away").position;
        TEnd.transform.position = offenseParent.Find("TE_Away").position;
        Wide1.transform.position = offenseParent.Find("WR1_Away").position;
        Wide2.transform.position = offenseParent.Find("WR2_Away").position;
        Wide3.transform.position = offenseParent.Find("WR3_Away").position;

        // Defense
        Transform defenseParent = GameObject.Find("LineOfS_Away/PistolPositions/Defense").transform;
        CornerB1.transform.position = defenseParent.Find("CB1_Home").position;
        CornerB2.transform.position = defenseParent.Find("CB2_Home").position;
        CornerB3.transform.position = defenseParent.Find("CB3_Home").position;
        DTackle1.transform.position = defenseParent.Find("DT1_Home").position;
        Dtackle2.transform.position = defenseParent.Find("DT2_Home").position;
        RightE.transform.position = defenseParent.Find("RE_Home").position;
        LeftE.transform.position = defenseParent.Find("LE_Home").position;
        Safety1.transform.position = defenseParent.Find("S1_Home").position;
        Safety2.transform.position = defenseParent.Find("S2_Home").position;
        LineB1.transform.position = defenseParent.Find("LB1_Home").position;
        LineB2.transform.position = defenseParent.Find("LB2_Home").position;
    }

    public void RandomFormation()
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
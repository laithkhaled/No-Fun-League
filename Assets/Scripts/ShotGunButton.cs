using UnityEngine;
using UnityEngine.UI;


public class ShotGunButton : MonoBehaviour
{

   //Offense 
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

   //Defense
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

   //Button
   public Button button;
   
   void Start()
   {
       button.onClick.AddListener(shotgunFormation);
   }


   void shotgunFormation()
   {

       //Offense 
       QuarterB.transform.position = QBSH.transform.position;
       RunningB.transform.position = RBSH.transform.position;
       LeftT.transform.position = LT.transform.position;
       LeftG.transform.position = LG.transform.position;
       Center.transform.position = C.transform.position;
       RightG.transform.position = RG.transform.position;
       RightT.transform.position = RT.transform.position;
       TEnd.transform.position = TE.transform.position;
       Wide1.transform.position = WR1.transform.position;
       Wide2.transform.position = WR2.transform.position;
       Wide3.transform.position = WR3.transform.position;

       //Defense
       CornerB1.transform.position = CB1.transform.position;
       CornerB2.transform.position = CB2.transform.position;
       CornerB3.transform.position = CB3.transform.position;
       DTackle1.transform.position = DT1.transform.position;
       Dtackle2.transform.position = DT2.transform.position;
       RightE.transform.position = RE.transform.position;
       LeftE.transform.position = LE.transform.position;
       Safety1.transform.position = S1.transform.position;
       Safety2.transform.position = S2.transform.position;
       LineB1.transform.position = LB1.transform.position;
       LineB2.transform.position = LB2.transform.position;
   }
}


using UnityEngine;

public class TeamSwapper : MonoBehaviour
{

    public GameObject lineOfScrimmage;
    public GameObject lineOfScrimmageAway;

    public GameObject awayTeamObject;

    public GameObject homeTeamObject;

    private bool isHomeTeamWithBall = true;

    void Start()
    {
        //GameObject awayTeamObject = GameObject.Find("AwayTeam");
        //awayTeamObject = GameObject.Find("AwayTeam");
        awayTeamObject.SetActive(false);
    }

    public void SwapTeams()
    {
        //GameObject awayTeamObject = GameObject.Find("AwayTeam");
        //GameObject homeTeamObject = GameObject.Find("HomeTeam");
        isHomeTeamWithBall = !isHomeTeamWithBall;

        if (isHomeTeamWithBall)
        {
            homeTeamObject.SetActive(true);
            awayTeamObject.SetActive(false);
        }
        else
        {
            homeTeamObject.SetActive(false);
            awayTeamObject.SetActive(true);
        }

        Vector3 tempPosition = lineOfScrimmage.transform.position;
        lineOfScrimmage.transform.position = lineOfScrimmageAway.transform.position;
        lineOfScrimmageAway.transform.position = tempPosition;
    }
}
using UnityEngine;

public class TeamSwapper : MonoBehaviour
{

    public GameObject lineOfScrimmage;
    public GameObject lineOfScrimmageAway;

    public GameObject awayTeamObject;

    public GameObject homeTeamObject;

    public GameObject awayTeamDObject;

    public GameObject homeTeamDObject;

    private PlayManager playManager;
    private PlayManagerAway playManagerAway;

    private bool isHomeTeamWithBall = true;

    void Start()
    {
        //GameObject awayTeamObject = GameObject.Find("AwayTeam");
        //awayTeamObject = GameObject.Find("AwayTeam");
        awayTeamObject.SetActive(false);
        homeTeamDObject.SetActive(false);
        playManager = GameObject.FindObjectOfType<PlayManager>();
        playManagerAway = GameObject.FindObjectOfType<PlayManagerAway>();

    }

    public void SwapTeams()
    {
        //GameObject awayTeamObject = GameObject.Find("AwayTeam");
        //GameObject homeTeamObject = GameObject.Find("HomeTeam");
        Vector3 tempPosition = lineOfScrimmage.transform.position;
        lineOfScrimmage.transform.position = lineOfScrimmageAway.transform.position;
        lineOfScrimmageAway.transform.position = tempPosition;
        
        isHomeTeamWithBall = !isHomeTeamWithBall;

        if (isHomeTeamWithBall)
        {
            homeTeamObject.SetActive(true);
            awayTeamObject.SetActive(false);
            homeTeamDObject.SetActive(false);
            awayTeamDObject.SetActive(true);
            playManager.RandomFormation();
        }
        else
        {
            homeTeamObject.SetActive(false);
            awayTeamObject.SetActive(true);
            homeTeamDObject.SetActive(true);
            awayTeamDObject.SetActive(false);
            playManagerAway.RandomFormation();
        }
    }
}
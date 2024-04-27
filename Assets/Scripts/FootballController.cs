using UnityEngine;

public class FootballController : MonoBehaviour
{
    private GameManager gameManager;

    void Start()
    {
    gameManager = FindObjectOfType<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Receiver"))
        {
            this.transform.SetParent(collision.transform);
            this.transform.localPosition = Vector3.zero;
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;

            // Call CatchBall on the receiver's PlayerController script
            collision.gameObject.GetComponent<PlayerController>()?.CatchBall();
        }
        if (collision.gameObject.CompareTag("Bounds"))
        {
            Debug.Log("***************D*AS*D*AS*DAS*D*AS*DA*SD*AS*");
            Invoke("DelayRandomFormation", 4.5f);
            Destroy(gameObject);
        }
    }

    void DelayRandomFormation()
    {
        gameManager.CallRandomFormationBoth();
    }
}
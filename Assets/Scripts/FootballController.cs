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
        else if (collision.gameObject.CompareTag("Bounds"))
        {
            // Increase the down count when colliding with a boundary object
            if (gameManager != null)
            {
                gameManager.CallRandomFormation();
            }
            Destroy(gameObject);
        }
    }
}
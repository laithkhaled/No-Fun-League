using UnityEngine;
using System.Collections;

public class FootballController : MonoBehaviour
{
    private GameManager gameManager;
    private SpriteRenderer spriteRenderer; 

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>(); 
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

            spriteRenderer.enabled = false;
        }
        else if (collision.gameObject.CompareTag("Bounds") || collision.gameObject.CompareTag("Player"))
        {
            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            //Debug.Log("***************D*AS*D*AS*DAS*D*AS*DA*SD*AS*");
            StartCoroutine(DelayIncreaseCount());
        }
        else
        {
            spriteRenderer.enabled = true;
        }
    }

    IEnumerator DelayIncreaseCount()
    {
        yield return new WaitForSeconds(4.5f);
        gameManager.IncreaseDownCount();
        gameManager.CallRandomFormationBoth();
        Destroy(gameObject);
    }
}

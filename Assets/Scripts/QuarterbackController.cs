using UnityEngine;
using System.Collections;

public class QuarterbackController : MonoBehaviour
{
    public GameObject[] receivers; 
    public GameObject footballPrefab;
    public GameObject RHRadiusPrefab;
    public float duration = 8f;

    public Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            StartCoroutine(ThrowFootball());
            // Throw animation
            animator.SetBool("throwBall", true);
        }
    }

    IEnumerator ThrowFootball()
    {
        GameObject footballInstance = Instantiate(footballPrefab, transform.position, Quaternion.identity);
        footballInstance.transform.SetParent(transform); 

        if (receivers.Length > 0)
        {
            int index = Random.Range(0, receivers.Length);
            yield return new WaitForSeconds(Random.Range(1f, 2f));
            GameObject targetReceiver = receivers[index];
            Debug.Log("Receiver Chosen: " + targetReceiver.name);
            Vector3 throwDirection = (targetReceiver.transform.position - transform.position).normalized;
            float throwForce = 1000;
            footballInstance.GetComponent<Rigidbody2D>().AddForce(throwDirection * throwForce);
        }

        animator.SetBool("throwBall", false);

        yield return null;
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the collision is with a defensive player
        if (collision.CompareTag("Defense"))
        {
            // Instantiate the RHRadiusPrefab at the current position of this GameObject
            GameObject RHRadius = Instantiate(RHRadiusPrefab, transform.position, Quaternion.identity);

            // Destroy the radius after a set time
            Destroy(RHRadius, duration);
        }
    }
}
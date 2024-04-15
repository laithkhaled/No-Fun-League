using UnityEngine;
using System.Collections;

public class QuarterbackController : MonoBehaviour
{
    public GameObject[] receivers; 
    public GameObject footballPrefab; 
    private PlayerController playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        if (playerController != null) {
            playerController.hasBall = true;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && playerController.hasBall)
        {
            StartCoroutine(ThrowFootball());
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
            float throwForce = 900;
            footballInstance.GetComponent<Rigidbody2D>().AddForce(throwDirection * throwForce);
            playerController.hasBall = false;  
        }

        yield return null;
    }
}
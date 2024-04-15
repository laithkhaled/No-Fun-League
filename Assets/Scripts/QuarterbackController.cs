using UnityEngine;
using System.Collections;

public class QuarterbackController : MonoBehaviour
{
    public GameObject[] receivers; 
    public GameObject footballPrefab; 

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
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
            float throwForce = 1000;
            footballInstance.GetComponent<Rigidbody2D>().AddForce(throwDirection * throwForce);
        }

        yield return null;
    }
}
using UnityEngine;

public class FootballController : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Receiver"))
        {
            this.transform.SetParent(collision.transform);

            this.transform.localPosition = Vector3.zero;

            GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
}
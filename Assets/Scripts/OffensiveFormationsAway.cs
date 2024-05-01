using UnityEngine;

public class OffensiveFormationsAway : MonoBehaviour
{
    public GameObject Wide1;
    public GameObject Wide2;
    public GameObject Wide3;
    public GameObject TEnd;

    public AudioSource whistleSound; 
    public AudioClip whistleClip;

    private float playerSpeed; 
    private bool playerTackled;
    private bool isRunning = false;

    private void Start()
    {
        PlayerController player = GetComponentInChildren<PlayerController>();
        playerSpeed = player.speed;
        
    }

    private void Update()
    {
        //CheckIfTackled();
        if (Input.GetKeyDown(KeyCode.F) && !isRunning) //Can't click F when the play is happening
        {
            if(whistleSound && whistleClip)
                    whistleSound.PlayOneShot(whistleClip);
            // Randomly choose a play. Need to make it choose through Formations in the future.
            int randomPlay = Random.Range(1, 4);
            switch (randomPlay)
            {
                case 1:
                    StartCoroutine(Shotgun1());
                    break;
                case 2:
                    StartCoroutine(Shotgun2());
                    break;
                case 3:
                    StartCoroutine(Shotgun3());
                    break;
            }
        }
    }

    private bool CheckIfTackled(GameObject obj)
    {
        PlayerController player = obj.GetComponent<PlayerController>();
        playerTackled = player.isTackled;
        return playerTackled;
    }

    private System.Collections.IEnumerator Shotgun1()
    {
        isRunning = true;

        //Wide1 Runs a verticle
        MoveObject(Wide1, Vector3.left, 5f); 

        //Wide2 runs slant
        MoveObject(Wide2, Vector3.left, 2f); 
        MoveObject(Wide2, Vector3.up, 3f);

        //Wide 3 runs a curl
        MoveObject(Wide3, Vector3.left, 3f);
        MoveObject(Wide3, Vector3.right, 0.5f);

        //TEnd runs a stick
        MoveObject(TEnd, Vector3.left, 2f);
        MoveObject(TEnd, Vector3.down, 1f);

        // Wait for all objects to finish moving
        yield return new WaitForSeconds(5f);
        isRunning = false;
    }

    private System.Collections.IEnumerator Shotgun2()
    {
        isRunning = true;

        // Wide 1 runs drag
        MoveObject(Wide1, Vector3.left, 1.5f);
        MoveObject(Wide1, Vector3.down, 3.5f);

        // Wide2 & 3 run drags
        MoveObject(Wide3, Vector3.left, 1.5f);
        MoveObject(Wide2, Vector3.left, 1.5f);
        MoveObject(Wide3, Vector3.up, 3.5f);
        MoveObject(Wide2, Vector3.up, 3.5f);

        //TEnd Runs a vertical
        MoveObject(TEnd, Vector3.left, 5f);

        // Wait for all objects to finish moving
        yield return new WaitForSeconds(5f);
        isRunning = false;
    }

    private System.Collections.IEnumerator Shotgun3()
    {
        isRunning = true;

        //All Receivers run verticles.
        MoveObject(TEnd, Vector3.left , 5f);
        MoveObject(Wide1, Vector3.left , 5f);
        MoveObject(Wide2, Vector3.left , 5f);
        MoveObject(Wide3, Vector3.left , 5f);

        // Wait for all objects to finish moving
        yield return new WaitForSeconds(5f);
        isRunning = false;
    }

    // Coroutine to move object for specified duration
    private void MoveObject(GameObject obj, Vector3 direction, float duration)
    {
        StartCoroutine(MoveObjectCoroutine(obj, direction, duration));
    }

    private System.Collections.IEnumerator MoveObjectCoroutine(GameObject obj, Vector3 direction, float duration)
    {
        float timer = 0f;
        // Add conidition that stops players from moving
        while (timer < duration && !CheckIfTackled(obj))
        {
            //Debug.Log("*******" + (duration-timer));
            obj.transform.Translate(direction * playerSpeed * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }

}


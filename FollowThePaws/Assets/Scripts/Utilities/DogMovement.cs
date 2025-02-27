using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public Transform endPosition;
    void Start()
    {
        StartCoroutine(DogStartsWalking());
    }

    IEnumerator DogStartsWalking()
    {
        yield return new WaitForSeconds(2f);
        GetComponent<Animator>().SetBool("Walking",true);
        yield return null;
        while (true)
        {
            float step = 1f * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, endPosition.position, step);
            if (transform.position == endPosition.position)
            {
                break;
            }

            yield return null;
        }

        GetComponent<Animator>().SetBool("Walking",false);
        yield return null;
        transform.position += new Vector3(0, -0.18f, 0f);
        transform.Rotate(0f,183f+17f,0f,Space.Self);
        yield return null;
    }
}

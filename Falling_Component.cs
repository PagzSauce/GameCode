using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Falling_Component : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    private float fallDelay = 0.9f;
    private float destroyDelay = 2f;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            StartCoroutine(Fall());
        }
    }
 

    private IEnumerator Fall()
    {
        yield return new WaitForSeconds(fallDelay);
        _rb.bodyType = RigidbodyType2D.Dynamic;
        Destroy(gameObject, destroyDelay); 
    }
}

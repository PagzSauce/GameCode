using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakTrees : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Trees"))
        {
          
            Destroy(collision.gameObject);
        }
    }



}

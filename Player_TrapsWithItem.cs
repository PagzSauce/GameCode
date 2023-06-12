using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TrapsWithItem : MonoBehaviour
{
    [SerializeField] private GameObject _trees;
    float smooth = 10f;
    float targetAngle = 62;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("TreeCP"))
        {
            Quaternion target = Quaternion.Euler(0, 0, targetAngle);

            //Debug.Log("test");
            _trees.transform.rotation = Quaternion.Lerp (transform.rotation, target,  smooth);
            _trees.transform.position = new Vector3(_trees.transform.position.x, _trees.transform.position.y - 1);
        }
    }
}

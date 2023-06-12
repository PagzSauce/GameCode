using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ItemCollector : MonoBehaviour
{
    private int kiwi = 0;
    [SerializeField] private TMP_Text itemCount;
    [SerializeField] private AudioSource collectionSoundEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Kiwi"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            kiwi++;
            itemCount.text = kiwi.ToString();
            
        }
    }
}

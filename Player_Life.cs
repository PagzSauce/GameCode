using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class Player_Life : MonoBehaviour
{
    private Animator _anim;
    private Rigidbody2D _rb;
    [SerializeField] private AudioSource deathSoundEffect;
    [SerializeField] private Transform player;

    [SerializeField] private TMP_Text tsunamiDeaths;
    [SerializeField] private TMP_Text EQDeaths;
    [SerializeField] private TMP_Text ThunderDeaths;

    [SerializeField] private TMP_Text _thunderCollisionPercentage;
    [SerializeField] private TMP_Text _EQCollisionPercentage;
    [SerializeField] private TMP_Text _TsunamiCollisionPercentage;
    [SerializeField] private TMP_Text _totalDeaths;

    private string prevPLatformTag;
    private float _thunderFloat = 0;
    private float _EQFloat = 0;
    private float _tsunamiFloat= 0;



    [SerializeField] private AudioSource respawnSoundEffect;
    private Vector2 respawnPoint;

    private void Start()
   {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        respawnPoint = player.position;

   }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string _trapName =  collision.gameObject.name.ToString();
        
        if (collision.gameObject.CompareTag("Trap"))
        {
            float  _total = int.Parse(_totalDeaths.text);
            //Debug.Log(collision.gameObject.name);
            Debug.Log(_trapName);
            switch (_trapName)
            {
                
                case "Wave":
                    _total++;
                    tsunamiDeaths.text = (int.Parse(tsunamiDeaths.text.ToString()) + 1).ToString();
                   
                    break;
                case "Debris1":
                    _total++;
                    EQDeaths.text = (int.Parse(EQDeaths.text.ToString()) + 1).ToString();
                    break;
                case "Lightning":
                    _total++;
                   
                    ThunderDeaths.text = (int.Parse(ThunderDeaths.text.ToString()) + 1).ToString();
                  
                    break;
                case "Lightning2":
                    _total++;
                 
                    ThunderDeaths.text = (int.Parse(ThunderDeaths.text.ToString()) + 1).ToString();

                 
                    break;
                case "BottomTrap":
                    if (prevPLatformTag == "Falling Platform")
                    {
                        _total++;
                        EQDeaths.text = (int.Parse(EQDeaths.text.ToString()) + 1).ToString();
                    }

                    break;
                default:

                    break;
            }

            if((collision.gameObject.name != "BottomTrap") || (collision.gameObject.name == "BottomTrap" && prevPLatformTag == "Falling Platform"))
            {
                _totalDeaths.text = _total.ToString();
                RecomputePercentageDeath(_total);
            }

            Respawn();
        }
 
        prevPLatformTag = _trapName;

    }

    private void RecomputePercentageDeath(float totalDeaths)
    {
        //Tsunami Percentage Death
        _tsunamiFloat = float.Parse(tsunamiDeaths.text.ToString());
        _TsunamiCollisionPercentage.text = RoundOff((_tsunamiFloat / totalDeaths) * 100).ToString() + "%";


        //Earthquake Percentage Death
        _EQFloat = float.Parse(EQDeaths.text.ToString()) ;
        _EQCollisionPercentage.text = RoundOff((_EQFloat / totalDeaths) * 100).ToString() + "%";

        //Thunder Percentage Death
        _thunderFloat = float.Parse(ThunderDeaths.text.ToString());
        _thunderCollisionPercentage.text = RoundOff((_thunderFloat / totalDeaths) * 100).ToString() + "%";


    }

    public int RoundOff(float i)
    {
        return ((int)Math.Round(i / 10.0)) * 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Checkpoint"))
        {
            SetRespawnPoint(player.position);
            //SetRespawnPoint()
           // Debug.Log(player.position);
        }
        else if (collision.gameObject.CompareTag("FallingCP"))
        {
            SetRespawnPoint(new Vector3(player.position.x + 16f, player.position.y));
        }
        else if (collision.gameObject.CompareTag("FallingCP_Short"))
        {
            SetRespawnPoint(new Vector3(player.position.x + 8f, player.position.y));
        }
    }

    public void SetRespawnPoint(Vector3 position)
    {
        respawnPoint = (Vector2)position;
    }

   

    private void Respawn()
    {
        respawnSoundEffect.Play();
        player.position = respawnPoint;
    }

    private void Die()
    {
        deathSoundEffect.Play();
        _rb.bodyType = RigidbodyType2D.Static;
        _anim.SetTrigger("death");
    }

    private void RestartLeve()
    { 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
}

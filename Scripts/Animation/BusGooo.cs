using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusGooo : MonoBehaviour
{
    public Vector3 start, end;
    public float EnablePlayerAfter = 6f;
    public float speed = .1f;
    public GameObject busCam;


    GameObject player;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");

        if (PlayerPrefs.GetInt("bugbus", 0) == 0)
        {
            busCam.SetActive(true);
            start = transform.position;
            player.GetComponent<PlayerController>().StopMovement = true;
            player.transform.GetChild(0).gameObject.SetActive(false);
            player.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            Destroy(busCam);
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("bugbus", 0) == 0)
        {
            timer += Time.deltaTime;

            transform.position = Vector3.Lerp(start, end, speed * timer);

            if (timer > EnablePlayerAfter)
            {
                player.transform.GetChild(0).gameObject.SetActive(true);
                player.transform.GetChild(2).gameObject.SetActive(false);
            }

            if (Vector3.Distance(transform.position, end) < 10f)
            {
                player.GetComponent<PlayerController>().StopMovement = false;
                PlayerPrefs.SetInt("bugbus", 1);
                busCam.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}

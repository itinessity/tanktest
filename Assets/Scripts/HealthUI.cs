using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    Player player;

    public Text healthtext;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        var health = player.GetHealth();

        healthtext.text = $"{health.GetHeath()} / {health.MaxHealth}";
    }
}

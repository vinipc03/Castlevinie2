using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HudController : MonoBehaviour
{
    //[Header("Potions")]
    //[SerializeField] private Image hpPotion;
    //[SerializeField] private Image mpPotion;

    //[Header("Powers")]
    //[SerializeField] private Image holyBolt;
    //[SerializeField] private Image holySlash;
    
    [SerializeField] private Color selectColor;
    [SerializeField] private Color alphaColor;
    public List<Image> itensUI = new List<Image>();

    public PlayerController player;

    public Text hpPotionCount;
    public Text mpPotionCount;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //  itensUI[player.handlingObj].color = selectColor;
        for (int i = 0; i < itensUI.Count; i++)
        {
            if(i == player.handlingObj)
            {
                itensUI[i].color = selectColor;
            }
            else
            {
                itensUI[i].color = alphaColor;
            }
        }

        hpPotionCount.text = player.hpPotCount.ToString();
        mpPotionCount.text = player.mpPotCount.ToString();

    }
}

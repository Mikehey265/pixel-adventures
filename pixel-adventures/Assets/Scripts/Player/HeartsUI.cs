using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartsUI : MonoBehaviour
{
    #region Private Variables

    [SerializeField] Image[] hearts;
    [SerializeField] Sprite fullHeart;
    [SerializeField] Sprite emptyHeart;
    PlayerHealth player;
        
    #endregion

    #region Unity Methods
        
    private void Start() {
        SetHearts();
    }

    private void Update() {
        if(player == null){
            player = FindObjectOfType<PlayerHealth>();
        }
        
        UpdateHearts();
    }

    #endregion

    #region Private Methods

    private void SetHearts(){
        List<Image> allHearts = new List<Image>();

        foreach(Transform child in transform){
            allHearts.Add(child.gameObject.GetComponent<Image>());
        }

        hearts = allHearts.ToArray();
    }

    private void UpdateHearts(){
        if(player == null){return;}

        if(player.currentHealth > player.maxHealth){
            player.currentHealth = player.maxHealth;
        }

        for(int i = 0; i < hearts.Length; i++){
            if(i < player.currentHealth){
                hearts[i].sprite = fullHeart;
            }else{
                hearts[i].sprite = emptyHeart;
            }

            if(i < player.maxHealth){
                hearts[i].enabled = true;
            }else{
                hearts[i].enabled = false;
            }
        }
    }
        
    #endregion
}

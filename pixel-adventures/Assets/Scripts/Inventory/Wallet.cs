using UnityEngine;
using TMPro;

public class Wallet : MonoBehaviour
{
    int currentRupees;

    [SerializeField] TextMeshProUGUI rupeeText;

    private void Update() {
        UpdateRupeeText();
    }

    public void UpdateRupeeText(){
        rupeeText.text = "" + currentRupees;
    }

    public void IncreaseRupeeCount(int amount){
        currentRupees += amount;
    }
}

using System;
using UnityEngine;

public class Pickups : MonoBehaviour
{
    public enum TypeOfPickUp{Rupee, Bomb};
    public TypeOfPickUp typeOfPickUp;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            Destroy(gameObject);

            if(typeOfPickUp == TypeOfPickUp.Rupee){
                PickUpRupee();
            }
        }
    }

    private void PickUpRupee()
    {
        FindObjectOfType<Wallet>().IncreaseRupeeCount(1);
    }
}

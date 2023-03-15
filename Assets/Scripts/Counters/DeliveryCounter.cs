using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : BaseCounter
{
    public override void Interact(Player player)
    {
        if (player.HasKitchenObject())
        {
            //Player has a kitchen object
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                //Only accepts plates

                DeliveryManager.Instance.DelieryRecipe(plateKitchenObject);
                
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}

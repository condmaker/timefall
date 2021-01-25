using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirectItemInteractor : ManualInteractor
{
    [SerializeField]
    private ItemStatePar[] par;

    public override InteractionResult Toggle(ItemData itemId, Vector3 position)
    {
        foreach(ItemStatePar i in par)
        {
            if (i.ID == itemId.ID)
            {
                ProcessResult(i.State);
                return InteractionResult.UseItem;

            }
        }

        return InteractionResult.WrongIntMessage;

    }


}

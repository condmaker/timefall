using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManualInteractor : Interactor
{
    public abstract InteractionResult Toggle(ItemData itemId, Vector3 position);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ManualInteractor : Interactor
{
    public abstract bool Toggle(ItemData itemId, Vector3 position);
}

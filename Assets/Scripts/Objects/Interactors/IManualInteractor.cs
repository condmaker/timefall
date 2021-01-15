using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IManualInteractor : IInteractor
{
    bool Toggle(ItemData itemId);
}

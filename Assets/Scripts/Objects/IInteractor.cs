using System;

public interface IInteractor
{
    event Action OnGoToFirst;
    event Action OnGoToLast;
    event Action OnGoToNext;
}

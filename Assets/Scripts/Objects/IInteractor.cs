using System;

public interface IInteractor
{
    event Action OnGoToLast;
    event Action OnGoToNext;
    event Action<short> OnGoTo;
}

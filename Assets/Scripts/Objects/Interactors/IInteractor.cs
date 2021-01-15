using System;

public interface IInteractor
{
    event Action OnGoToLast;
    event Action<IterationType> OnGoToNext;
    event Action<short> OnGoTo;
}


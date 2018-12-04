using System;

namespace DinnergeddonUI.Interfaces
{
    public interface ICloseable
    {
        event EventHandler<EventArgs> RequestClose;
    }
}

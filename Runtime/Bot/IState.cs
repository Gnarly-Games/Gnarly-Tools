using System;

namespace Bot
{
    public interface IState
    {
        bool Lock { get; }
        void Start();
        void Refresh();
        void Update();
        bool Check(); 
        void Stop();
    }
}
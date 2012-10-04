using System;

namespace CQRS
{
    public interface IHandle
    {
        Type CommandType { get; }
        void Handle(Command cmd);
    }
}
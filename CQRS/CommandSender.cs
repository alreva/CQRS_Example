using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public class CommandSender : ICommandSender
    {
        private readonly IDictionary<Type, IHandle> _commandHandlers;

        public CommandSender(params IHandle[] commandHandlers)
        {
            _commandHandlers = commandHandlers.ToDictionary(cmdH => cmdH.CommandType);
        }

        public void Send(Command cmd)
        {
            _commandHandlers[cmd.GetType()].Handle(cmd);
        }
    }
}

using System;
using System.Linq;

namespace CQRS
{
    public abstract class CommandHandler<TAggregateRoot, TCommand> : IHandle
        where TCommand : Command where TAggregateRoot : AggregateRoot
    {
        private readonly IRepository<TAggregateRoot> _repository;

        public CommandHandler(
            IRepository<TAggregateRoot> repository)
        {
            _repository = repository;
        }

        public Type CommandType { get { return typeof (TCommand); } }
        public void Handle(Command cmd)
        {
            if (!(cmd is TCommand))
            {
                throw new ArgumentException("cmd");
            }

            var specificCommand = (TCommand) cmd;
            var aggregateRootId = GetAggregateRootId(specificCommand);

            var aggrerateRoot = _repository.Get(aggregateRootId);

            DoDomainLogicWith(aggrerateRoot, specificCommand);

            _repository.Save(aggrerateRoot);
        }

        protected abstract string GetAggregateRootId(TCommand cmd);
        protected abstract void DoDomainLogicWith(TAggregateRoot aggrerateRoot, TCommand cmd);
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CQRS;

namespace Model
{
    public class AddCategoryHandler : CommandHandler<Category,AddCategory>
    {
        public AddCategoryHandler(IRepository<Category> repository, IEventPublisher eventPublisher)
            : base(repository)
        {
        }

        protected override string GetAggregateRootId(AddCategory cmd)
        {
            return cmd.Id;
        }

        protected override void DoDomainLogicWith(Category aggrerateRoot, AddCategory cmd)
        {
            aggrerateRoot.AddNew(cmd.Id, cmd.Title, cmd.ParentId);
        }
    }
}

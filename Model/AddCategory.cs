using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CQRS;

namespace Model
{
    public class AddCategory : Command
    {
        public AddCategory(string id, string title, string parentId)
        {
            Id = id;
            Title = title;
            ParentId = parentId;
        }

        public string Id { get; private set; }
        public string Title { get; private set; }
        public string ParentId { get; private set; }
    }
}

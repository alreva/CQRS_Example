using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS
{
    public abstract class AggregateRoot
    {
        private readonly List<Event> _changes = new List<Event>(); 

        protected void ApplyAndAddChange(Event evt)
        {
            this.AsDynamic().Apply(evt);
            _changes.Add(evt);
        }

        public string Id { get; protected set; }
        public IEnumerable<Event> Changes { get { return _changes.AsReadOnly(); } } 
    }
}

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadModel
{
    public interface IGetCatalogTreeQueryService
    {
        CategoryDto GetCatalogTree();
    }
}

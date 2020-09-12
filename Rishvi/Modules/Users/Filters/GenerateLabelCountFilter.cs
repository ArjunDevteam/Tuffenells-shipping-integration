using Rishvi.Modules.Core.Filters;
using Rishvi.Modules.Users.Models;
using Rishvi.Modules.Users.Models.DTOs;
using System.Linq;

namespace Rishvi.Modules.Core.Filters
{
    public class GenerateLabelCountFilter : BaseFilter<GeneratelabelLog, Users.Models.DTOs.GenerateLabelFilterDto>
    {
        public GenerateLabelCountFilter(IQueryable<GeneratelabelLog> query, Users.Models.DTOs.GenerateLabelFilterDto dto) : base(query, dto) { }

        internal void WhereOrderId()
        {
            Query = Query.Where(w => w.Orderid == Dto.OrderId);
        }
    }
}

using Rishvi.Modules.Core.Filters;
using Rishvi.Modules.Core.ListOrders;
using Rishvi.Modules.Users.Models;
using System.Linq;

namespace Rishvi.Modules.Users.ListOrders
{
    public class UserListOrder : BaseListOrder<User>
    {
        public UserListOrder(IQueryable<User> query, BaseFilterDto dto) : base(query, dto)
        {
        }

        internal void Name()
        {
            Query = OrderBy(t => t.Name);
        }

        internal void IsActive()
        {
            Query = OrderBy(t => t.IsActive);
        }

        internal void CreatedAt()
        {
            Query = OrderBy(t => t.CreatedAt);
        }

        internal void UpdatedAt()
        {
            Query = OrderBy(t => t.UpdatedAt);
        }
    }
}
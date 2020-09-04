using Microsoft.EntityFrameworkCore;
using Rishvi.Modules.Core.Filters;
using Rishvi.Modules.Users.Models;
using System.Linq;

namespace Rishvi.Modules.Users.Filters
{
    public class UserFilter : BaseFilter<User, AddNewRequest>
    {
        public UserFilter(IQueryable<User> query, AddNewRequest dto) : base(query, dto) { }

        internal void Name()
        {
            Query = Query.Where(w => w.Name.Contains(Dto.Name));
        }

        internal void IsActive()
        {
            Query = Query.Where(w => w.IsActive == Dto.IsActive);
        }

        internal void FromCreatedAt()
        {
            Query = Query.Where(w => w.CreatedAt.Date >= Dto.FromCreatedAt.Date);
        }

        internal void ToCreatedAt()
        {
            Query = Query.Where(w => w.CreatedAt.Date <= Dto.ToCreatedAt.Date);
        }

        internal void FromUpdatedAt()
        {
            Query = Query.Where(w => w.UpdatedAt.Date >= Dto.FromUpdatedAt.Date);
        }

        internal void ToUpdatedAt()
        {
            Query = Query.Where(w => w.UpdatedAt.Date <= Dto.ToUpdatedAt.Date);
        }
    }
}
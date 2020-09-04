using Rishvi.Modules.Core.Extensions;
using Rishvi.Modules.Core.Filters;
using Spinx.Web.Modules.Core.Extensions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Rishvi.Modules.Core.ListOrders
{
	public class BaseListOrder<TEntity> where TEntity : new()
	{
		protected IQueryable<TEntity> Query;
		private readonly string _defaultSortColumn;
		private readonly SortType _defaultSortType;
		protected string SortColumn;
		protected SortType SortType;

		protected BaseListOrder(IQueryable<TEntity> query, BaseFilterDto dto, 
			string defaultSortColumn = "CreatedAt", SortType defaultSortType = SortType.Desc)
		{
			Query = query;
			_defaultSortColumn = defaultSortColumn;
			_defaultSortType = defaultSortType;

			SortColumn = SetSortColumn(dto?.SortColumn ?? _defaultSortColumn);
			SortType = SetSortType(dto?.SortType ?? _defaultSortType.ToString());
		}

		private string SetSortColumn(string sortColumn)
		{
			var methodInfos = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

			var methodName = methodInfos.FirstOrDefault(w => w.Name.ToLower() == sortColumn.NullSafeToLower())?.Name;

			return methodName ?? _defaultSortColumn;
		}

		private SortType SetSortType(string sortType)
		{
			switch (sortType.ToLower())
			{
				case "asc":
					return SortType.Asc;
				case "desc":
					return SortType.Desc;
			}

			return _defaultSortType;
		}

		internal IQueryable<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector)
		{
			return SortType == SortType.Asc ? 
				Query.OrderBy(keySelector) :
				Query.OrderByDescending(keySelector);
		}

		public IQueryable<TEntity> OrderByQuery()
		{
			var methodInfos = GetType().GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);

			var methodInfo = methodInfos.FirstOrDefault(w => w.Name == SortColumn);

			if (methodInfo == null)
				throw new Exception($"Invalid order by column {SortColumn} in {typeof(TEntity).Name}.");

			methodInfo.Invoke(this, null);

			return Query;
		}
	}
}
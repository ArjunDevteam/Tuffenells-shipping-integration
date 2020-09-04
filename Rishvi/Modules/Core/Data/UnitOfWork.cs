namespace Rishvi.Modules.Core.Data
{
    public interface IUnitOfWork
    {
        RishviDbContext Context { get; }
        void Commit();
    }
    public class UnitOfWork : IUnitOfWork
    {
        public RishviDbContext Context { get; }

        public UnitOfWork(RishviDbContext context)
        {
            Context = context;
        }
        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}

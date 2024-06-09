using AnhDevGa.Core.SeedWorks;

namespace AnhDevGa.Data.SeedWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AnhDevGaContext _context;
        public UnitOfWork(AnhDevGaContext context)
        {
            _context = context;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

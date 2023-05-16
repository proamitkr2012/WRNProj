using System;

namespace AdmissionRepo
{
    public interface IUnitOfWork : IDisposable
    {
        AdminRepository IAdmin { get; }
        int SaveChanges();
        public IstudentPreRepo studentPreRepo { get; }
    }
}
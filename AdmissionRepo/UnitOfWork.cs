using AdmissionData;
using AdmissionData.Dapper;
using AdmissionRepo.Utilities;
using System;

namespace AdmissionRepo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext db;
         private IMailClient _mailClient; IDapperContext _dapperContext;
        public UnitOfWork(DataContext _db, IDapperContext dapperContext, IMailClient mailClient)
        {
            db = _db;
            _dapperContext = dapperContext;
        }

        private AdminRepository _IAdmin;
        public AdminRepository IAdmin
        {
            get
            {
                if (this._IAdmin == null)
                {
                    this._IAdmin = new AdminRepository(db, _dapperContext);
                }
                return this._IAdmin;
            }
        }


        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        public void Dispose()
        {
            db.Dispose();
        }
    }
}

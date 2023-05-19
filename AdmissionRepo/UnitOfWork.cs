﻿using AdmissionData;
using AdmissionData.Dapper;
using AdmissionRepo.Utilities;
using System;

namespace AdmissionRepo
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext db;
        private IMailClient _mailClient; IDapperContext _dapperContext;
        private readonly IstudentPreRepo _istudentPreRepo;
        private readonly IMasterRepo _imasterRepo;
        private readonly IStudentQualificationRepo _iqulificationRepo;
        private readonly IStdWeightageRep _iStdWeightageRep;
        private readonly IStudentApplyCourseRepo _istudentApplyCourse;
        public UnitOfWork(DataContext _db, IDapperContext dapperContext, IMailClient mailClient, IstudentPreRepo istudentPreRepo, IMasterRepo imasterRepo, IStudentQualificationRepo iqulificationRepo
            , IStdWeightageRep iStdWeightageRep, IStudentApplyCourseRepo istudentApplyCourse)
        {
            db = _db;
            _dapperContext = dapperContext;
            _istudentPreRepo = istudentPreRepo;
            _imasterRepo = imasterRepo;
            _iqulificationRepo = iqulificationRepo;
            _iStdWeightageRep = iStdWeightageRep;
            _istudentApplyCourse = istudentApplyCourse;
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

        public IstudentPreRepo studentPreRepo
        {
            get
            {
                return this._istudentPreRepo;
            }
        }




        public IMasterRepo masterRepo
        {
            get
            {
                return _imasterRepo;
            }

        }

        public IStudentQualificationRepo qulificationRepo
        {
            get
            {
               return _iqulificationRepo;
            }
        }

        public IStdWeightageRep StdWeightageRep
        {
            get { return _iStdWeightageRep; }

        }

        public IStudentApplyCourseRepo studentApplyCourse
        {
            get { return _istudentApplyCourse; }

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

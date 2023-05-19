﻿using System;

namespace AdmissionRepo
{
    public interface IUnitOfWork : IDisposable
    {
        AdminRepository IAdmin { get; }
        int SaveChanges();
        public IstudentPreRepo studentPreRepo { get; }
        public IMasterRepo masterRepo { get; }
        public IStudentQualificationRepo qulificationRepo { get; }

        public IStdWeightageRep  StdWeightageRep { get; }

        public  IStudentApplyCourseRepo studentApplyCourse { get; }
    }
}
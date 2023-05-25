
using AdmissionRepo;
using DataAccess.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using SMS_MAIL.GateWay;

namespace ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<ISMS_MAIL, SMS_MAILAPP>();
            services.AddScoped<IstudentPreRepo, StudentPreRegRepo >();
            services.AddScoped<IMasterRepo, MasterRepo>();
            services.AddScoped<IStudentQualificationRepo, StudentQualificationRepo>();
            services.AddScoped<IStdWeightageRep, StdWeightageRep>();
            services.AddScoped<IStudentApplyCourseRepo, StudentApplyCourseRepo>();
            services.AddScoped<IstudentApplyCollegeRepo, studentApplyCollegeRepo > ();
         



        }
    }
}

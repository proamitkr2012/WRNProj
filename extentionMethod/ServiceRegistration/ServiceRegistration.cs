
using AdmissionRepo;
using DataAccess.Infrastructure;
using Microsoft.Extensions.DependencyInjection;


namespace ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            //services.AddScoped<IstudentPreRepo, StudentPreRegRepo>();
            services.AddScoped<IstudentPreRepo, StudentPreRegRepo >();
            services.AddScoped<IMasterRepo, MasterRepo>();
            services.AddScoped<IStudentQualificationRepo, StudentQualificationRepo>();
            services.AddScoped<IStdWeightageRep, StdWeightageRep>();
            services.AddScoped<IStudentApplyCourseRepo, StudentApplyCourseRepo>();
            services.AddScoped<IstudentApplyCollegeRepo, studentApplyCollegeRepo > ();
         



        }
    }
}

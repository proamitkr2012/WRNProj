﻿using AdmissionData.Entities;
using AdmissionModel;
using Dapper;
using DataAccess.Infrastructure;
using Microsoft.Extensions.Logging;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace AdmissionRepo
{
    public class StudentApplyCourseRepo : IStudentApplyCourseRepo
    {


        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<StudentApplyCourseRepo> _logger;

        public StudentApplyCourseRepo(IConnectionFactory connectionFactory, ILogger<StudentApplyCourseRepo> logger)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
        }
        public async  Task<int> AddAsync(StudentAppliedCourse entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "InsertStudentApplyCourse";
                    var param = new DynamicParameters();
                    param.Add("@ApllicationNo", entity.ApllicationNo);
                    param.Add("@CourseId", entity.CourseId);
                    param.Add("@IsFor_Paid", entity.IsFor_Paid);
                    var rowsInserted = await SqlMapper.ExecuteAsync(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return -1;
            }
        }


        public async  Task<IEnumerable<StudentAppliedCourse>> GetAllAsyncByAppNo(string appno)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "selectStudentAppliedCourse";
                    var param = new DynamicParameters();
                    param.Add("@ApllicationNo", appno);
                    var list = await SqlMapper.QueryAsync<StudentAppliedCourse>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return list;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return null;
            }



        }









        public Task<int> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async  Task<int> DeleteAsync(string id,int courseid)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "DeleteStudentChooseCourse";
                    var param = new DynamicParameters();
                    param.Add("@ApllicationNo", id);
                    param.Add("@CourseId", courseid);
                    var rowsInserted = await SqlMapper.ExecuteAsync(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return -1;
            }
        }


        
        public Task<IEnumerable<StudentAppliedCourse>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

       
        public Task<StudentAppliedCourse> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(StudentAppliedCourse entity)
        {
            throw new NotImplementedException();
        }

        public async  Task<int> updateCourseFeesPaymentStatus(StudentAppliedCourse entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "UpdateFeesCoursePayment";
                    var param = new DynamicParameters();
                    param.Add("@ApllicationNo", entity.ApllicationNo);
                    param.Add("@CourseId", entity.CourseId);
                    param.Add("@transId", entity.TransactionID);
                    param.Add("@Ispaid", entity.IsPaid);
                    var rowsInserted = await SqlMapper.ExecuteAsync(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return -1;
            }
        }

        
        public async  Task<string> genrateGrpFeesforPayment(string appno, int courseId,double amt)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {
            try
             {
                var query = "GenrateGroupNo";
                var param = new DynamicParameters();
                param.Add("@ApplicationNo", appno);
                param.Add("@CourseId", courseId);
                param.Add("@Amount", amt);
                    
                var rowsInserted = await SqlMapper.QuerySingleOrDefaultAsync<string>(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                connection.Close();
                return rowsInserted;
             }
            catch (Exception ex)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }

            return "";
        }

        }


        public async Task<int> updateCourseFeesGrnStatus(StudentAppliedCourse entity)
        {
            using (IDbConnection connection = _connectionFactory.GetConnection)
            {

                try
                {
                    var query = "UpdateGRNCoursePayment";
                    var param = new DynamicParameters();
                    param.Add("@ApllicationNo", entity.ApllicationNo);
                    param.Add("@CourseId", entity.CourseId);
                    param.Add("@transId", entity.TransactionID);
                    var rowsInserted = await SqlMapper.ExecuteAsync(connection, query, param, commandType: System.Data.CommandType.StoredProcedure);
                    connection.Close();
                    return rowsInserted;
                }
                catch (Exception ex)
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }

                return -1;
            }
        }








    }
}

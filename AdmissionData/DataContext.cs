using AdmissionData.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace AdmissionData
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Role> Roles{ get; set; }
        public DbSet<AdminMaster> AdminMasters { get; set; }
        public DbSet<StudentPreData> StudentPreData { get; set; }
        public DbSet<AdminMasterRoles> AdminMasterRoles { get; set; }
        public DbSet<tblState> tblState { get; set; }
        public DbSet<tblDistrict> tblDistrict { get; set; }
        public DbSet<StudentMasters> StudentMasters { get; set; }
        public DbSet<DocSetting> DocSetting { get; set; }
        public DbSet<QualifationMasters> QualifationMasters { get; set; }
        public DbSet<StudentDocUploaded> StudentDocUploaded { get; set; }
        public DbSet<CollegeMasters> CollegeMasters { get; set; }

        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserRoles>().HasKey(e => new { e.StudId, e.RoleId });
            //modelBuilder.Entity<MemberRoles>()
            //            .HasOne(mr => mr.Member)
            //            .WithMany(m => m.MemberRoles)
            //            .HasForeignKey(mr => mr.MemberId);
            //modelBuilder.Entity<MemberRoles>()
            //            .HasOne(mr => mr.Role)
            //            .WithMany(r => r.MemberRoles)
            //            .HasForeignKey(mr => mr.RoleId);

            modelBuilder.Entity<AdminMasterRoles>().HasKey(e => new { e.AdminId, e.RoleId });
            modelBuilder.Entity<AdminMasterRoles>()
                        .HasOne(am => am.Admin)
                        .WithMany(m => m.AdminMasterRoles)
                        .HasForeignKey(mr => mr.AdminId);
            modelBuilder.Entity<AdminMasterRoles>()
                        .HasOne(mr => mr.Role)
                        .WithMany(r => r.AdminMasterRoles)
                        .HasForeignKey(mr => mr.RoleId);

        }
    }
}
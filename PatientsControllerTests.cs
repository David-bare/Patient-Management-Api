using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PatientManagement.Controllers;
using PatientManagement.Data;
using PatientManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManagement.Tests
{
    public class PatientsControllerTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
            .Options;

            var context = new ApplicationDbContext(options);

            //Seed the database
            context.Patients.Add(new Patient { Id = 1, Name = "David", Email = "david@gmail.com", IsDeleted = false });
            context.Patients.Add(new Patient { Id = 2, Name = "Segun", Email = "segun@gmail.com", IsDeleted = false });
            context.SaveChanges();

            return context;
        }

        [Fact]
        public async Task GetPatients_ShouldReturnAllPatients()
        {
            var context = GetDbContext();
            var controller = new PatientsController(context);
            var result = await controller.GetPatients();
            var patients = Assert.IsType<List<Patient>>(result.Value);
            Assert.Equal(2, patients.Count);
        }

        [Fact]
        public async Task GetPatient_WithValidId_ShouldReturnPatient()
        {
            var context = GetDbContext();
            var controller = new PatientsController(context);
            var result = await controller.GetPatient(1);
            var patient = Assert.IsType<Patient>(result.Value);
            Assert.Equal("David", patient.Name);
        }

        [Fact]
        public async Task GetPatient_WithInvalidId_ShouldReturnNotFound()
        {
            var context = GetDbContext();
            var controller = new PatientsController(context);
            var result = await controller.GetPatient(99);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreatePatient_ShouldReturnCreatedPatient()
        {
            var context = GetDbContext();
            var controller = new PatientsController(context);

            var newPatient = new Patient { Id = 3, Name = "John", Email = "john@gmail.com", IsDeleted = false };
            var result = await controller.CreatePatient(newPatient);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            var patient = Assert.IsType<Patient>(createdResult.Value);
            Assert.Equal("John", patient.Name);
        }

        [Fact]
        public async Task SoftDeletePatient_ShouldMarkPatientAsDeleted()
        {
            var context = GetDbContext();
            var controller = new PatientsController(context);

            var result = await controller.SoftDeletePatient(1);
            Assert.IsType<NoContentResult>(result);
            var patient = await context.Patients.FindAsync(1);
            Assert.True(patient.IsDeleted);
        }

        [Fact]
        public async Task SoftDeletePatient_WithInvalidId_ShouldReturnNotFound()
        {
            var context = GetDbContext();
            var controller = new PatientsController(context);
            var result = await controller.SoftDeletePatient(99);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

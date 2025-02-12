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
    public class PatientRecordControllerTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationDbContext(options);
            context.PatientRecords.Add(new PatientRecord { Id = 1, Name = "David Ogunlaja", Description = "Checkup", Age = 30, BloodGroup = "O+", Genotype = "AA", Sickness = "None", Date = DateTime.UtcNow, PatientId = 1 });
            context.PatientRecords.Add(new PatientRecord { Id = 2, Name = "Seyi Makinde", Description = "Flu", Age = 25, BloodGroup = "A+", Genotype = "AS", Sickness = "Cold", Date = DateTime.UtcNow, PatientId = 2 });
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task CreateRecord_ShouldReturnCreatedRecord()
        {
            var context = GetDbContext();
            var controller = new PatientRecordsController(context);
            var newRecord = new PatientRecord { Id = 3, Name = "David Smith", Description = "Fever", Age = 28, PatientId = 3 };
            var result = await controller.CreateRecord(newRecord);
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var record = Assert.IsType<PatientRecord>(createdResult.Value);
            Assert.Equal("Fever", record.Description);
        }

        [Fact]
        public async Task GetAllRecords_ShouldReturnAllRecords()
        {
            var context = GetDbContext();
            var controller = new PatientRecordsController(context);
            var result = await controller.GetAllRecords();
            var records = Assert.IsType<List<PatientRecord>>(result.Value);
            Assert.Equal(2, records.Count);
        }

        [Fact]
        public async Task GetRecord_WithValidId_ShouldReturnRecord()
        {
            var context = GetDbContext();
            var controller = new PatientRecordsController(context);
            var result = await controller.GetRecord(1);
            var okResult = Assert.IsType<OkObjectResult>(result);
            var record = Assert.IsType<PatientRecord>(okResult.Value);
            Assert.Equal("Checkup", record.Description);
        }

        [Fact]
        public async Task GetRecord_WithInvalidId_ShouldReturnNotFound()
        {
            var context = GetDbContext();
            var controller = new PatientRecordsController(context);
            var result = await controller.GetRecord(99);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task UpdateRecord_WithValidId_ShouldReturnNoContent()
        {
            var context = GetDbContext();
            var controller = new PatientRecordsController(context);
            var updatedRecord = new PatientRecord { Id = 1, Name = "John Doe", Description = "Updated Checkup", Age = 30, PatientId = 1 };
            var result = await controller.UpdateRecord(1, updatedRecord);
            Assert.IsType<NoContentResult>(result);
            var record = await context.PatientRecords.FindAsync(1);
            Assert.Equal("Updated Checkup", record.Description);
        }

        [Fact]
        public async Task UpdateRecord_WithInvalidId_ShouldReturnNotFound()
        {
            var context = GetDbContext();
            var controller = new PatientRecordsController(context);
            var updatedRecord = new PatientRecord { Id = 99, Name = "Unknown", Description = "No Data", Age = 40, PatientId = 99 };
            var result = await controller.UpdateRecord(99, updatedRecord);
            Assert.IsType<NotFoundResult>(result);
        }
    }
}

using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Models;
using Newtonsoft.Json;
using NUnit.Framework;
using TestProjAPI;

namespace UnitTestProjectAPITest
{
    public class ApiTests
    {
        private const string ApplicationJson = "application/json";
        private HttpClient _clientTestServer;

        [SetUp]
        public void SetUp()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>();

            var server = new TestServer(builder);
            _clientTestServer = server.CreateClient();
        }

        [Test]
        public async Task TestMethodForGrossIncomeBetween37001_To_87000()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = 60050,
                SupperRate = 9,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);
            var content = await result.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<EmployeeModel>(content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(5004, responseContent.Grossincome);
            Assert.AreEqual(922, responseContent.Incometax);
        }

        [Test]
        public async Task TestMethodForGrossIncomeIncomeBetween87001_To_180000()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = 120000,
                SupperRate = 10,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);
            var content = await result.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<EmployeeModel>(content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(10000, responseContent.Grossincome);
            Assert.AreEqual(2669, responseContent.Incometax);
        }

        [Test]
        public async Task TestMethodForGrossIncomeLessThan18200()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = 18000,
                SupperRate = 10,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);
            var content = await result.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<EmployeeModel>(content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(1500, responseContent.Grossincome);
            Assert.AreEqual(0, responseContent.Incometax);
        }

        [Test]
        public async Task TestMethodForGrossIncomeBetween18201_To_37000()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = 20000,
                SupperRate = 6,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);
            var content = await result.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<EmployeeModel>(content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(1666, responseContent.Grossincome);
            Assert.AreEqual(28, responseContent.Incometax);
        }

        [Test]
        public async Task TestMethodForGrossIncomeGreaterThan180001()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = 190000,
                SupperRate = 6,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);
            var content = await result.Content.ReadAsStringAsync();
            var responseContent = JsonConvert.DeserializeObject<EmployeeModel>(content);

            //Assert
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(15833, responseContent.Grossincome);
            Assert.AreEqual(4894, responseContent.Incometax);
        }

        [Test]
        public async Task TestMethodForGrossIncomeIs0()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = 0,
                SupperRate = 6,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public async Task TestMethodForGrossIncomeisLessThan0_Negative()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = -1000,
                SupperRate = 6,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public async Task TestMethodFoSuperRateLassThan0_Negative()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = 190000,
                SupperRate = -5,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public async Task TestMethodFoSuperRate_Is_0()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = 20000,
                SupperRate = 0,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public async Task TestMethodForModelNotValidateSupperRateMissing()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                AnnualSalary = 20000,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }

        [Test]
        public async Task TestMethodForModelNotValidateAnnualSalaryMissing()
        {
            //Arrange
            var jsonRes = JsonConvert.SerializeObject(new Employee
            {
                FirstName = "David",
                LastName = "Smith",
                SupperRate = 9,
                Startdate = "04-07-2018",
                EndDate = "05-07-2018"
            });

            var stringContent = new StringContent(jsonRes, Encoding.UTF8, ApplicationJson);

            //Act
            var result = await _clientTestServer.PostAsync("/Employee", stringContent);

            //Assert
            Assert.AreEqual(HttpStatusCode.BadRequest, result.StatusCode);
        }
    }
}
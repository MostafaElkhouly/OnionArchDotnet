using CsmsAPI.Test.Initialization;
using Infrastructure.ViewModel.Response;
using Infrastructure.ViewModel.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsmsAPI.Test.ApiTest
{
    [TestClass]
    public class SparPartTest : InitializationTest
    {

        [TestMethod]
        public async Task CreateSparPart()
        {
            var result = await PostAsync<SuccessResponse<Guid>>("api/sparpart", new ResReqSparPart
            {
                Quantity = 10,
                SparName = "Test"
            });

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Data != Guid.Empty);
        }

        [TestMethod]
        public async Task GetSparPartById()
        {
            var resultCreate = await PostAsync<SuccessResponse<Guid>>("api/sparpart", new ResReqSparPart
            {
                Quantity = 10,
                SparName = "Test"
            });

            Assert.IsNotNull(resultCreate);
            Assert.IsTrue(resultCreate.Data != Guid.Empty);

            var resultAll = await GetAsync<SuccessResponse<ResReqSparPart>>($"api/sparpart/{resultCreate.Data}");

            Assert.IsNotNull(resultAll);
            Assert.IsNotNull(resultAll.Data);
        }

        [TestMethod]
        public async Task UpdateSparPart()
        {
            var resultCreate = await PostAsync<SuccessResponse<Guid>>("api/sparpart", new ResReqSparPart
            {
                Quantity = 10,
                SparName = "Test"
            });

            Assert.IsNotNull(resultCreate);
            Assert.IsTrue(resultCreate.Data != Guid.Empty);

            var result = await GetAsync<SuccessResponse<ResReqSparPart>>($"api/sparpart/{resultCreate.Data}");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);

            var sparPart = result.Data;

            var newQuantity = 5;
            var SparName = "Test5";

            sparPart.Quantity = newQuantity;
            sparPart.SparName = SparName;
            
            var resultUpdate = await PutAsync<SuccessResponse<bool>>($"api/sparpart/{resultCreate.Data}", sparPart);

            Assert.IsNotNull(resultUpdate);
            Assert.IsTrue(resultUpdate.Data);


            result = await GetAsync<SuccessResponse<ResReqSparPart>>($"api/sparpart/{resultCreate.Data}");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);

            Assert.AreEqual(result.Data.Quantity, newQuantity);
            Assert.AreEqual(result.Data.SparName, SparName);
        }

        [TestMethod]
        public async Task GetAll()
        {
            var result = await GetAsync<SuccessResponse<List<ResReqSparPart>>>($"api/sparpart");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);

            Assert.IsTrue(result.Data.Count > 0);
        }

        [TestMethod]
        public async Task Delete()
        {
            var rCreate = await PostAsync<SuccessResponse<Guid>>("api/sparpart", new ResReqSparPart
            {
                Quantity = 10,
                SparName = "Test"
            });

            Assert.IsNotNull(rCreate);
            Assert.IsTrue(rCreate.Data != Guid.Empty);

            var rGet = await GetAsync<SuccessResponse<ResReqSparPart>>($"api/sparpart/{rCreate.Data}");

            Assert.IsNotNull(rGet);
            Assert.IsNotNull(rGet.Data);

            var rDelete = await DeleteAsync<SuccessResponse<bool>>($"api/sparpart/{rGet.Data.Id}");

            Assert.IsNotNull(rDelete);
            Assert.IsTrue(rDelete.Data);
        }
    }
}

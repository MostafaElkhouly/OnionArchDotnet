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
    public class CarTest : InitializationTest
    {
        [TestMethod]
        public async Task CreateCar()
        {
            var resultColor = await PostAsync<SuccessResponse<Guid>>("api/Color" , new ResReqColor
            {
                ColorName= "red",
                HEX = "#F00",
                RGB = "#F00",
                
            });

            Assert.IsNotNull(resultColor.Data);

            var resultCarCompany = await PostAsync<SuccessResponse<Guid>>("api/CarModel", new ResReqCarModel
            {
                Model = "BMW Test",
                ParentId = null
            });

            Assert.IsNotNull(resultCarCompany.Data);

            var resultCarModel = await PostAsync<SuccessResponse<Guid>>("api/CarModel", new ResReqCarModel
            {
                Model = "X6",
                ParentId= resultCarCompany.Data,
            });

            Assert.IsNotNull(resultCarModel.Data);

            var resultUser = await PostAsync<SuccessResponse<CacheModel>>("api/User/Register", new ReqCreateUser
            {
                Car = new ResReqCar
                {
                    CarModelId = resultCarModel.Data,
                    ColorId = resultColor.Data,
                    CarEngineNumber = "251425321 Test",
                    CarNumber = "251425321 Test",
                    HoursePower = 5246,
                    RegistrationNumber = "251425321 Test",
                    YearOfVersion = "2015 Test",
                    AccountId = "0e8afb4e-86fa-4665-be79-a8318b49517e"
                },
                Email = "Mostafa.Anwar.Gawad@Gmail.com",
                FirstName = "Mostafa",
                LastName = "El-Khouly",
                MiddleName = "",
                Password = "qweewq",
                PhoneNumber = "01016379792 Test",
                RoleId = "e540adb7-dde5-48bc-ad2c-24db7236df8b",
                UserName = "KhoulyTest"
            });

            Assert.IsNotNull(resultUser);


            //var result = await PostAsync<SuccessResponse<Guid>>("api/Car", new ResReqCar
            //{
            //    CarModelId = resultCarModel.Data,
            //    ColorId = resultColor.Data,
            //    CarEngineNumber = "251425321 Test",
            //    CarNumber = "251425321 Test",
            //    HoursePower = 5246,
            //    RegistrationNumber = "251425321 Test",
            //    YearOfVersion = "2015 Test",
            //    AccountId = "0e8afb4e-86fa-4665-be79-a8318b49517e"
            //});

            //Assert.IsNotNull(result);
            //Assert.IsTrue(result.Data != Guid.Empty);
        }

        [TestMethod]
        public async Task GetCarById()
        {
            var rCreate = await PostAsync<SuccessResponse<Guid>>("api/Car", new ResReqCar
            {
                
            });

            Assert.IsNotNull(rCreate);
            Assert.IsTrue(rCreate.Data != Guid.Empty);

            var rGetAll = await GetAsync<SuccessResponse<ResReqCar>>($"api/Car/{rCreate.Data}");

            Assert.IsNotNull(rGetAll);
            Assert.IsNotNull(rGetAll.Data);
        }

        [TestMethod]
        public async Task UpdateCar()
        {
            var rCreate = await PostAsync<SuccessResponse<Guid>>("api/Car", new ResReqCar
            {

            });

            Assert.IsNotNull(rCreate);
            Assert.IsTrue(rCreate.Data != Guid.Empty);

            var result = await GetAsync<SuccessResponse<ResReqCar>>($"api/Car/{rCreate.Data}");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);

            var Car = result.Data;

            

            var rUpdate = await PutAsync<SuccessResponse<bool>>($"api/Car/{rCreate.Data}", Car);

            Assert.IsNotNull(rUpdate);
            Assert.IsTrue(rUpdate.Data);


            result = await GetAsync<SuccessResponse<ResReqCar>>($"api/Car/{rCreate.Data}");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public async Task GetAll()
        {
            var result = await GetAsync<SuccessResponse<List<ResReqCar>>>($"api/Car");

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);

            Assert.IsTrue(result.Data.Count > 0);
        }

        [TestMethod]
        public async Task Delete()
        {
            var rCreate = await PostAsync<SuccessResponse<Guid>>("api/Car", new ResReqCar
            {

            });

            Assert.IsNotNull(rCreate);
            Assert.IsTrue(rCreate.Data != Guid.Empty);

            var rGet = await GetAsync<SuccessResponse<ResReqCar>>($"api/Car/{rCreate.Data}");

            Assert.IsNotNull(rGet);
            Assert.IsNotNull(rGet.Data);

            var rDelete = await DeleteAsync<SuccessResponse<bool>>($"api/Car/{rGet.Data.Id}");

            Assert.IsNotNull(rDelete);
            Assert.IsTrue(rDelete.Data);
        }
    }
}

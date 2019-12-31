using System;
using System.Linq;
using Microsoft.Extensions.Options;
using TimeTracker.Core.Data;
using TimeTracker.Core.Models;
using TimeTracker.Core.Services;
using TimeTracker.Core.Services.Implements;
using TimeTracker.Core.Utils;
using Xunit;
using Xunit.Priority;

namespace TimeTracker.Web.Test.Services
{
    //[TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class TheFullScenarioTest
    {
        private static string ClientName = "ITestableClient";
        private static string Item1Name = "IItem1TestName";
        private static string Item2Name = "IItem2TestName";

        protected NHibernateHelper GetConnectionHelper()
        {
            IOptions<ApplicationSettings> config = Options.Create<ApplicationSettings>(new ApplicationSettings() { ConnectionString = "Server=localhost,1433;Database=TimeTracker;User Id=TimeTrackerManagerLogin;Password=TimeTrackerManagerPassword123;MultipleActiveResultSets=True;Integrated Security=False" });
            var dbHelper = new NHibernateHelper(config);
            return dbHelper;
        }

        [Fact, Priority(0)]
        public void ClientInitialisationTest()
        {
            Client client = null;
            IClientService clService = new ClientService(this.GetConnectionHelper());
            //Given
            var totalClients = clService.GetAll().Count();

            client = clService.GetClientByTitle(ClientName);
            if (client == null)
            {
                client = new Client()
                {
                    Title = ClientName
                };
                clService.Save(client);

                Assert.True(client.Id > 0);

                totalClients = clService.GetAll().Count();
            }


            Assert.True(clService.GetAll().Count() == totalClients);

        }

        [Fact, Priority(1)]
        public void ClientProcessInitialisationTest()
        {

            var clientId = 0;
            clientId = new ClientService(this.GetConnectionHelper()).GetClientByTitle(ClientName).Id;

            Assert.True(clientId != 0);

            var process = new ClientProcess()
            {
                ProcessName = "ITespProcess for Client " + clientId
            };
            var prService = new ClientProcessService(this.GetConnectionHelper());
            var processes = prService.GetByClientId(clientId);

            if (processes.Count() == 0)
            {
                var client = new ClientService(this.GetConnectionHelper()).GetById(clientId);
                Assert.True(client != null);

                process.OwnerClient = client;
                prService.Save(process);

                processes = prService.GetByClientId(clientId);
                Assert.True(processes.Count() > 0);
            }

            process = processes.First();

            Assert.True(processes.Count() == 1);
            Assert.True(process.Id > 0);
        }
        [Fact, Priority(2)]
        public void TrackableItemInitializationTest()
        {
            var client = new ClientService(this.GetConnectionHelper()).GetClientByTitle(ClientName);
            var process = new ClientProcessService(this.GetConnectionHelper()).GetByClientId(client.Id).FirstOrDefault();

            Assert.True(process != null);

            var itemService = new TrackableItemService(this.GetConnectionHelper());

            TrackableItem item1 = itemService.GetByTitleForProcess(Item1Name, process.Id);
            TrackableItem item2 = itemService.GetByTitleForProcess(Item2Name, process.Id);

            if (item2 == null && item2 == null)
            {
                item1 = new TrackableItem()
                {
                    Title = Item1Name,
                    OwnerProcess = process
                };
                item2 = new TrackableItem()
                {
                    Title = Item2Name,
                    OwnerProcess = process
                };

                itemService.Save(item1);
                itemService.Save(item2);

                var items = itemService.GetAllForProcessId(process.Id);

                Assert.True(item1.Id > 0);
                Assert.True(item2.Id > 0);
                Assert.True(items.Count() == 2);

                item1 = itemService.GetByTitleForProcess(Item1Name, process.Id);
                item2 = itemService.GetByTitleForProcess(Item2Name, process.Id);
            }

            Assert.True(item1 != null);
            Assert.True(item2 != null);
        }




        [Fact, Priority(3)]
        public void TrackableItemTrackingTest()
        {
            var clientId = new ClientService(this.GetConnectionHelper()).GetClientByTitle(ClientName).Id;
            var processId = new ClientProcessService(this.GetConnectionHelper()).GetByClientId(clientId).FirstOrDefault().Id;
            var items = new TrackableItemService(this.GetConnectionHelper()).GetAllForProcessId(processId);

            Assert.True(items.Count() == 2);

            var startTime = DateTime.Now.Date.AddHours(9).AddMinutes(30);
            var startTrackTime = DateTime.Now.Date.AddHours(10);
            var finishTime = DateTime.Now.Date.AddHours(11);
            var finishTrackTime = DateTime.Now.Date.AddHours(12);

            foreach (var item in items)
            {
                new TrackService(this.GetConnectionHelper()).ReleaseTrackbleItemBetweenTimes(item.Id, startTrackTime, finishTrackTime);
            }

            foreach (var item in items)
            {
                Assert.True(new TrackableItemService(this.GetConnectionHelper()).CheckAvailableCountBetweenTimes(item.Id, startTime, finishTime));
            }

            //add atrcking
            foreach (var item in items)
            {
                new TrackService(this.GetConnectionHelper()).CreateTrack(item.Id, startTrackTime, finishTrackTime);
            }

            foreach (var item in items)
            {
                var itemService = new TrackableItemService(this.GetConnectionHelper());
                Assert.False(itemService.CheckAvailableCountBetweenTimes(item.Id, startTime, finishTime));
            }


            //Given

            //When

            //Then

        }

    }
}
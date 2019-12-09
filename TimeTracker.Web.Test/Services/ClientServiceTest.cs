using System;
using System.Collections.Generic;
using System.Linq;
using TimeTracker.Core.Models;
using TimeTracker.Core.Services;
using TimeTracker.Core.Services.Implements;
using TimeTracker.Web.Test.Utils;
using Xunit;

namespace TimeTracker.Web.Test.Services
{
    public class ClientServiceTest : ABaseServiceTest<ClientService>
    {
        public override ClientService GetService()
        {
            return new ClientService(this.GetConnectionHelper());
        }

        [Fact]
        public void SaveTest()
        {
            var item = new Client()
            {
                Title = DateTime.Now.ToString()
            };

            var saved = this.GetService().Save(item);
            Assert.True(saved.Id > 0);
        }

        [Fact]
        public void getAll()
        {
            Assert.True(this.GetService().GetAll().Count() > 0);
        }
    }
}
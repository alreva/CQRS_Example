using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;

namespace CQRS.Integration.Tests
{
    public static class EventPlayerTests
    {
        [TestClass]
        public class Play
        {
            [TestMethod]
            public void HappyPath()
            {
                var connectionProvider = new AdoNetConnectionProvider("DefaultConnection");
                var player = new EventPlayer(
                    connectionProvider,
                    new ToEventConverter(),
                    new EventPublisher(
                        new UpdateAvailableCategoriesOnCategoryAdded(connectionProvider),
                        new UpdateCategoryTreeOnCategoryAdded(connectionProvider)));

                player.Play();
            }
        }
    }
}

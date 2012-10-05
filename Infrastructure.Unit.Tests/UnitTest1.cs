using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReadModel;

namespace Infrastructure.Unit.Tests
{
    public static class DtoDataReaderTests
    {
        [TestClass]
        public class Where
        {
            [TestMethod]
            public void two_clauses_parsed_correctly()
            {
                var id = Guid.NewGuid();
                var date = DateTime.Now;
                var reader = new DtoDataReader(null, new SqlBuilder());
                reader.Where<AvailableCategoryDto>(
                    o => o.Title == "abc");
            }
        }
    }

    public class AvailableCategoryDto
    {
        public AvailableCategoryDto(
            string id,
            string title)
        {
            Title = title;
            Id = id;
        }

        public string Id { get; private set; }

        public string Title { get; private set; }
    }
}

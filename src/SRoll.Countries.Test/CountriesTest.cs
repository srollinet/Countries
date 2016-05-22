using System.Globalization;
using System.Threading;
using Xunit;

namespace SRoll.Countries.Test
{
    public class CountriesTest
    {
        public CountriesTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("en");
        }

        [Fact]
        public void CreateTest()
        {
            var country = new Country("CH", "CHE", "");
            Assert.Equal("Switzerland", country.Name);
        }

        [Fact]
        public void OtherCultureTest()
        {
            CultureInfo.CurrentUICulture = new CultureInfo("fr");
            var country = new Country("CH", "CHE", "");
            Assert.Equal("Suisse", country.Name);
        }

        [Fact]
        public void GetListTest()
        {
            var list = Country.Countries;
        }
    }
}
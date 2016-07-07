using System.Globalization;
using Xunit;

namespace SRoll.Countries.Test
{
    public class CountriesTest
    {
        public CountriesTest()
        {
            SetCulture("en");
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
            SetCulture("fr");
            var country = new Country("CH", "CHE", "");
            Assert.Equal("Suisse", country.Name);
        }

        [Fact]
        public void GetListTest()
        {
            var list = Country.Countries;
        }

        private static void SetCulture(string culture)
        {
            var ci = new CultureInfo(culture);
            #if LEGACY
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
            #else
            CultureInfo.CurrentUICulture = ci;
            #endif
        }
    }
}
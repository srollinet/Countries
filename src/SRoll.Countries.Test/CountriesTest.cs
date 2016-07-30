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
            var country = new Country("CH", "CHE", 756);
            Assert.Equal("Switzerland", country.Name);
        }

        [Fact]
        public void OtherCultureTest()
        {
            SetCulture("fr-CH");
            var country = new Country("CH", "CHE", 756);
            Assert.Equal("Suisse", country.Name);
        }

        [Fact]
        public void ChangeCultureTest()
        {
            var country = new Country("CH", "CHE", 756);
            Assert.Equal("Switzerland", country.Name);

            SetCulture("fr");
            Assert.Equal("Suisse", country.Name);
        }

        [Fact]
        public void UnsupportedCultureTest()
        {
            SetCulture("nb-NO");
            var country = new Country("CH", "CHE", 756);
            Assert.Equal("Switzerland", country.Name);
        }

        [Fact]
        public void GetLocalizedNameTest()
        {
            var country = new Country("CH", "CHE", 756);
            Assert.Equal("Switzerland", country.Name);
            Assert.Equal("Suisse", country.GetLocalizedName(new CultureInfo("fr")));
        }

        [Fact]
        public void GetListTest()
        {
            var list = Country.List;
            Assert.NotEmpty(list);
            foreach (var country in list)
            {
                Assert.NotNull(country.Name);
            }
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
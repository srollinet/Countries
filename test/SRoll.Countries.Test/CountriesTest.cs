using System.Globalization;
using Moq;
using SRoll.Countries.TranslationProvider;
using Xunit;

namespace SRoll.Countries.Test
{
    public class CountriesTest
    {
        public CountriesTest()
        {
            SetCulture("en");
            Country.CustomTranslationProvider = null;
        }

        [Fact]
        public void CreateTest()
        {
            var country = new Country("CH", "CHE", 756);
            Assert.Equal("Switzerland", country.Name);
        }

        [Fact]
        public void ChildCultureTest()
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
                Assert.NotEmpty(country.Name);
            }
        }

        [Fact]
        public void CustomProviderTest()
        {
            var providerMock = new Mock<ITranslationProvider>();
            Country.CustomTranslationProvider = providerMock.Object;

            var country = new Country("CH", "CHE", 756);

            providerMock.Setup(provider => provider.GetValue("CHE")).Returns("Switzerland!");
            Assert.Equal("Switzerland!", country.Name);

            providerMock.Setup(provider => provider.GetValue("CHE")).Returns((string) null);
            Assert.Equal("Switzerland", country.Name);

            providerMock.Setup(provider => provider.GetValue("CHE")).Returns("");
            Assert.Equal("", country.Name);

            var culture = new CultureInfo("fr");
            providerMock.Setup(provider => provider.GetValue("CHE", culture)).Returns("Suisse!");
            Assert.Equal("Suisse!", country.GetLocalizedName(culture));

            providerMock.Setup(provider => provider.GetValue("CHE", culture)).Returns((string)null);
            Assert.Equal("Suisse", country.GetLocalizedName(culture));

            providerMock.Setup(provider => provider.GetValue("CHE", culture)).Returns("");
            Assert.Equal("", country.GetLocalizedName(culture));

            Country.CustomTranslationProvider = null;
            Assert.Equal("Switzerland", country.Name);
            Assert.Equal("Suisse", country.GetLocalizedName(culture));

        }

        private static void SetCulture(string culture)
        {
            var ci = new CultureInfo(culture);
#if NET452
            System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
#else
            CultureInfo.CurrentUICulture = ci;
#endif
        }
    }
}
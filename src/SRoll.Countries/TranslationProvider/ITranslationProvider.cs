using System.Globalization;

namespace SRoll.Countries.TranslationProvider
{
    /// <summary>
    /// Provides methods to get the translated country names, based on the Alpha3 code
    /// </summary>
    public interface ITranslationProvider
    {
        /// <summary>
        /// Returns the country name for the given Alpha3 code in the current UI Culture
        /// </summary>
        /// <param name="alpha3Code"></param>
        /// <returns></returns>
        string GetValue(string alpha3Code);

        /// <summary>
        /// Returns the country name for the given Alpha3 code in the given culture
        /// </summary>
        /// <param name="alpha3Code"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        string GetValue(string alpha3Code, CultureInfo culture);
    }
}

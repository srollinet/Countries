using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SRoll.Countries.TranslationProvider
{
    internal class TranslationProvider : ITranslationProvider
    {
        private const string DefaultCultureName = "en";

        private readonly Dictionary<string, Dictionary<string, string>> _cultureMaps = new Dictionary<string, Dictionary<string, string>>();

        /// <inheritdoc />
        public string GetValue(string alpha3Code)
        {
            return GetValue(alpha3Code, CultureInfo.CurrentUICulture);
        }

        /// <inheritdoc />
        public string GetValue(string alpha3Code, CultureInfo culture)
        {
            return GetMatchingDictionary(culture)[alpha3Code];
        }

        private IReadOnlyDictionary<string, string> GetMatchingDictionary(CultureInfo culture)
        {
            //First, try the culture itself
            if (_cultureMaps.TryGetValue(culture.Name, out var dict))
            {
                return dict;
            }

            //Then, try the parent culture if it is not the invariant.
            if (!Equals(culture.Parent, CultureInfo.InvariantCulture) &&
                _cultureMaps.TryGetValue(culture.Parent.Name, out dict))
            {
                return dict;
            }

            //Then, fallback to the default culture
            return _cultureMaps[DefaultCultureName];
        }

        private void AddCultureFromFile(string fileName, CultureInfo culture)
        {
            var assembly = typeof(TranslationProvider).GetTypeInfo().Assembly;
            var resource = assembly.GetManifestResourceStream(fileName);
            var translations = new Dictionary<string, string>();
            using (var reader = new StreamReader(resource))
            {
                var separator = new[] { ':' };
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(separator, 2);
                    translations.Add(line[0], line[1]);
                }
            }
            _cultureMaps[culture.Name] = translations;
        }

        internal static TranslationProvider CreateDefaultProvider()
        {
            var provider = new TranslationProvider();
            var assembly = typeof(Country).GetTypeInfo().Assembly;
            const string regex = @"^SRoll.Countries.Translations.CountryNames_(?<culture>[a-z]{2}(?:-[A-Z]{2})?).txt$";
            foreach (var fileName in assembly.GetManifestResourceNames())
            {
                var match = Regex.Match(fileName, regex);
                if (match.Success)
                {
                    var culture = match.Groups["culture"].Value;
                    provider.AddCultureFromFile(fileName, new CultureInfo(culture));
                }
            }

            return provider;
        }
    }
}

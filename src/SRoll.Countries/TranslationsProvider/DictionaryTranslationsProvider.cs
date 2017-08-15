using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SRoll.Countries.TranslationsProvider
{
    internal class DictionaryTranslationsProvider
    {
        private readonly Dictionary<string, Dictionary<string, string>> _cultureMaps = new Dictionary<string, Dictionary<string, string>>();
        private string _defaultCultureName;

        public string GetValue(string key)
        {
            return GetValue(key, CultureInfo.CurrentUICulture);
        }

        public string GetValue(string key, CultureInfo culture)
        {
            Dictionary<string, string> dict;
            if (!_cultureMaps.TryGetValue(culture.Name, out dict))
            {
                if (Equals(culture.Parent, CultureInfo.InvariantCulture) || !_cultureMaps.TryGetValue(culture.Parent.Name, out dict))
                {
                    dict = _cultureMaps[_defaultCultureName];
                }
            }

            return dict[key];
        }

        public void AddCulture(CultureInfo culture, IReadOnlyDictionary<string, string> values)
        {
            //First culture is considered as default if not defined
            if (_defaultCultureName == null)
            {
                _defaultCultureName = culture.Name;
            }

            Dictionary<string, string> vals;
            if (!_cultureMaps.TryGetValue(culture.Name, out vals))
            {
                vals = new Dictionary<string, string>();
                _cultureMaps[culture.Name] = vals;
            }
            foreach (var valuePair in values)
            {
                vals[valuePair.Key] = valuePair.Value;
            }
        }

        private void AddCultureFromFile(string fileName, CultureInfo culture)
        {
            var assembly = typeof(Country).GetTypeInfo().Assembly;
            var resource = assembly.GetManifestResourceStream(fileName);
            var translations = new Dictionary<string, string>();
            using (var reader = new StreamReader(resource))
            {
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine().Split(new[] { ':' }, 2);
                    translations.Add(line[0], line[1]);
                }
            }
            AddCulture(culture, translations);
        }

        internal static DictionaryTranslationsProvider CreateDefaultProvider()
        {
            var provider = new DictionaryTranslationsProvider();
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

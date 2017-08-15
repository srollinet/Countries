using System;
using System.Collections.Generic;
using System.Globalization;
using SRoll.Countries.TranslationProvider;

namespace SRoll.Countries
{
    /// <summary>
    /// Localized ISO 3166 Country with alpha-2, alpha-3 and Numeric code
    /// </summary>
    public class Country
    {
        private static readonly ITranslationProvider DefaultTranslationProvider = TranslationProvider.TranslationProvider.CreateDefaultProvider();

        /// <summary>
        /// Use this to overide the values provided by default.
        /// If the custom provider returns a null value for a country, the value is taken from the default provider.
        /// </summary>
        public static ITranslationProvider CustomTranslationProvider { get; set; }

        //Used for countries created manually
        private readonly string _name;

        /// <summary>
        /// ISO 3166 2 chars code
        /// </summary>
        public string Alpha2Code { get; }

        /// <summary>
        /// ISO 3166 3 chars code
        /// </summary>
        public string Alpha3Code { get; }

        /// <summary>
        /// ISO 3166 Numeric code
        /// </summary>
        public int NumericCode { get; }

        /// <summary>
        /// Name in the current UI culture
        /// </summary>
        public string Name => _name ??
            CustomTranslationProvider?.GetValue(Alpha3Code) ??
            DefaultTranslationProvider.GetValue(Alpha3Code);

        /// <summary>
        /// Returns a new instance of <see cref="Country"/> with custom codes and name
        /// </summary>
        /// <param name="alpha2Code">2 chars code</param>
        /// <param name="alpha3Code">3 chars code</param>
        /// <param name="numericCode">Numeric code</param>
        /// <param name="name">Name</param>
        public Country(string alpha2Code, string alpha3Code, int numericCode, string name)
        {
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            NumericCode = numericCode;
            _name = name ?? throw new ArgumentNullException(nameof(name));
        }

        /// <summary>
        /// Returns a new instance of <see cref="Country"/> with localized name from the resources
        /// </summary>
        /// <param name="alpha2Code">2 chars code</param>
        /// <param name="alpha3Code">3 chars code</param>
        /// <param name="numericCode">Numeric code</param>
        internal Country(string alpha2Code, string alpha3Code, int numericCode)
        {
            Alpha2Code = alpha2Code;
            Alpha3Code = alpha3Code;
            NumericCode = numericCode;
        }

        /// <summary>
        /// Returns the name in the given culture
        /// </summary>
        /// <param name="culture">The culture used to localize the name</param>
        /// <returns>The localized name</returns>
        public string GetLocalizedName(CultureInfo culture)
        {
            return CustomTranslationProvider?.GetValue(Alpha3Code, culture) ??
                   DefaultTranslationProvider.GetValue(Alpha3Code, culture);
        }

        /// <summary>
        /// List with all the countries
        /// </summary>
        public static IReadOnlyCollection<Country> List = new[]{
            new Country("AF", "AFG", 004),
            new Country("AX", "ALA", 248),
            new Country("AL", "ALB", 008),
            new Country("DZ", "DZA", 012),
            new Country("AS", "ASM", 016),
            new Country("AD", "AND", 020),
            new Country("AO", "AGO", 024),
            new Country("AI", "AIA", 660),
            new Country("AQ", "ATA", 010),
            new Country("AG", "ATG", 028),
            new Country("AR", "ARG", 032),
            new Country("AM", "ARM", 051),
            new Country("AW", "ABW", 533),
            new Country("AU", "AUS", 036),
            new Country("AT", "AUT", 040),
            new Country("AZ", "AZE", 031),
            new Country("BS", "BHS", 044),
            new Country("BH", "BHR", 048),
            new Country("BD", "BGD", 050),
            new Country("BB", "BRB", 052),
            new Country("BY", "BLR", 112),
            new Country("BE", "BEL", 056),
            new Country("BZ", "BLZ", 084),
            new Country("BJ", "BEN", 204),
            new Country("BM", "BMU", 060),
            new Country("BT", "BTN", 064),
            new Country("BO", "BOL", 068),
            new Country("BQ", "BES", 535),
            new Country("BA", "BIH", 070),
            new Country("BW", "BWA", 072),
            new Country("BV", "BVT", 074),
            new Country("BR", "BRA", 076),
            new Country("IO", "IOT", 086),
            new Country("BN", "BRN", 096),
            new Country("BG", "BGR", 100),
            new Country("BF", "BFA", 854),
            new Country("BI", "BDI", 108),
            new Country("CV", "CPV", 132),
            new Country("KH", "KHM", 116),
            new Country("CM", "CMR", 120),
            new Country("CA", "CAN", 124),
            new Country("KY", "CYM", 136),
            new Country("CF", "CAF", 140),
            new Country("TD", "TCD", 148),
            new Country("CL", "CHL", 152),
            new Country("CN", "CHN", 156),
            new Country("CX", "CXR", 162),
            new Country("CC", "CCK", 166),
            new Country("CO", "COL", 170),
            new Country("KM", "COM", 174),
            new Country("CG", "COG", 178),
            new Country("CD", "COD", 180),
            new Country("CK", "COK", 184),
            new Country("CR", "CRI", 188),
            new Country("CI", "CIV", 384),
            new Country("HR", "HRV", 191),
            new Country("CU", "CUB", 192),
            new Country("CW", "CUW", 531),
            new Country("CY", "CYP", 196),
            new Country("CZ", "CZE", 203),
            new Country("DK", "DNK", 208),
            new Country("DJ", "DJI", 262),
            new Country("DM", "DMA", 212),
            new Country("DO", "DOM", 214),
            new Country("EC", "ECU", 218),
            new Country("EG", "EGY", 818),
            new Country("SV", "SLV", 222),
            new Country("GQ", "GNQ", 226),
            new Country("ER", "ERI", 232),
            new Country("EE", "EST", 233),
            new Country("ET", "ETH", 231),
            new Country("FK", "FLK", 238),
            new Country("FO", "FRO", 234),
            new Country("FJ", "FJI", 242),
            new Country("FI", "FIN", 246),
            new Country("FR", "FRA", 250),
            new Country("GF", "GUF", 254),
            new Country("PF", "PYF", 258),
            new Country("TF", "ATF", 260),
            new Country("GA", "GAB", 266),
            new Country("GM", "GMB", 270),
            new Country("GE", "GEO", 268),
            new Country("DE", "DEU", 276),
            new Country("GH", "GHA", 288),
            new Country("GI", "GIB", 292),
            new Country("GR", "GRC", 300),
            new Country("GL", "GRL", 304),
            new Country("GD", "GRD", 308),
            new Country("GP", "GLP", 312),
            new Country("GU", "GUM", 316),
            new Country("GT", "GTM", 320),
            new Country("GG", "GGY", 831),
            new Country("GN", "GIN", 324),
            new Country("GW", "GNB", 624),
            new Country("GY", "GUY", 328),
            new Country("HT", "HTI", 332),
            new Country("HM", "HMD", 334),
            new Country("VA", "VAT", 336),
            new Country("HN", "HND", 340),
            new Country("HK", "HKG", 344),
            new Country("HU", "HUN", 348),
            new Country("IS", "ISL", 352),
            new Country("IN", "IND", 356),
            new Country("ID", "IDN", 360),
            new Country("IR", "IRN", 364),
            new Country("IQ", "IRQ", 368),
            new Country("IE", "IRL", 372),
            new Country("IM", "IMN", 833),
            new Country("IL", "ISR", 376),
            new Country("IT", "ITA", 380),
            new Country("JM", "JAM", 388),
            new Country("JP", "JPN", 392),
            new Country("JE", "JEY", 832),
            new Country("JO", "JOR", 400),
            new Country("KZ", "KAZ", 398),
            new Country("KE", "KEN", 404),
            new Country("KI", "KIR", 296),
            new Country("KP", "PRK", 408),
            new Country("KR", "KOR", 410),
            new Country("KW", "KWT", 414),
            new Country("KG", "KGZ", 417),
            new Country("LA", "LAO", 418),
            new Country("LV", "LVA", 428),
            new Country("LB", "LBN", 422),
            new Country("LS", "LSO", 426),
            new Country("LR", "LBR", 430),
            new Country("LY", "LBY", 434),
            new Country("LI", "LIE", 438),
            new Country("LT", "LTU", 440),
            new Country("LU", "LUX", 442),
            new Country("MO", "MAC", 446),
            new Country("MK", "MKD", 807),
            new Country("MG", "MDG", 450),
            new Country("MW", "MWI", 454),
            new Country("MY", "MYS", 458),
            new Country("MV", "MDV", 462),
            new Country("ML", "MLI", 466),
            new Country("MT", "MLT", 470),
            new Country("MH", "MHL", 584),
            new Country("MQ", "MTQ", 474),
            new Country("MR", "MRT", 478),
            new Country("MU", "MUS", 480),
            new Country("YT", "MYT", 175),
            new Country("MX", "MEX", 484),
            new Country("FM", "FSM", 583),
            new Country("MD", "MDA", 498),
            new Country("MC", "MCO", 492),
            new Country("MN", "MNG", 496),
            new Country("ME", "MNE", 499),
            new Country("MS", "MSR", 500),
            new Country("MA", "MAR", 504),
            new Country("MZ", "MOZ", 508),
            new Country("MM", "MMR", 104),
            new Country("NA", "NAM", 516),
            new Country("NR", "NRU", 520),
            new Country("NP", "NPL", 524),
            new Country("NL", "NLD", 528),
            new Country("NC", "NCL", 540),
            new Country("NZ", "NZL", 554),
            new Country("NI", "NIC", 558),
            new Country("NE", "NER", 562),
            new Country("NG", "NGA", 566),
            new Country("NU", "NIU", 570),
            new Country("NF", "NFK", 574),
            new Country("MP", "MNP", 580),
            new Country("NO", "NOR", 578),
            new Country("OM", "OMN", 512),
            new Country("PK", "PAK", 586),
            new Country("PW", "PLW", 585),
            new Country("PS", "PSE", 275),
            new Country("PA", "PAN", 591),
            new Country("PG", "PNG", 598),
            new Country("PY", "PRY", 600),
            new Country("PE", "PER", 604),
            new Country("PH", "PHL", 608),
            new Country("PN", "PCN", 612),
            new Country("PL", "POL", 616),
            new Country("PT", "PRT", 620),
            new Country("PR", "PRI", 630),
            new Country("QA", "QAT", 634),
            new Country("RE", "REU", 638),
            new Country("RO", "ROU", 642),
            new Country("RU", "RUS", 643),
            new Country("RW", "RWA", 646),
            new Country("BL", "BLM", 652),
            new Country("SH", "SHN", 654),
            new Country("KN", "KNA", 659),
            new Country("LC", "LCA", 662),
            new Country("MF", "MAF", 663),
            new Country("PM", "SPM", 666),
            new Country("VC", "VCT", 670),
            new Country("WS", "WSM", 882),
            new Country("SM", "SMR", 674),
            new Country("ST", "STP", 678),
            new Country("SA", "SAU", 682),
            new Country("SN", "SEN", 686),
            new Country("RS", "SRB", 688),
            new Country("SC", "SYC", 690),
            new Country("SL", "SLE", 694),
            new Country("SG", "SGP", 702),
            new Country("SX", "SXM", 534),
            new Country("SK", "SVK", 703),
            new Country("SI", "SVN", 705),
            new Country("SB", "SLB", 090),
            new Country("SO", "SOM", 706),
            new Country("ZA", "ZAF", 710),
            new Country("GS", "SGS", 239),
            new Country("SS", "SSD", 728),
            new Country("ES", "ESP", 724),
            new Country("LK", "LKA", 144),
            new Country("SD", "SDN", 729),
            new Country("SR", "SUR", 740),
            new Country("SJ", "SJM", 744),
            new Country("SZ", "SWZ", 748),
            new Country("SE", "SWE", 752),
            new Country("CH", "CHE", 756),
            new Country("SY", "SYR", 760),
            new Country("TW", "TWN", 158),
            new Country("TJ", "TJK", 762),
            new Country("TZ", "TZA", 834),
            new Country("TH", "THA", 764),
            new Country("TL", "TLS", 626),
            new Country("TG", "TGO", 768),
            new Country("TK", "TKL", 772),
            new Country("TO", "TON", 776),
            new Country("TT", "TTO", 780),
            new Country("TN", "TUN", 788),
            new Country("TR", "TUR", 792),
            new Country("TM", "TKM", 795),
            new Country("TC", "TCA", 796),
            new Country("TV", "TUV", 798),
            new Country("UG", "UGA", 800),
            new Country("UA", "UKR", 804),
            new Country("AE", "ARE", 784),
            new Country("GB", "GBR", 826),
            new Country("US", "USA", 840),
            new Country("UM", "UMI", 581),
            new Country("UY", "URY", 858),
            new Country("UZ", "UZB", 860),
            new Country("VU", "VUT", 548),
            new Country("VE", "VEN", 862),
            new Country("VN", "VNM", 704),
            new Country("VG", "VGB", 092),
            new Country("VI", "VIR", 850),
            new Country("WF", "WLF", 876),
            new Country("EH", "ESH", 732),
            new Country("YE", "YEM", 887),
            new Country("ZM", "ZMB", 894),
            new Country("ZW", "ZWE", 716),
        };
    }
}
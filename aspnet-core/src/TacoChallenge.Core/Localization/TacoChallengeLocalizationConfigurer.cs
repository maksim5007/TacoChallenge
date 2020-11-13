using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace TacoChallenge.Localization
{
    public static class TacoChallengeLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(TacoChallengeConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(TacoChallengeLocalizationConfigurer).GetAssembly(),
                        "TacoChallenge.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}

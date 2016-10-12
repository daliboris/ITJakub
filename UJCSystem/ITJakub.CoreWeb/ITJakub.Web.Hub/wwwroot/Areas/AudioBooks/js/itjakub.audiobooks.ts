﻿$(document).ready(() => {
    const modulInicializator = new AudioBooksModulInicializator({
        bibliographyModule: {
            forcedBookType: BookTypeEnum.AudioBook
        },
        search: {
            enabledOptions: [
                SearchTypeEnum.Title,
                SearchTypeEnum.Author,
                SearchTypeEnum.Editor,
                SearchTypeEnum.Dating
            ],
            url: {
                advanced: getBaseUrl() + "AudioBooks/AudioBooks/AdvancedSearchPaged",
                text: getBaseUrl() + "AudioBooks/AudioBooks/TextSearchPaged",
                textCount: getBaseUrl() + "AudioBooks/AudioBooks/TextSearchCount",
                advancedCount: getBaseUrl() + "AudioBooks/AudioBooks/AdvancedSearchResultsCount"
            }
        },
        searchBox: {
            controllerPath: "AudioBooks/AudioBooks"
        },
        dropDownSelect: {
            dataUrl: getBaseUrl() + "AudioBooks/AudioBooks/GetAudioWithCategories"
        }
    });
    modulInicializator.init();
});

//functions used in VariableInterpreter.interpretScript
var aduioTypeTranslation = [
    "Neznámý",
    "Mp3",
    "Ogg",
    "Wav"
];

function translateAudioType(audioType: number): string {
    return aduioTypeTranslation[audioType];
}

function getAudioLengthString(value: string): string {
    return new TimeSpan(value).toShortString();
}

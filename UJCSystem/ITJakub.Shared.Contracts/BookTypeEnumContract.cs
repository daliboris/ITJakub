using System.Runtime.Serialization;

namespace ITJakub.Shared.Contracts
{
    [DataContract]
    public enum BookTypeEnumContract : byte
    {
        [EnumMember] Edition = 0, //Edice
        [EnumMember] Dictionary = 1, //Slovnik
        [EnumMember] Grammar = 2, //Mluvnice
        [EnumMember] ProfessionalLiterature = 3, //Odborna literatura
        [EnumMember] TextBank = 4, //Textova banka
        [EnumMember] BibliographicalItem = 5,   //Bibliograficky zaznam
        [EnumMember] CardFile = 6,   //Kartoteka
        [EnumMember] AudioBook = 7  //Audiokniha

    }
}
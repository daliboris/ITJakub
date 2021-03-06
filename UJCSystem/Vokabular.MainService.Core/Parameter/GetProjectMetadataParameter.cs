﻿namespace Vokabular.MainService.Core.Parameter
{
    public class GetProjectMetadataParameter
    {
        public bool IncludeGenre { get; set; }
        public bool IncludeKind { get; set; }
        public bool IncludeAuthor { get; set; }
        public bool IncludeResponsiblePerson { get; set; }

        public bool IsAnyAdditionalParameter()
        {
            return IncludeGenre || IncludeKind || IncludeAuthor || IncludeResponsiblePerson;
        }
    }
}

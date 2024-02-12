using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public record NoteQueryDTO(string FullPath);

    public record NoteContentDTO(string? Text);

    public record NoteInsertDTO(
        string BaseCatalog, 
        string Name, 
        string? Text
    );

    public record NoteUpdateDTO(
        string SourceFullPath, 
        string? TargetBasePath, 
        string? Name, 
        string? Text
    );

    public record NoteUpdateContentDTO(string FullPath, string? Text);
}

// Type: PivotalTracker.FluentAPI.Domain.Note
// Assembly: PivotalTracker.FluentAPI, Version=0.9.0.0, Culture=neutral, PublicKeyToken=null
// Assembly location: C:\Projetos\Pivodeck\Dependencias\PivotalTracker.FluentAPI.dll

using System;

namespace PivotalTracker.FluentAPI.Domain
{
  public class Note
  {
    public int Id { get; set; }

    public int StoryId { get; set; }

    public string Description { get; set; }

    public string Author { get; set; }

    public DateTime? NoteDate { get; set; }
  }
}

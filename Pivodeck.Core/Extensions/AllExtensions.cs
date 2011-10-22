using PivotalTracker.FluentAPI.Domain;

namespace Pivodeck.Core.Extensions
{
    public static class AllExtensions
    {
        public static string ToMyString(this Note note)
        {
            return string.Format("Autor:{0}\nData: {1}\nDescrição: {2}", note.Author, note.NoteDate.Value.ToShortDateString(), note.Description);
        }
    }
}

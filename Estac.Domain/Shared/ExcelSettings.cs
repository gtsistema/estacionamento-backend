namespace Estac.Domain.Shared
{
    public class ExcelSettings
    {
        public string Path { get; set; }
        public string FileName { get; set; }

        public string GetFullPath()
        {
            return System.IO.Path.Combine(Path, FileName);
        }
    }
}

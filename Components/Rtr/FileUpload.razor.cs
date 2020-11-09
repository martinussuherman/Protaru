namespace Protaru.Components.Rtr
{
    public partial class FileUpload
    {
        private bool IsFilePathExists()
        {
            return !string.IsNullOrEmpty(FilePath);
        }
        private string InputDivClass()
        {
            return IsFilePathExists() ? "custom-file d-none" : "custom-file";
        }
        private string LinkClass()
        {
            return IsFilePathExists() ? "file-info" : "file-info d-none";
        }
        private string LinkPath()
        {
            return IsFilePathExists() ? Url.Content("~" + FilePath) : string.Empty;
        }
    }
}
namespace Protaru.Components.Rtr
{
    public partial class ViewFile
    {
        private string GetClass()
        {
            return RtrDokumen.FilePathAda ? "file-info" : "file-info d-none";
        }

        private string GetPath()
        {
            return RtrDokumen.FilePathAda ? Url.Content("~" + RtrDokumen.FilePath) : "";
        }
    }
}
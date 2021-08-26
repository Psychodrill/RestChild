using RestChild.Web.Properties;
using System.IO;

namespace RestChild.Web.Handlers
{
    public class UploadPupilFilesHandler : UploadHandlerBase
    {
        protected override string StorageRoot => Path.Combine(Settings.Default.StoragePupils); //Path should! always end with '/'
    }
}

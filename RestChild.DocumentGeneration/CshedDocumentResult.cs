using RestChild.Comon;

namespace RestChild.DocumentGeneration
{
    public struct CshedDocumentResult : ICshedDocument
    {
        public CshedDocumentResult(IDocument doc)
        {
            FileBody = doc.FileBody;
            FileName = doc.FileName;
            MimeType = doc.MimeType;
            MimeTypeShort = doc.MimeTypeShort;
            RequestFileTypeId = doc.RequestFileTypeId;

            SsoId = null;
            CodeChed = null;
            CodeAsGuf = null;
            RequestId = 0;
        }

        public byte[] FileBody { get; set; }

        public string FileName { get; set; }

        public string MimeTypeShort { get; set; }

        public string MimeType { get; set; }

        public int FileLengt => FileBody?.Length ?? 0;

        public string SsoId { get; set; }

        public string CodeChed { get; set; }

        public string CodeAsGuf { get; set; }

        public long RequestId { get; set; }

        public long? RequestFileTypeId { get; set; }
    }
}

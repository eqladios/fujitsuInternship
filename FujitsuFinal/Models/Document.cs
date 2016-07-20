using System;
using System.Data.Entity;

namespace FujitsuFinal.Models
{
    public class Document
    {
        public int documentID { get; set; }
        public string documentTitle { get; set; }
        public string documentExtension { get; set; }
        public string documentPath { get; set; }
        public string documentSize { get; set; }
        public Nullable<DateTime> documentUploadedAt { get; set; }
    }

    public class DocumentDBContext : DbContext
    {
        public DbSet<Document> Documents { get; set; }
    }
}
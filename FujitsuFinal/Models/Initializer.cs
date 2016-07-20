using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace FujitsuFinal.Models
{
    public class Initializer : DropCreateDatabaseIfModelChanges<DocumentDBContext>
    {
        protected override void Seed(DocumentDBContext context) 
        {
            var documents = new List<Document> 
            {  
                 new Document 
                 {
                        documentID = 1, 
                        documentExtension=".txt",
                        documentPath = "/path/",
                        documentSize = "100B",
                        documentTitle = "Test",
                        documentUploadedAt = DateTime.Now
                 }  
             };

            documents.ForEach(d => context.Documents.Add(d));
        }
    }
}
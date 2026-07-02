using System;
using System.IO;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Aspose.Words; // Assuming Aspose.Words is used for conversion

namespace RAMCO_Workflow_Activities.Plugins
{
    public partial class ConvertWordAttachmentToPDF : BaseWorkflow
    {
        protected override void ExecuteInternal(LocalWorkflowContext context)
        {
            // Use the context's PrimaryEntityId as the target record
            EntityReference target = new EntityReference(context.WorkflowContext.PrimaryEntityName, context.WorkflowContext.PrimaryEntityId);

            // Query to get the most recent note
            QueryExpression query = new QueryExpression("annotation")
            {
                TopCount = 1,
                Criteria = new FilterExpression
                {
                    Conditions =
                    {
                        new ConditionExpression("objectid", ConditionOperator.Equal, target.Id),
                        new ConditionExpression("isdocument", ConditionOperator.Equal, true)
                    }
                },
                Orders =
                {
                    new OrderExpression("createdon", OrderType.Descending)
                }
            };

            EntityCollection notes = context.OrganizationService.RetrieveMultiple(query);

            // Process the note if found
            if (notes.Entities.Count > 0)
            {
                Entity note = notes.Entities[0];
                string base64Document = note["documentbody"].ToString();
                string fileName = note["filename"].ToString();

                // Convert Word to PDF
                byte[] documentBytes = Convert.FromBase64String(base64Document);
                MemoryStream wordStream = new MemoryStream(documentBytes);

                Document wordDocument = new Document(wordStream);
                MemoryStream pdfStream = new MemoryStream();
                wordDocument.Save(pdfStream, SaveFormat.Pdf);

                // Create a new note with the PDF
                string pdfBase64 = Convert.ToBase64String(pdfStream.ToArray());
                Entity newNote = new Entity("annotation");
                newNote["subject"] = "Converted PDF";
                newNote["notetext"] = "This PDF was converted from a Word document.";
                newNote["documentbody"] = pdfBase64;
                newNote["mimetype"] = "application/pdf";
                newNote["filename"] = Path.ChangeExtension(fileName, ".pdf");
                newNote["objectid"] = new EntityReference(target.LogicalName, target.Id);

                context.OrganizationService.Create(newNote);
            }
        }
    }
}

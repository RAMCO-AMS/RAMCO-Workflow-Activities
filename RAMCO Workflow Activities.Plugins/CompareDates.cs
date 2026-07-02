using System;
using System.Linq;

using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Crm.Sdk.Messages;
using System.Collections.Generic;

namespace RAMCO_Workflow_Activities.Plugin
{

    public class DateComparisonActivity : CodeActivity
    {
        // Define the input arguments
        [Input("Date 1")]
        public InArgument<DateTime> Date1 { get; set; }

        [Input("Date 2")]
        public InArgument<DateTime> Date2 { get; set; }

        [Input("Date 3")]
        public InArgument<DateTime> Date3 { get; set; }

        [Input("Date 4")]
        public InArgument<DateTime> Date4 { get; set; }

        [Input("Date 5")]
        public InArgument<DateTime> Date5 { get; set; }

        [Input("Date 6")]
        public InArgument<DateTime> Date6 { get; set; }

        [Input("Date 7")]
        public InArgument<DateTime> Date7 { get; set; }

        [Input("Date 8")]
        public InArgument<DateTime> Date8 { get; set; }

        [Input("Date 9")]
        public InArgument<DateTime> Date9 { get; set; }

        [Input("Date 10")]
        public InArgument<DateTime> Date10 { get; set; }

        [Output("Oldest Date")]
        public OutArgument<DateTime> OldestDate { get; set; }

        [Output("Newest Date")]
        public OutArgument<DateTime> NewestDate { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            // Retrieve the workflow context
            IWorkflowContext context = executionContext.GetExtension<IWorkflowContext>();
            IOrganizationServiceFactory serviceFactory = executionContext.GetExtension<IOrganizationServiceFactory>();
            IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);
            //instantiate tracing service
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            // Collect the dates into an array, filtering out null values

            tracer.Trace("Collecting dates");
            List<DateTime> dates = new List<DateTime>();

            AddDateIfValid(Date1.Get(executionContext), dates);
            AddDateIfValid(Date2.Get(executionContext), dates);
            AddDateIfValid(Date3.Get(executionContext), dates);
            AddDateIfValid(Date4.Get(executionContext), dates);
            AddDateIfValid(Date5.Get(executionContext), dates);
            AddDateIfValid(Date6.Get(executionContext), dates);
            AddDateIfValid(Date7.Get(executionContext), dates);
            AddDateIfValid(Date8.Get(executionContext), dates);
            AddDateIfValid(Date9.Get(executionContext), dates);
            AddDateIfValid(Date10.Get(executionContext), dates);



            tracer.Trace("Checking if no dates were provided");
            // Check if there are any dates
            if (dates.Count == 0)
            {
                // No dates provided, set outputs to null
                OldestDate.Set(executionContext, null);
                NewestDate.Set(executionContext, null);
                return;
            }

            tracer.Trace("Finding oldest and newest dates");
            // Find the oldest and newest dates
            var oldest = dates.Min();
            var newest = dates.Max();

            // Set the output arguments
            OldestDate.Set(executionContext, oldest);
            NewestDate.Set(executionContext, newest);
        }

        private void AddDateIfValid(DateTime date, List<DateTime> dates)
        {
            if (date != DateTime.MinValue) // Or use any other default/invalid value check
            {
                dates.Add(date);
            }
        }
    }
}

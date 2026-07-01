using System;
using System.Collections.Generic;
using System.Linq;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;

// ─────────────────────────────────────────────────────────────────────────────
// RECONSTRUCTED custom workflow activity.
//
// This type was compiled into the deployed assembly (build 1.0.8714.26841, still
// registered in the 'canopy' org) but was NEVER committed to the source repo
// RAMCO-AMS/RAMCO-Workflow-Activities. Solution build 2.0.0.0 (1.0.8903.28701)
// dropped it, which is why importing that solution into canopy fails: the two
// workflows 'Test - Compare Dates' and 'WFH - Update - Child - Set new Class
// Expiration Date with Oldest Class Completion Date' still reference it.
//
// The public contract below (Date1..Date10 in; OldestDate/NewestDate out, all
// DateTime) is EXACT — recovered from the deployed assembly's metadata and from
// the consuming workflows' XAML (<ActivityReference> argument keys). The Execute
// body is a behavioral reconstruction (oldest = min, newest = max, ignoring
// unset dates). For a byte-exact original, decompile
//   recovered/RAMCOWorkflowActivities_canopy_8714.dll
// with ILSpy / dotPeek and replace the body if it differs.
//
// The namespace is intentionally the SINGULAR "RAMCO_Workflow_Activities.Plugin"
// (the sibling activities use the plural "...Plugins"). It must stay singular so
// the plugin type name remains exactly
//   RAMCO_Workflow_Activities.Plugin.DateComparisonActivity
// which is what canopy has registered — so an assembly update matches the
// existing type instead of dropping and recreating it.
// ─────────────────────────────────────────────────────────────────────────────

namespace RAMCO_Workflow_Activities.Plugin
{
    public partial class DateComparisonActivity : CodeActivity
    {
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

        protected override void Execute(CodeActivityContext context)
        {
            // An unset InArgument<DateTime> reads back as DateTime.MinValue (default);
            // treat those as "not supplied" so partially-filled inputs still work.
            var dates = new List<DateTime>
            {
                Date1.Get(context),
                Date2.Get(context),
                Date3.Get(context),
                Date4.Get(context),
                Date5.Get(context),
                Date6.Get(context),
                Date7.Get(context),
                Date8.Get(context),
                Date9.Get(context),
                Date10.Get(context),
            }
            .Where(d => d != DateTime.MinValue)
            .ToList();

            if (dates.Count == 0)
                return; // nothing supplied — leave outputs unset

            OldestDate.Set(context, dates.Min());
            NewestDate.Set(context, dates.Max());
        }
    }
}

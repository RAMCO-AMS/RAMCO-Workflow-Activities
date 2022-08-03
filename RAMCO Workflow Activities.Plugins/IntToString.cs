using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using Microsoft.Crm.Sdk.Messages;

namespace RAMCO_Workflow_Activities.Plugins
{
    public partial class IntToString : CodeActivity
    {
        // This is an example argument
        [RequiredArgument]
        [Input("Whole Number to be Converted")]
        public InArgument<int> IntReference { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            // Todo: implement your logic here
            int intReference = context.GetValue(this.IntReference);
            this.OutputString.Set(context, intReference.ToString());


        }

        [Output("Number as string")]
        public OutArgument<string> OutputString { get; set; }
    }
}


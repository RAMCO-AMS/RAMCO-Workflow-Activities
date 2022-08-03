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
    public partial class NumberStringToInt : CodeActivity
    {
        // This is an example argument
        [RequiredArgument]
        [Input("String to become a Number")]
        public InArgument<string> StringReference { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            // Todo: implement your logic here
            string stringReferenceUntrimmmed = context.GetValue<string>(this.StringReference);
            string stringReference = stringReferenceUntrimmmed.Trim();
            this.OutputInt.Set(context, Int32.Parse(stringReference));
           
        }

        [Output("String as Number")]
        public OutArgument<int> OutputInt { get; set; }
    }
}


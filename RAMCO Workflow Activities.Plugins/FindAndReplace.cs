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
    public partial class FindAndReplace : CodeActivity
    {
        // This is an example argument
        [RequiredArgument]
        [Input("Find")]
        public InArgument<String> FindString { get; set; }

        [Input("Replace")]
        public InArgument<String> ReplaceString { get; set; }

        [RequiredArgument]
        [Input("Seed String")]
        public InArgument<String> SeedString { get; set; }

        [RequiredArgument]
        [Output("Replaced String")]
        public OutArgument<String> OutString { get; set; }

        protected override void Execute(CodeActivityContext context)
        {
            // Todo: implement your logic here
            string seedString = context.GetValue(SeedString);
            string findString = context.GetValue(FindString);
            string replaceString = context.GetValue(ReplaceString);


            if (string.IsNullOrEmpty(replaceString))
            {
                this.OutString.Set(context, seedString.Replace(findString, ""));
            }
            else
            {
                this.OutString.Set(context, seedString.Replace(findString, replaceString));
            }       
            


        }
    }
}


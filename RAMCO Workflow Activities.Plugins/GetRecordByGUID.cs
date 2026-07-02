using System;
using System.Collections.Generic;
using System.Linq;
using System.Activities;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;

// -----------------------------------------------------------------------------
// RECONSTRUCTED custom workflow activity "Get Record by ID".
//
// Present in the shipped v2 assembly (build 1.0.8903.28701) but never committed
// to source. The PUBLIC CONTRACT below is EXACT - recovered by reflection over the
// deployed DLL (property names, argument types, [Input]/[Output]/[ReferenceTarget]/
// [AttributeTarget] attributes) and cross-checked against the ramco_tabletoretrieve
// option set in the solution (option value -> table, 379120000..379120030).
//
// The ExecuteInternal BODY is a behavioral reconstruction: parse the GUID string,
// switch on the selected Table-to-Retrieve option, Retrieve that record to confirm
// it exists, set the matching EntityReference output and IsRetrievalSuccessful.
// Decompile recovered/RAMCOWorkflowActivities_v2_8903.dll with ILSpy/dotPeek and
// replace the body if byte-exact behavior matters.
// -----------------------------------------------------------------------------

namespace RAMCO_Workflow_Activities.Plugins
{
    public partial class GetRecordByGUID : BaseWorkflow
    {
        [RequiredArgument]
        [Input("Primary Key (GUID)")]
        public InArgument<string> RecordId { get; set; }

        [RequiredArgument]
        [Input("Table to Retrieve")]
        [AttributeTarget("ramco_workflowactivitysetting", "ramco_tabletoretrieve")]
        public InArgument<OptionSetValue> TableToRetrieve { get; set; }

        [Output("Is Retrieval Successful")]
        public OutArgument<bool> IsRetrievalSuccessful { get; set; }

        [Output("Account")]
        [ReferenceTarget("account")]
        public OutArgument<EntityReference> Account { get; set; }
        [Output("Class")]
        [ReferenceTarget("cobalt_class")]
        public OutArgument<EntityReference> Class { get; set; }
        [Output("Class Registration Fee")]
        [ReferenceTarget("cobalt_classregistrationfee")]
        public OutArgument<EntityReference> ClassRegistrationFee { get; set; }
        [Output("Committee")]
        [ReferenceTarget("cobalt_committee")]
        public OutArgument<EntityReference> Committee { get; set; }
        [Output("Committee Membership")]
        [ReferenceTarget("cobalt_committeemembership")]
        public OutArgument<EntityReference> CommitteeMembership { get; set; }
        [Output("Committee Nomination")]
        [ReferenceTarget("cobalt_committeenomination")]
        public OutArgument<EntityReference> CommitteeNomination { get; set; }
        [Output("Contact")]
        [ReferenceTarget("contact")]
        public OutArgument<EntityReference> Contact { get; set; }
        [Output("Contribution")]
        [ReferenceTarget("ramco_contribution")]
        public OutArgument<EntityReference> Contribution { get; set; }
        [Output("Designation")]
        [ReferenceTarget("ramco_designation")]
        public OutArgument<EntityReference> Designation { get; set; }
        [Output("Dues Cycle")]
        [ReferenceTarget("cobalt_duescycle")]
        public OutArgument<EntityReference> DuesCycle { get; set; }
        [Output("Dues Item")]
        [ReferenceTarget("cobalt_duesitem")]
        public OutArgument<EntityReference> DuesItem { get; set; }
        [Output("Dues Option")]
        [ReferenceTarget("cobalt_duesoption")]
        public OutArgument<EntityReference> DuesOption { get; set; }
        [Output("Meeting")]
        [ReferenceTarget("cobalt_meeting")]
        public OutArgument<EntityReference> Meeting { get; set; }
        [Output("Meeting Activity")]
        [ReferenceTarget("cobalt_meetingactivity")]
        public OutArgument<EntityReference> MeetingActivity { get; set; }
        [Output("Meeting Activity Registration Fee")]
        [ReferenceTarget("cobalt_meetingactivityregfee")]
        public OutArgument<EntityReference> MeetingActivityRegistrationFee { get; set; }
        [Output("Meeting Registration Fee")]
        [ReferenceTarget("cobalt_meetingregistrationfee")]
        public OutArgument<EntityReference> MeetingRegistrationFee { get; set; }
        [Output("Membership Application")]
        [ReferenceTarget("cobalt_membershipapplication")]
        public OutArgument<EntityReference> MembershipApplication { get; set; }
        [Output("Member Financial")]
        [ReferenceTarget("ramco_memberfinancial")]
        public OutArgument<EntityReference> MemberFinancial { get; set; }
        [Output("Membership")]
        [ReferenceTarget("cobalt_membership")]
        public OutArgument<EntityReference> Membership { get; set; }
        [Output("Office Membership")]
        [ReferenceTarget("ramco_officemembership")]
        public OutArgument<EntityReference> OfficeMembership { get; set; }
        [Output("Office Transfer")]
        [ReferenceTarget("ramco_officetransfer")]
        public OutArgument<EntityReference> OfficeTransfer { get; set; }
        [Output("Order")]
        [ReferenceTarget("salesorder")]
        public OutArgument<EntityReference> Order { get; set; }
        [Output("Payment")]
        [ReferenceTarget("cobalt_payment")]
        public OutArgument<EntityReference> Payment { get; set; }
        [Output("Payment Profile")]
        [ReferenceTarget("cobalt_recurringpaymentprofile")]
        public OutArgument<EntityReference> PaymentProfile { get; set; }
        [Output("Publication")]
        [ReferenceTarget("cobalt_publication")]
        public OutArgument<EntityReference> Publication { get; set; }
        [Output("Publication Subscription")]
        [ReferenceTarget("cobalt_publicationsubscription")]
        public OutArgument<EntityReference> PublicationSubscription { get; set; }
        [Output("Refund")]
        [ReferenceTarget("cobalt_refund")]
        public OutArgument<EntityReference> Refund { get; set; }
        [Output("Scheduled Payment")]
        [ReferenceTarget("ramco_scheduledpayment")]
        public OutArgument<EntityReference> ScheduledPayment { get; set; }
        [Output("Scheduled Payment Detail")]
        [ReferenceTarget("ramco_scheduledpaymentdetail")]
        public OutArgument<EntityReference> ScheduledPaymentDetail { get; set; }
        [Output("State License")]
        [ReferenceTarget("ramco_statelicense")]
        public OutArgument<EntityReference> StateLicense { get; set; }
        [Output("Store Location")]
        [ReferenceTarget("ramco_storelocation")]
        public OutArgument<EntityReference> StoreLocation { get; set; }

        protected override void ExecuteInternal(LocalWorkflowContext context)
        {
            var ctx = context.CodeActivityContext;
            var service = context.OrganizationService;

            this.IsRetrievalSuccessful.Set(ctx, false);

            string recordIdRaw = this.RecordId.Get(ctx);
            OptionSetValue table = this.TableToRetrieve.Get(ctx);

            Guid recordId;
            if (table == null || !Guid.TryParse(recordIdRaw, out recordId))
            {
                context.Trace("GetRecordByGUID: missing/invalid RecordId or Table to Retrieve.");
                return;
            }

            string logicalName;
            Action<EntityReference> setOutput;
            switch (table.Value)
            {
                case 379120000: logicalName = "account"; setOutput = r => this.Account.Set(ctx, r); break; // Account
                case 379120001: logicalName = "cobalt_class"; setOutput = r => this.Class.Set(ctx, r); break; // Class
                case 379120002: logicalName = "cobalt_classregistrationfee"; setOutput = r => this.ClassRegistrationFee.Set(ctx, r); break; // Class Registration Fee
                case 379120003: logicalName = "cobalt_committee"; setOutput = r => this.Committee.Set(ctx, r); break; // Committee
                case 379120004: logicalName = "cobalt_committeemembership"; setOutput = r => this.CommitteeMembership.Set(ctx, r); break; // Committee Membership
                case 379120005: logicalName = "cobalt_committeenomination"; setOutput = r => this.CommitteeNomination.Set(ctx, r); break; // Committee Nomination
                case 379120006: logicalName = "contact"; setOutput = r => this.Contact.Set(ctx, r); break; // Contact
                case 379120007: logicalName = "ramco_contribution"; setOutput = r => this.Contribution.Set(ctx, r); break; // Contribution
                case 379120008: logicalName = "ramco_designation"; setOutput = r => this.Designation.Set(ctx, r); break; // Designation
                case 379120009: logicalName = "cobalt_duescycle"; setOutput = r => this.DuesCycle.Set(ctx, r); break; // Dues Cycle
                case 379120010: logicalName = "cobalt_duesitem"; setOutput = r => this.DuesItem.Set(ctx, r); break; // Dues Item
                case 379120011: logicalName = "cobalt_duesoption"; setOutput = r => this.DuesOption.Set(ctx, r); break; // Dues Option
                case 379120012: logicalName = "cobalt_meeting"; setOutput = r => this.Meeting.Set(ctx, r); break; // Meeting
                case 379120013: logicalName = "cobalt_meetingactivity"; setOutput = r => this.MeetingActivity.Set(ctx, r); break; // Meeting Activity
                case 379120014: logicalName = "cobalt_meetingactivityregfee"; setOutput = r => this.MeetingActivityRegistrationFee.Set(ctx, r); break; // Meeting Activity Registration Fee
                case 379120015: logicalName = "cobalt_meetingregistrationfee"; setOutput = r => this.MeetingRegistrationFee.Set(ctx, r); break; // Meeting Registration Fee
                case 379120016: logicalName = "cobalt_membershipapplication"; setOutput = r => this.MembershipApplication.Set(ctx, r); break; // Membership Application
                case 379120017: logicalName = "ramco_memberfinancial"; setOutput = r => this.MemberFinancial.Set(ctx, r); break; // Member Financial
                case 379120018: logicalName = "cobalt_membership"; setOutput = r => this.Membership.Set(ctx, r); break; // Membership
                case 379120019: logicalName = "ramco_officemembership"; setOutput = r => this.OfficeMembership.Set(ctx, r); break; // Office Membership
                case 379120020: logicalName = "ramco_officetransfer"; setOutput = r => this.OfficeTransfer.Set(ctx, r); break; // Office Transfer
                case 379120021: logicalName = "salesorder"; setOutput = r => this.Order.Set(ctx, r); break; // Order
                case 379120022: logicalName = "cobalt_payment"; setOutput = r => this.Payment.Set(ctx, r); break; // Payment
                case 379120023: logicalName = "cobalt_recurringpaymentprofile"; setOutput = r => this.PaymentProfile.Set(ctx, r); break; // Payment Profile
                case 379120024: logicalName = "cobalt_publication"; setOutput = r => this.Publication.Set(ctx, r); break; // Publication
                case 379120025: logicalName = "cobalt_publicationsubscription"; setOutput = r => this.PublicationSubscription.Set(ctx, r); break; // Publication Subscription
                case 379120026: logicalName = "cobalt_refund"; setOutput = r => this.Refund.Set(ctx, r); break; // Refund
                case 379120027: logicalName = "ramco_scheduledpayment"; setOutput = r => this.ScheduledPayment.Set(ctx, r); break; // Scheduled Payment
                case 379120028: logicalName = "ramco_scheduledpaymentdetail"; setOutput = r => this.ScheduledPaymentDetail.Set(ctx, r); break; // Scheduled Payment Detail
                case 379120029: logicalName = "ramco_statelicense"; setOutput = r => this.StateLicense.Set(ctx, r); break; // State License
                case 379120030: logicalName = "ramco_storelocation"; setOutput = r => this.StoreLocation.Set(ctx, r); break; // Store Location
                default:
                    context.Trace("GetRecordByGUID: unmapped Table to Retrieve option " + table.Value + ".");
                    return;
            }

            try
            {
                Entity record = service.Retrieve(logicalName, recordId, new ColumnSet(false));
                setOutput(record.ToEntityReference());
                this.IsRetrievalSuccessful.Set(ctx, true);
            }
            catch (Exception ex)
            {
                context.Trace("GetRecordByGUID: retrieve of " + logicalName + " " + recordId + " failed: " + ex.Message);
                this.IsRetrievalSuccessful.Set(ctx, false);
            }
        }
    }
}

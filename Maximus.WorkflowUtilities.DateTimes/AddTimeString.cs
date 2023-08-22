using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace Maximus.WorkflowUtilities.DateTimes
{
    public sealed class AddTimeString : CodeActivity
    {
        [RequiredArgument]
        [Input("Original Date")]
        public InArgument<DateTime> OriginalDate { get; set; }

        [Input("Hours To Add <string>")]
        public InArgument<string> HoursToAdd { get; set; }

        [Input("Minutes To Add <string>")]
        public InArgument<string> MinutesToAdd { get; set; }

        [OutputAttribute("Updated Date")]
        public OutArgument<DateTime> UpdatedDate { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                DateTimeOffset originalDate = OriginalDate.Get(executionContext);
                string hours = HoursToAdd.Get(executionContext);
                string minutes = MinutesToAdd.Get(executionContext);

                int hoursToAdd;
                int minutesToAdd;

                try
                {
                    hoursToAdd = Int16.Parse(hours);
                }
                catch (FormatException e)
                {
                    hoursToAdd = 0;
                }

                try
                {
                    minutesToAdd = Int16.Parse(minutes);
                }
                catch (FormatException e)
                {
                    minutesToAdd = 0;
                }

                DateTimeOffset updatedDate = originalDate.AddHours(hoursToAdd);
                updatedDate = updatedDate.AddMinutes(minutesToAdd);
                UpdatedDate.Set(executionContext, updatedDate.DateTime);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
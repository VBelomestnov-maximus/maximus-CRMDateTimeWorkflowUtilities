using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;

namespace Maximus.WorkflowUtilities.DateTimes
{
    public sealed class DateDiffDays : CodeActivity
    {
        [RequiredArgument]
        [Input("Starting Date")]
        public InArgument<DateTime> StartingDate { get; set; }

        [RequiredArgument]
        [Input("Ending Date")]
        public InArgument<DateTime> EndingDate { get; set; }

        [OutputAttribute("Days Difference")]
        public OutArgument<int> DaysDifference { get; set; }

        protected override void Execute(CodeActivityContext executionContext)
        {
            ITracingService tracer = executionContext.GetExtension<ITracingService>();

            try
            {
                DateTime startingDate = StartingDate.Get(executionContext);
                DateTime endingDate = EndingDate.Get(executionContext);

                TimeSpan difference = startingDate - endingDate;

                int daysDifference = Math.Abs(Convert.ToInt32(difference.TotalDays));

                DaysDifference.Set(executionContext, daysDifference);
            }
            catch (Exception ex)
            {
                tracer.Trace("Exception: {0}", ex.ToString());
            }
        }
    }
}
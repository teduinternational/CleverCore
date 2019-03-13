using CleverCore.Application.ViewModels.Common;

namespace CleverCore.WebApp.Models
{
    public class ContactPageViewModel
    {
        public ContactViewModel Contact { set; get; }

        public FeedbackViewModel Feedback { set; get; }
    }
}

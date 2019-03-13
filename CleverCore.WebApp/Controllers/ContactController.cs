using System.Threading.Tasks;
using CleverCore.Application.Interfaces;
using CleverCore.Utilities.Constants;
using CleverCore.WebApp.Models;
using CleverCore.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CleverCore.WebApp.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;
        private readonly IFeedbackService _feedbackService;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;
        private readonly IViewRenderService _viewRenderService;

        public ContactController(IContactService contactSerivce,
            IViewRenderService viewRenderService,
            IConfiguration configuration,
            IEmailSender emailSender, IFeedbackService feedbackService)
        {
            _contactService = contactSerivce;
            _feedbackService = feedbackService;
            _emailSender = emailSender;
            _configuration = configuration;
            _viewRenderService = viewRenderService;
        }
        [Route("contact.html")]
        [HttpGet]
        public IActionResult Index()
        {
            var contact = _contactService.GetById(CommonConstants.DefaultContactId);
            var model = new ContactPageViewModel { Contact = contact };
            return View(model);
        }

        [Route("contact.html")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Index(ContactPageViewModel model)
        {
            if (ModelState.IsValid)
            {
                _feedbackService.Add(model.Feedback);
                _feedbackService.SaveChanges();
                var content = await _viewRenderService.RenderToStringAsync("Contact/_ContactMail", model.Feedback);
                await _emailSender.SendEmailAsync(_configuration["MailSettings:AdminMail"], "Have new contact feedback", content);
                ViewData["Success"] = true;
            }

            model.Contact = _contactService.GetById("default");

            return View("Index", model);
        }
    }
}
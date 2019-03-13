using System.Collections.Generic;
using CleverCore.Application.ViewModels.Common;

namespace CleverCore.Application.Interfaces
{
    public interface ICommonService
    {
        FooterViewModel GetFooter();
        List<SlideViewModel> GetSlides(string groupAlias);
        SystemConfigViewModel GetSystemConfig(string code);
    }
}

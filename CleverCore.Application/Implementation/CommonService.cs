using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleverCore.Application.Interfaces;
using CleverCore.Application.ViewModels.Common;
using CleverCore.Data.Entities;
using CleverCore.Infrastructure.Interfaces;
using CleverCore.Utilities.Constants;

namespace CleverCore.Application.Implementation
{
    public class CommonService : ICommonService
    {
        readonly IRepository<Footer, string> _footerRepository;
        readonly IRepository<SystemConfig, string> _systemConfigRepository;
        IUnitOfWork _unitOfWork;
        readonly IRepository<Slide, int> _slideRepository;
        public CommonService(IRepository<Footer, string> footerRepository,
            IRepository<SystemConfig, string> systemConfigRepository,
            IUnitOfWork unitOfWork,
            IRepository<Slide, int> slideRepository)
        {
            _footerRepository = footerRepository;
            _unitOfWork = unitOfWork;
            _systemConfigRepository = systemConfigRepository;
            _slideRepository = slideRepository;
        }

        public FooterViewModel GetFooter()
        {
            return Mapper.Map<Footer, FooterViewModel>(_footerRepository.FindSingle(x => x.Id ==
            CommonConstants.DefaultFooterId));
        }

        public List<SlideViewModel> GetSlides(string groupAlias)
        {
            return _slideRepository.FindAll(x => x.Status && x.GroupAlias == groupAlias)
                .ProjectTo<SlideViewModel>().ToList();
        }

        public SystemConfigViewModel GetSystemConfig(string code)
        {
            return Mapper.Map<SystemConfig, SystemConfigViewModel>(_systemConfigRepository.FindSingle(x => x.Id == code));
        }
    }
}

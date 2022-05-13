using AutoMapper;
using KCMS.Domain.Advertising;
using KCMS.Domain.Base;
using KCMS.Domain.ViewModel;
using KCMS.Ultitlies;
using Microsoft.AspNetCore.Hosting;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace KCMS.Services
{
    public class AdversitingService
    {
        private readonly IAdvertisingRepository _advertisingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHostingEnvironment _hostingEnvironment;

        public AdversitingService(IAdvertisingRepository advertisingRepository, IUnitOfWork unitOfWork, IMapper mapper, IHostingEnvironment hostingEnvironment)
        {
            _advertisingRepository = advertisingRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _hostingEnvironment = hostingEnvironment;
        }

        public AdvertisingListViewModel GetAdvertisings(int pageNumber, int pageSize, string searchValue)
        {
            Expression<Func<Advertising, bool>> filter = a =>
            (String.IsNullOrEmpty(searchValue) || ((a.Url.Contains(searchValue)) || (a.Id.ToString() == searchValue) || (a.Title.Contains(searchValue)) || (a.Type.Contains(searchValue))
            || (searchValue.Equals(AdvertisingStatus.Active.ToString()) || (a.Status == AdvertisingStatus.Active))
            || (searchValue.Equals(AdvertisingStatus.InActice.ToString()) || (a.Status == AdvertisingStatus.InActice))));

            var advertisings = _advertisingRepository.Get(filter, a => a.OrderByDescending(a => a.CreatedDate)).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

            return new AdvertisingListViewModel
            {
                Results = advertisings,
                TotalPages = _advertisingRepository.GetTotalPages(filter, pageSize)
            };
        }

        public Advertising GetAdvertising(long id)
        {
            var advertising = _advertisingRepository.GetByID(id);

            if (advertising == null)
            {
                throw new Exception("NotFound");
            }

            return advertising;
        }

        public async Task<Advertising> AddAdvertising(AdvertisingInsertModel model)
        {
            var fileName = "";
            if (model.Image != null)
            {
                fileName = FileUlti.UploadFile(model.Image, _hostingEnvironment);
            }

            var advertising = _mapper.Map<Advertising>(model);
            advertising.Image = fileName;
            advertising.CreatedDate = DateTime.UtcNow;
            _advertisingRepository.Insert(advertising);

            try
            {
                await _unitOfWork.CommitAsync();
                return advertising;
            }
            catch (Exception ex)
            {
                throw new Exception("Insert Fail : " + ex);
            }
        }

        public async Task UpdateAdvertising(AdvertisingUpdateModel model)
        {
            var advertising = _advertisingRepository.GetByID(model.Id);

            if (advertising == null)
            {
                throw new Exception("NotFound");
            }

            if (model.Image != null)
            {
                FileUlti.DeleteFile(advertising.Image, _hostingEnvironment);
                var fileName = FileUlti.UploadFile(model.Image, _hostingEnvironment);
                advertising.Image = fileName;
            }

            advertising.Options = model.Options;
            advertising.Position = model.Position;
            advertising.Priority = model.Priority;
            advertising.Status = model.Status;
            advertising.Title = model.Title;
            advertising.Type = model.Type;
            advertising.Url = model.Url;
            advertising.UpdatedDate = DateTime.UtcNow;

            try
            {
                _advertisingRepository.Update(advertising);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Update Fail : " + ex);
            }
        }

        public async Task<Advertising> DeleteAdvertising(long id)
        {
            var advertising = _advertisingRepository.GetByID(id);
            var image = advertising.Image;

            if (advertising == null)
            {
                throw new Exception("NotFound");
            }

            _advertisingRepository.Delete(advertising);
            await _unitOfWork.CommitAsync();
            FileUlti.DeleteFile(image, _hostingEnvironment);
            return advertising;
        }
    }
}

using Hospital.ViewModels;
using Hospital.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Hospital.Repositories.Interfaces;
using Hospital.Utilities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
namespace Hospital.Services;
public class HospitalInfoService : IHospitalInfo
{
    private readonly IUnitOfWork _unitOfWork;
    public HospitalInfoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public void DeleteHospitalInfo(int id) 
    {
        var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(id);
        if (model == null)
        {
            throw new Exception ("Hospital info not found.");
        }
        _unitOfWork.GenericRepository<HospitalInfo>().Delete(model);
        _unitOfWork.Save();
    }
    public PagedResult<HospitalInfoViewModel> GetAll(int pageNumber, int pageSize)
    {
        List<HospitalInfoViewModel> vmList = new List<HospitalInfoViewModel>();
        int totalCount;
        try
        {
            int excludeRecords = (pageSize * pageNumber) - pageSize;

            // Fetch data with pagination using the GenericRepository from UnitOfWork
            var modelList = _unitOfWork.GenericRepository<HospitalInfo>().GetAll()
                .Skip(excludeRecords).Take(pageSize).ToList();
            // Get total count for pagination purposes
            totalCount = _unitOfWork.GenericRepository<HospitalInfo>().GetAll().Count();
            // Convert model list to view model list
            vmList = ConvertModelToViewModelList(modelList);

            return new PagedResult<HospitalInfoViewModel>
            {
                Data = vmList,
                TotalItems = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
            };

        }
        catch (Exception ex)
        {

            return new PagedResult<HospitalInfoViewModel>
            {
                //Data = vmList,
                //TotalItems = totalCount,
                //PageNumber = pageNumber,
                //PageSize = pageSize,
            };

        }
        // Constructing the paged result
    }
    public HospitalInfoViewModel GetHospitalById(int hospitalId)
    {
        var model = _unitOfWork.GenericRepository<HospitalInfo>().GetById(hospitalId);
        if (model == null)
        {
            throw new Exception("Hospital info not found.");
        }
        return new HospitalInfoViewModel(model);
    }
    public void InsertHospitalInfo(HospitalInfoViewModel hospitalInfo)
    {
        var model = new HospitalInfo
        {
            Name = hospitalInfo.Name,
            Address = hospitalInfo.Address,
            City = hospitalInfo.City,
            Country = hospitalInfo.Country,
            Type = hospitalInfo.Type,
            PinCode = hospitalInfo.PinCode,
        };
        _unitOfWork.GenericRepository<HospitalInfo>().Add(model);
        _unitOfWork.Save();
    }
    public void UpdateHospitalInfo(HospitalInfoViewModel hospitalInfo)
    {
        var modelById = _unitOfWork.GenericRepository<HospitalInfo>().GetById(hospitalInfo.Id);
        if (modelById == null)
        {
            throw new Exception("Hospital info not found.");
        }
        modelById.Name = hospitalInfo.Name;
        modelById.City = hospitalInfo.City;
        modelById.PinCode = hospitalInfo.PinCode;
        modelById.Country = hospitalInfo.Country;
        _unitOfWork.GenericRepository<HospitalInfo>().update(modelById);
        _unitOfWork.Save();
    }
    // Helper method to convert the list of HospitalInfo models to HospitalInfoViewModels
    private List<HospitalInfoViewModel> ConvertModelToViewModelList(List<HospitalInfo> modelList)
    {
        return modelList.Select(x => new HospitalInfoViewModel
        {
            Id = x.Id,
            Name = x.Name,
            Type = x.Type,
            City = x.City,
            Country = x.Country,
            PinCode = x.PinCode,
        }).ToList();
    }
}

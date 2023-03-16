using DataLayer.Models;
using DataLayer.Repositories.Address;
using ModelLayer.DTO;
using ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;

        public AddressService(IAddressRepository addressRepository) {
            _addressRepository = addressRepository;
        }

        public async Task<bool> AddNewAddress(AddressCreateDto addressCreateDto)
        {
            var addressEntity = new AddressEntity{
                StreetName = addressCreateDto.StreetName,
                Province =  addressCreateDto.Province,
                City = addressCreateDto.City,
                PostalCode = addressCreateDto.PostalCode == null ? "" : addressCreateDto.PostalCode,
                FullAddress = addressCreateDto.City + " " + addressCreateDto.Province + " " + addressCreateDto.StreetName + addressCreateDto.HouseNumber,
            };

            try
            {
                await _addressRepository.AddAddress(addressEntity);
                return true;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}

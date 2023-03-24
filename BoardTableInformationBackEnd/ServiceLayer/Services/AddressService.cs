using DataLayer.Models;
using DataLayer.Repositories.Address;
using DataLayer.Repositories.User;
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
        private readonly IUserRepository _userRepository;

        public AddressService(IAddressRepository addressRepository, IUserRepository userRepository) {
            _addressRepository = addressRepository;
            _userRepository = userRepository;
        }

        public async Task<bool> AddNewAddress(AddressCreateDto addressCreateDto)
        {
            var addressEntity = new AddressEntity{
                Country = addressCreateDto.Country,
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

        public async Task<bool> UpdateUserAddress(int id, UpdateUserAddress addressDto)
        {
            var addressId = await _addressRepository.CheckIfUserHasAddress(id);

            if(!addressId.HasValue)
            {
                var newAddress = new UserAddressEntity
                {
                    City = addressDto.Address.City,
                    Country = addressDto.Address.Country,
                    StreetName = addressDto.Address.StreetName,
                    Province = addressDto.Address.Province,
                    Map_X_Coords = addressDto.Address.Map_X_Coords,
                    Map_Y_Coords = addressDto.Address.Map_Y_Coords,

                };

                var result = await _addressRepository.AddAddressToUser(newAddress);
                await _userRepository.UpdateUserAddress(result.UserAddressId, id);
            }
            else
            {
                var newAddress = new UserAddressEntity
                {
                    UserAddressId = (int)addressId,
                    City = addressDto.Address.City,
                    Country = addressDto.Address.Country,
                    StreetName = addressDto.Address.StreetName,
                    Province = addressDto.Address.Province,
                    Map_X_Coords = addressDto.Address.Map_X_Coords,
                    Map_Y_Coords = addressDto.Address.Map_Y_Coords,

                };

                await _addressRepository.UpdateUserAddress(newAddress);

            }

            await _userRepository.UpdateInvitationNotification(id,addressDto.EnabledInvitationSettings);

            return true;
        }
    }
}

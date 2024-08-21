using AutoMapper;
using Fitness_Tracker.Data;
using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;
using Fitness_Tracker.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fitness_Tracker.Services
{
    public class DietService : IDietService
    {
        private readonly IDietRepository _dietRepository;
        private readonly IMapper _mapper;

        public DietService(IMapper mapper, IDietRepository dietRepository)
        {
            _mapper = mapper;
            _dietRepository = dietRepository;
        }

        public async Task<DietDto> AddDietAsync(DietDto dietDto)
        {
            var Diet = await _dietRepository.AddAsync(_mapper.Map<Diet>(dietDto));
            return _mapper.Map<DietDto>(Diet);
        }

        public async Task<bool> DeleteDietAsync(int id, int userId)
        {
            var Diet = await _dietRepository.DeleteAsync(id, userId);
            return Diet;
        }

        public async Task<DietDto> GetDietAsync(int id, int userId)
        {
            var Diet = await _dietRepository.GetDietAsync(id, userId);
            return _mapper.Map<DietDto>(Diet);
        }

        public async Task<List<DietDto>> GetDietListAsync(int userId)
        {
            var Diet = await _dietRepository.GetDietListAsync(userId);
            return _mapper.Map<List<DietDto>>(Diet);
        }

        public async Task<DietDto> UpdateDietAsync(DietDto dietDto)
        {
            var Diet = await _dietRepository.UpdateAsync(_mapper.Map<Diet>(dietDto));
            return _mapper.Map<DietDto>(Diet);
        }
    }
}

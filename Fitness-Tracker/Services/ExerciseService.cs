using AutoMapper;
using Fitness_Tracker.DTOs;
using Fitness_Tracker.Entities;
using Fitness_Tracker.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Fitness_Tracker.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IMapper _mapper;
        private readonly IExerciseRepository _exerciseRepository;

        public ExerciseService(IMapper mapper, IExerciseRepository exerciseRepository)
        {
            _mapper = mapper;
            _exerciseRepository = exerciseRepository;
        }

        public async Task<ExerciseDto> AddExercisesAsync(ExerciseDto exerciseDto)
        {
            Exercise exercise = new Exercise();
            exercise = _mapper.Map<Exercise>(exerciseDto);

            var addExercise = await _exerciseRepository.AddExercisesAsync(exercise);
            return _mapper.Map<ExerciseDto>(addExercise);
        }

        public async Task<bool> DeleteExerciseAsync(int id, int userId)
        {
           var deletedExercise = await _exerciseRepository.DeleteExerciseAsync(id, userId);
           return deletedExercise;
        }

        public async Task<List<ExerciseDto>> GetExerciseListAsync(int userId)
        {
            var exerciseList = await _exerciseRepository.GetExerciseListAsync(userId);
            return _mapper.Map<List<ExerciseDto>>(exerciseList);
        }

        public async Task<ExerciseDto> GetExercisesByIdAsync(int id, int userId)
        {
            var exercise = await _exerciseRepository.GetExercisesByIdAsync(id, userId);
            return _mapper.Map<ExerciseDto>(exercise);
        }

        public async Task<ExerciseDto> UpdateExerciseAsync(ExerciseDto exerciseDto)
        {
            Exercise exercise = new Exercise();
            exercise = _mapper.Map<Exercise>(exerciseDto);

            var updatedExercise = await _exerciseRepository.UpdateExerciseAsync(exercise);
            return _mapper.Map<ExerciseDto>(updatedExercise);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AmazingJourney.Infrastructure.Data;
using AmazingJourney_BE.AmazingJourney.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AmazingJourney.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByIdAsync(int id);
        Task<Category> AddCategoryAsync(Category category);
        Task<Category> UpdateCategoryAsync(int id, Category category);
        Task<bool> DeleteCategoryAsync(int id);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            foreach (var category in categories)
            {
                // Kiểm tra và xử lý giá trị NULL nếu cần
                category.Name ??= "Unknown"; // Hoặc giá trị mặc định khác
            }
            return categories;
        }

        public async Task<Category> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task<Category> AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int id, Category category)
        {
            var existingCategory = await _context.Categories.FindAsync(id);
            if (existingCategory == null)
                return null;

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

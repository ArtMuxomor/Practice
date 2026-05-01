using FinalTaskFinanceAPI.Data;
using FinalTaskFinanceAPI.DTO;
using FinalTaskFinanceAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalTaskFinanceAPI.Controllers
{
    /// <summary>
    /// API-контроллер категории расходов.
    /// </summary>
    // api/ExpenseCategories
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoriesController : ControllerBase
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly FinanceDbContext _context;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="context">Контекст БД.</param>
        public ExpenseCategoriesController(FinanceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список всех категорий расходов.
        /// </summary>
        /// <returns>Коллекция существующих категрий.</returns>
        // GET: api/ExpenseCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseCategory>>> GetCategories()
        {
            var categories = await _context.ExpenseCategories
              .Include(c => c.Items)
              .ToListAsync();

            // Преобразование каждой категории в формат DTO
            var result = categories.Select(c => new CategoryDto(c));

            return Ok(result);
        }

        /// <summary>
        /// Возвращает конкретную категорию расходов.
        /// </summary>
        /// <param name="name">Название искомой категории расходов.</param>
        /// <returns>Найденная категория расходов.</returns>
        // GET: api/ExpenseCategories/5
        [HttpGet("{name}")]
        public async Task<ActionResult<ExpenseCategory>> GetExpenseCategory(string name)
        {
            var category = await _context.ExpenseCategories
              .Include(c => c.Items)
              .FirstOrDefaultAsync(c => c.Name == name);

            if (category == null)
            {
                return NotFound();
            }

            // Возвращение преобразованной в DTO категории
            return Ok(new CategoryDto(category));
        }

        /// <summary>
        /// Обновляет информацию о категории расходов.
        /// </summary>
        /// <param name="name">Название категории расходов.</param>
        /// <param name="expenseCategory">Новый экземпляр категории расходов.</param>
        /// <returns>Результат обновления.</returns>
        // PUT: api/ExpenseCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutExpenseCategory(string name, ExpenseCategory expenseCategory)
        {
            if (name != expenseCategory.Name)
            {
                return BadRequest();
            }

            _context.Entry(expenseCategory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseCategoryExists(name))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Добавляет категорию расходов.
        /// </summary>
        /// <param name="expenseCategory">Добавляемая категория расходов.</param>
        /// <returns>Результат добавления.</returns>
        // POST: api/ExpenseCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseCategory>> PostExpenseCategory(ExpenseCategory expenseCategory)
        {
            _context.ExpenseCategories.Add(expenseCategory);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExpenseCategoryExists(expenseCategory.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExpenseCategory", new { name = expenseCategory.Name }, expenseCategory);
        }

        /// <summary>
        /// Удаляет категорию расходов.
        /// </summary>
        /// <param name="name">Название категории расходов.</param>
        /// <returns>Результат удаления.</returns>
        // DELETE: api/ExpenseCategories/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteExpenseCategory(string name)
        {
            var category = await _context.ExpenseCategories.FindAsync(name);
            if (category == null)
            {
                return NotFound();
            }
            try
            {
                _context.ExpenseCategories.Remove(category);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Нельзя удалить категорию, так как по её статьям уже есть совершённые транзакции.");
            }
        }

        /// <summary>
        /// Проверяет наличие категории расходов.
        /// </summary>
        /// <param name="name">Название искомой категории расходов.</param>
        /// <returns>Результат поиска.</returns>
        private bool ExpenseCategoryExists(string name)
        {
            return _context.ExpenseCategories.Any(e => e.Name == name);
        }
    }
}

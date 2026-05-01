using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalTaskFinanceAPI.Data;
using FinalTaskFinanceAPI.Models;
using NuGet.DependencyResolver;
using FinalTaskFinanceAPI.DTO;

namespace FinalTaskFinanceAPI.Controllers
{
    /// <summary>
    /// API-контроллер статьи расходов.
    /// </summary>
    // api/ExpenseItems
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseItemsController : ControllerBase
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly FinanceDbContext _context;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="context">Контекст БД.</param>
        public ExpenseItemsController(FinanceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список всех статей расходов.
        /// </summary>
        /// <returns>Коллекция существующих категрий расходов.</returns>
        // GET: api/ExpenseItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpenseItem>>> GetExpenseItems()
        {
            var items = await _context.ExpenseItems
              .Include(i => i.Transactions)
              .ToListAsync();

            var result = items.Select(i => new ItemDto(i));

            return Ok(result);
        }

        /// <summary>
        /// Возвращает конкретную статью расходов.
        /// </summary>
        /// <param name="name">Название искомой статьи расходов.</param>
        /// <returns>Найденная статья расходов.</returns>
        // GET: api/ExpenseItems/5
        [HttpGet("{name}")]
        public async Task<ActionResult<ExpenseItem>> GetExpenseItem(string name)
        {
            var item = await _context.ExpenseItems
              .Include(i => i.Transactions)
              .FirstOrDefaultAsync(i => i.Name == name);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(new ItemDto(item));
        }

        /// <summary>
        /// Обновляет информацию о статье расходов.
        /// </summary>
        /// <param name="name">Название статьи расходов.</param>
        /// <param name="expenseItem">Новый экземпляр статьи расходов.</param>
        /// <returns>Результат обновления.</returns>
        // PUT: api/ExpenseItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{name}")]
        public async Task<IActionResult> PutExpenseItem(string name, ExpenseItem expenseItem)
        {
            if (name != expenseItem.Name)
            {
                return BadRequest();
            }

            _context.Entry(expenseItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpenseItemExists(name))
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
        /// Добавляет статью расходов.
        /// </summary>
        /// <param name="expenseItem">Добавляемая статья расходов.</param>
        /// <returns>Результат добавления.</returns>
        // POST: api/ExpenseItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpenseItem>> PostExpenseItem(ExpenseItem expenseItem)
        {
            // Проверка указанной категории
            var category = await _context.ExpenseCategories
              .AsNoTracking()
              .FirstOrDefaultAsync(c => c.Name == expenseItem.ExpenseCategoryName);

            if (category == null)
            {
                return BadRequest("Указанная категория не существует.");
            }

            if (!category.IsActive)
            {
                return BadRequest("Нельзя добавлять статьи в неактивную категорию.");
            }

            _context.ExpenseItems.Add(expenseItem);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ExpenseItemExists(expenseItem.Name))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetExpenseItem", new { name = expenseItem.Name }, expenseItem);
        }

        /// <summary>
        /// Удаляет статью расходов.
        /// </summary>
        /// <param name="name">Название статьи расходов.</param>
        /// <returns>Результат удаления.</returns>
        // DELETE: api/ExpenseItems/5
        [HttpDelete("{name}")]
        public async Task<IActionResult> DeleteExpenseItem(string name)
        {
            var item = await _context.ExpenseItems.FindAsync(name);
            if (item == null)
            {
                return NotFound();
            }
            try
            {
                _context.ExpenseItems.Remove(item);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (DbUpdateException)
            {
                return BadRequest("Нельзя удалить статью, так как существуют зависимые от неё транзакции.");
            }
        }

        /// <summary>
        /// Проверяет наличие статьи расходов.
        /// </summary>
        /// <param name="name">Название искомой статьи расходов.</param>
        /// <returns>Результат поиска.</returns>
        private bool ExpenseItemExists(string name)
        {
            return _context.ExpenseItems.Any(e => e.Name == name);
        }
    }
}

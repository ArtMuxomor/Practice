using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinalTaskFinanceAPI.Data;
using FinalTaskFinanceAPI.Models;
using FinalTaskFinanceAPI.Constants;

namespace FinalTaskFinanceAPI.Controllers
{
    /// <summary>
    /// API-контроллер транзакций.
    /// </summary>
    // api/Transactions
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        /// <summary>
        /// Контекст БД.
        /// </summary>
        private readonly FinanceDbContext _context;

        /// <summary>
        /// Конструктор контроллера.
        /// </summary>
        /// <param name="context">Контекст БД.</param>
        public TransactionsController(FinanceDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Возвращает список всех транзакций.
        /// </summary>
        /// <returns>Коллекция существующих транзакций.</returns>
        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            return await _context.Transactions.ToListAsync();
        }

        /// <summary>
        /// Возвращает конкретную транзакцию.
        /// </summary>
        /// <param name="id">Идентификатор транзакции.</param>
        /// <returns>Найденная транзакция.</returns>
        // GET: api/Transactions/Id/5
        [HttpGet("Id/{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        /// <summary>
        /// Обновляет информацию о транзакции.
        /// </summary>
        /// <param name="id">Идентификатор транзакции.</param>
        /// <param name="transaction">Новый экземпляр транзакции.</param>
        /// <returns>Результат обновления.</returns>
        // PUT: api/Transactions/Id/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("Id/{id}")]
        public async Task<IActionResult> PutTransaction(Guid id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return BadRequest();
            }

            // Проверка существующей транзакции
            var currentTransac = await _context.Transactions
              .AsNoTracking()
              .FirstOrDefaultAsync(t => t.Id == id);

            if (currentTransac == null)
            {
                return NotFound();
            }

            // Проверка активности указанной статьи
            var originalItem = await _context.ExpenseItems
              .AsNoTracking()
              .FirstOrDefaultAsync(i => i.Name == currentTransac.ExpenseItemName);

            // Если статья стала неактивной и пользователь пытается сменить её название
            if (originalItem != null &&
              !originalItem.IsActive &&
              currentTransac.ExpenseItemName != transaction.ExpenseItemName)
            {
                return BadRequest("Статья стала неактивной. Изменение поля \"Статья расхода\" запрещено.");
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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
        /// Добавляет транзакцию.
        /// </summary>
        /// <param name="transaction">Добавляемая транзакция.</param>
        /// <returns>Результат добавления.</returns>
        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            // Проверка активности присвоенной статьи
            var expenseItem = await _context.ExpenseItems
              .AsNoTracking()
              .FirstOrDefaultAsync(i => i.Name == transaction.ExpenseItemName);

            if (expenseItem == null)
            {
                return BadRequest("Указанная статья расходов не существует.");
            }

            if (!expenseItem.IsActive)
            {
                return BadRequest("Нельзя добавлять транзакции с неактивной статьёй расходов.");
            }

            // Проверка лимита на день в сумме
            var dailySum = await _context.Transactions
              .Where(t => t.Date == transaction.Date)
              .SumAsync(t => t.Amount);

            if (dailySum + transaction.Amount > (decimal)FinanceLimits.TRANSACTION_MAX_DAY)
            {
                return BadRequest($"Превышен лимит расходов за день (макс. {FinanceLimits.TRANSACTION_MAX_DAY:N2} руб).");
            }

            if (transaction.Id == Guid.Empty)
            {
                transaction.Id = Guid.NewGuid();
            }

            _context.Transactions.Add(transaction);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TransactionExists(transaction.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTransactionById", new { id = transaction.Id }, transaction);
        }

        /// <summary>
        /// Возвращает транзакции за конкретный период (год, месяц, день).
        /// </summary>
        /// <param name="year">Год поиска.</param>
        /// <param name="month">Месяц поиска.</param>
        /// <param name="day">День поиска.</param>
        /// <returns>Информация по найденным транзакциям.</returns>
        // GET: api/Transactions/Date/2026/4/30
        [HttpGet("Date/{year:int}/{month:int?}/{day:int?}")]
        public async Task<ActionResult> GetTransactionsByDate(int year, int? month = null, int? day = null)
        {
            if (year < 1)
            {
                return BadRequest("Год должен быть не меньше 1.");
            }

            var tQuery = _context.Transactions
              .Where(t => t.Date.Year == year);

            if (month.HasValue)
            {
                if (month < 1 || 12 < month)
                {
                    return BadRequest("Указан неверный месяц.");
                }

                tQuery = tQuery.Where(t => t.Date.Month == month);
            }


            if (day.HasValue)
            {
                if (!month.HasValue)
                {
                    return BadRequest("Нельзя указать день без месяца.");
                }

                int maxDays = DateTime.DaysInMonth(year, month.Value);

                if (day < 1 || day > maxDays)
                {
                    return BadRequest($"В указанном месяце должно быть от 1 до {maxDays} дней.");
                }

                tQuery = tQuery.Where(t => t.Date.Day == day);

            }

            // Общая сумма по выбранным транзакциям
            var totalAmount = await tQuery.SumAsync(t => t.Amount);

            // Цвет стикера по сумме за день
            string? stickerColor = !day.HasValue
                         ? null
                         : totalAmount switch
                         {
                             < 500 => "Green",
                             <= 2000 => "Yellow",
                             _ => "Red"
                         };

            // ID выбранных транзакций
            var IDs = await tQuery.Select(t => t.Id).ToListAsync();

            // Успех и нужная информация
            return Ok(
              new
              {
                  Year = year,
                  Month = month,
                  Day = day,
                  TotalAmount = totalAmount,
                  StickerColor = stickerColor,
                  Transactions = IDs
              });
        }

        /// <summary>
        /// Удаляет транзакцию.
        /// </summary>
        /// <param name="id">Идентификатор транзакции.</param>
        /// <returns>Результат удаления.</returns>
        // DELETE: api/Transactions/Id/5
        [HttpDelete("Id/{id}")]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Проверяет наличие транзакции.
        /// </summary>
        /// <param name="id">Идентификатор транзакции.</param>
        /// <returns>Результат поиска.</returns>
        private bool TransactionExists(Guid id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}
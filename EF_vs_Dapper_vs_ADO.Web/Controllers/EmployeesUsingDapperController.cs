using EF_vs_Dapper_vs_ADO.Core;
using EF_vs_Dapper_vs_ADO.Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;


namespace EF_vs_Dapper_vs_ADO.Web.Controllers
{
    public class EmployeesUsingDapperController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesUsingDapperController()
        {
            _unitOfWork = new UnitOfWork();
        }

        // GET: EmployeesUsingEF
        public async Task<IActionResult> Index()
        {
            return View(await _unitOfWork.Employees.GetAllAsync());
        }

        // GET: EmployeesUsingEF/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (_unitOfWork.Employees == null)
            {
                return NotFound();
            }

            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: EmployeesUsingEF/Create
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["departments"] = new SelectList(await _unitOfWork.Departments.GetAllAsync(), "Id", "Name");
            return View();
        }

        // POST: EmployeesUsingEF/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                await _unitOfWork.Employees.AddAsync(employee);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["departments"] = new SelectList(await _unitOfWork.Departments.GetAllAsync(), "Id", "Name");
            return View(employee);
        }

        // GET: EmployeesUsingEF/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (_unitOfWork.Employees == null)
            {
                return NotFound();
            }

            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["departments"] = new SelectList(await _unitOfWork.Departments.GetAllAsync(), "Id", "Name", employee.Id);
            return View(employee);
        }

        // POST: EmployeesUsingEF/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _unitOfWork.Employees.UpdateAsync(employee);
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["departments"] = new SelectList(await _unitOfWork.Departments.GetAllAsync(), "Id", "Name", employee.Id);
            return View(employee);
        }

        // GET: EmployeesUsingEF/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (_unitOfWork.Employees == null)
            {
                return NotFound();
            }

            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: EmployeesUsingEF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_unitOfWork.Employees == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Employees'  is null.");
            }
            var employee = await _unitOfWork.Employees.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            await _unitOfWork.Employees.DeleteByIdAsync(id);
            
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return _unitOfWork.Employees.GetByIdAsync(id).Result != null;
        }
    }
}

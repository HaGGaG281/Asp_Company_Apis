using Asp_Company_Application.DTO;
using Asp_Company_Application.Interfaces;
using Asp_Company_Core.Entities;
using Asp_Company_Infrastructure.Data;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class EmployeeRepository : IEmployeeRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public EmployeeRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<addEmployeeDto> AddEmployee(addEmployeeDto employeeDto)
    {
        var employee = _mapper.Map<Employee>(employeeDto);
        await _context.Employees.AddAsync(employee);
        await _context.SaveChangesAsync();
        return _mapper.Map<addEmployeeDto>(employee);
    }


    public async Task<bool> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null) return false;

        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return true;
    }


    public async Task<EmployeeDto> GetEmployee(int id)
    {
        var employee = await _context.Employees
         .Include(e => e.Department)  // تأكد من تحميل الكائن Department مع Employee
         .FirstOrDefaultAsync(e => e.Id == id);

        // في حالة عدم العثور على الموظف، يمكنك إرجاع null أو رفع استثناء
        if (employee == null)
            return null;

        // تحويل الكائن إلى DTO باستخدام AutoMapper
        return _mapper.Map<EmployeeDto>(employee);
    }


    public async Task<IEnumerable<EmployeeDto>> GetEmployees()
    {
        var employees = await _context.Employees.Include(e => e.Department).ToListAsync();
        return _mapper.Map<IEnumerable<EmployeeDto>>(employees);
    }

    public async Task<addEmployeeDto> UpdateEmployee(int id , addEmployeeDto employeeDto)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
        if (employee == null) return null;

        _mapper.Map(employeeDto, employee);
        _context.Employees.Update(employee);

        await _context.SaveChangesAsync();
        return _mapper.Map<addEmployeeDto>(employee);

    }


}

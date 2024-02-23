using Microsoft.EntityFrameworkCore.Storage;
using MyFinances.WebApi.Models.Domains;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyFinances.WebApi.Models.Repositories
{
    public class OperationRepository
    {
        private readonly MyFinancesContext _context;

        public OperationRepository(MyFinancesContext context)
        {
            _context = context;
        }

        public IEnumerable<Operation> Get()
        {
            return _context.Operations;
        }

        public Operation Get(int id)
        {
            return _context.Operations.SingleOrDefault(x => x.Id == id);
        }

        public IEnumerable<Operation> Get(int records, int page)
        {

            return _context.Operations.Skip(records * (page - 1)).Take(records);
        }

        public void Add(Operation operation)
        {
            operation.Date = DateTime.Now;
            _context.Operations.Add(operation);
        }

        public void Update(Operation operation)
        {
            var operationToUpdate = _context.Operations.Single(x => x.Id == operation.Id);
            operationToUpdate.CategoryId = operation.CategoryId;
            operationToUpdate.Description = operation.Description;
            operationToUpdate.Name= operation.Name;
            operationToUpdate.Value = operation.Value;

        }

        public void Delete(int id)
        {
            var operationToDelete = _context.Operations.Single(x => x.Id == id);
            _context.Operations.Remove(operationToDelete);

        }
    }
}

using Descartes.Controllers;
using Descartes.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Descartes.Repository
{
    public interface IDifferenceRepository
    {
        public string DetermineDifferences(int id);

        public void SaveObject(string identifier, RequestDifferenceInputHelper requestInput, int id);

        public List<DifferenceObject> GetAllDatabaseContent();
    }
}

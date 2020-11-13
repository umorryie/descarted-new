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
        public string determineDifferences(int id);

        public void saveObject(string identifier, RequestDifferenceInputHelper requestInput, int id);

        public List<DifferenceObject> getAllDatabaseContent();
    }
}
